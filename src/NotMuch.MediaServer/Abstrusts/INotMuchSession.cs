using SuperSocket.Server.Abstractions.Session;
using System;
using System.Threading.Tasks;

namespace NotMuch.MediaServer.Abstrusts;

public interface INotMuchSession:IAppSession
{
    public Func<Exception, Task> Logger { get; set; }    
    public Func<Task> ProxyReceivePackage { get; set; }
}
