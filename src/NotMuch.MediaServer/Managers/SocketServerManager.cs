using NotMuch.MediaServer.Abstrusts;
using NotMuch.MediaServer.Enums;
using NotMuch.MediaServer.SocketServices;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotMuch.MediaServer.Managers;

public class SocketServerInput
{
    public SocketServerStatus Status { get; set; } = SocketServerStatus.None;
    public string? KeyWord { get; set; }
}

public class SocketServerManager
{
    private readonly ConcurrentDictionary<Guid, ISocketServer<INotMuchSession>> _servers = new();

    public void AddOrUpdateServer(ISocketServer<INotMuchSession> server)
    {
        if (_servers.ContainsKey(server.Id))
        {
            _servers[server.Id] = server;
        }
        else
        {
            _servers.TryAdd(server.Id, server);
        }

    }

    public ISocketServer<INotMuchSession> RemoteServer(Guid id)
    {
        _servers.TryGetValue(id, out var server);

        return server;
    }

    public int Count()
    {
        return _servers.Count;
    }

    public ISocketServer<INotMuchSession>? GetById(Guid id)
    {
        return _servers.GetValueOrDefault(id);
    }

    public List<ISocketServer<INotMuchSession>> GetAll(SocketServerInput? input = null)
    {
        var query= _servers.Values.AsQueryable();

        if (input is null) return query.ToList();

        if(input.Status != SocketServerStatus.None)
        {
            query = query.Where(x => x.Status == input.Status);
        }

        if(string.IsNullOrWhiteSpace(input.KeyWord))
        {
            query = query.Where(x => x.Name.Contains(input.KeyWord));
        }

        return query.ToList();
    }

    //public void CreteServer<TAppSession, TPackageInfo, TPipelineFilter>(SocketServerInfo info)
    //    where TAppSession : INotMuchSession
    //    where TPackageInfo : class, IPackageInfo
    //    where TPipelineFilter : IPipelineFilter<TPackageInfo>, new()
    //{
    //    if (info is null) throw new ArgumentNullException(nameof(info));

    //    var server = new SocketServer<INotMuchSession, IPackageInfo, TPipelineFilter>(info);

    //    AddOrUpdateServer(server);

    //}
}
