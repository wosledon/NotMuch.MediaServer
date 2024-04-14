using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Abstractions.Session;
using SuperSocket.Server.Host;

namespace NotMuch.MediaServer;

public class SocketServerOption
{
    public string Host { get; set; } = "Any";
    public int Port { get; set; } = 0;
    public bool IsSsl { get; set; } = false;
    public ProtocolType ProtocolType { get; set; } = ProtocolType.Tcp;
    public int BackLog { get; set; } = 100;
    public Action<Exception>? Logger { get; set; }
}

public class SocketServer<TAppSession>(SocketServerOption option) : ISocketServer
    where TAppSession : IAppSession
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
        return SuperSocketHostBuilder.Create<StringPackageInfo, CommandLinePipelineFilter>()
            .ConfigureSuperSocket(opts =>
            {
                opts.AddListener(new ListenOptions()
                {
                    Ip = option.Host,
                    Port = option.Port,
                    BackLog = option.BackLog,
                });
            })
            .UseSession<TAppSession>()
            .UsePackageHandler(async (s, p) =>
            {
                await s.SendAsync(null);
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

public interface ISocketServer
{
    Guid Id { get; }
    Task StartAsync();
    Task StopAsync();
}