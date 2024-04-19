using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotMuch.MediaServer.Abstrusts;
using NotMuch.MediaServer.Enums;
using NotMuch.MediaServer.Extensions;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.Server.Abstractions;
using SuperSocket.Server.Abstractions.Session;
using SuperSocket.Server.Host;

namespace NotMuch.MediaServer.SocketServices;

public class SocketServer<TAppSession, TPackageInfo, TPipelineFilter>(SocketServerInfo option) 
    : ISocketServer<TAppSession>
    where TAppSession : INotMuchSession
    where TPackageInfo : class, IPackageInfo
    where TPipelineFilter : IPipelineFilter<TPackageInfo>, new()
{
    public Guid Id => option.Id;

    public string Name => option.Name;

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

    public SocketServerStatus Status { get; private set; } = SocketServerStatus.Stopped;

    public SocketServerInfo Info => option;

    public ISessionContainer GetSessionContainer()
    {
        return _server.GetSessionContainer();
    }

    public TAppSession GetSession(string id)
    {
        return GetSessionContainer().GetSessionByID(id).As<TAppSession>();
    }

    public int GetSessionCount()
    {
        return GetSessionContainer().GetSessionCount();
    }

    public IEnumerable<TAppSession> GetSessions()
    {
        return GetSessionContainer().GetSessions().Select(x => x.As<TAppSession>());
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
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]-[{Name}:{Id}]-[Session:{s.SessionID}]{Environment.NewLine}Receive Date:{Environment.NewLine}{p}");
#endif
                await s.As<TAppSession>().ProxyReceivePackage.Invoke();
                // await s.SendAsync(null);
            })
            .ConfigureErrorHandler(async (s, p) =>
            {
                await s.As<TAppSession>().Logger.Invoke(p);

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
        try
        {
            await Server.StartAsync();
            Status = SocketServerStatus.Running;
        }
        catch
        {
            Status = SocketServerStatus.Stopped;
        }
    }

    public async Task StopAsync()
    {
        try
        {
            await Server.StopAsync();
            Status = SocketServerStatus.Stopped;
        }
        catch
        {
            Status = SocketServerStatus.Running;
        }
    }
}

