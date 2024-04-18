using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Abstractions.Session;
using SuperSocket.Server.Host;

namespace NotMuch.MediaServer;

public class SocketServer<TAppSession> : SocketServer<TAppSession, StringPackageInfo, CommandLinePipelineFilter>
    where TAppSession : IAppSession
{
    public SocketServer(SocketServerOption option) : base(option)
    {
    }
}

public class SocketServer<TAppSession, TPackageInfo, TPipelineFilter>(SocketServerOption option) : ISocketServer
    where TAppSession : IAppSession
    where TPackageInfo : class
    where TPipelineFilter : IPipelineFilter<TPackageInfo>, new()
{
    public Guid Id { get; } = Guid.NewGuid();

    private IServer? _server;

    private IServer Server
    {
        get
        {
            _server ??= Init();

            return _server;
        }
        set => _server = value;
    }

    private IServer Init()
    {
        return SuperSocketHostBuilder.Create<TPackageInfo, TPipelineFilter>()
            .ConfigureSuperSocket(opts =>
            {
                opts.AddListener(new ListenOptions()
                {
                    Ip = option.Host,
                    Port = option.Port,
                    BackLog = option.BackLog,
                    Security = option.IsSsl ? SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 : SslProtocols.None,
                });
            })
            .UseSession<TAppSession>()
            .UsePackageHandler(async (s, p) =>
            {
#if DEBUG
                Console.WriteLine(p);
#endif
                await option.ProxyReceivePackage.Invoke();
                // await s.SendAsync(null);
            })
            .ConfigureErrorHandler(async (s, p) =>
            {
                await option.Logger.Invoke(p.InnerException ?? p);

                return true;
            })
            .UseClearIdleSession()
            .UseInProcSessionContainer()
            .UseMiddleware<InProcSessionContainerMiddleware>()
            .ConfigureLogging((hostCtx, loggingBuilder) =>
            {
                loggingBuilder.AddConsole();
            })
            .BuildAsServer();
    }

    public async Task StartAsync()
    {
        await Server.StartAsync();
    }

    public async Task StopAsync()
    {
        await Server.StopAsync();
    }
}

