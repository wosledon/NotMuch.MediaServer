using NotMuch.MediaServer.Abstrusts;
using NotMuch.MediaServer.Enums;
using SuperSocket.Server.Abstractions.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotMuch.MediaServer.SocketServices;

public interface ISocketServer<TAppSession> where TAppSession:INotMuchSession
{
    Guid Id { get; }
    string Name { get; }
    SocketServerInfo Info { get; }
    SocketServerStatus Status { get; }
    TAppSession GetSession(string id);
    ISessionContainer GetSessionContainer();
    int GetSessionCount();
    IEnumerable<TAppSession> GetSessions();
    Task StartAsync();
    Task StopAsync();
}