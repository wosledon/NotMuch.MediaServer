using System;
using System.Threading.Tasks;

namespace NotMuch.MediaServer;

public class SocketServerOption
{
    public string Host { get; set; } = "Any";
    public int Port { get; set; } = 0;
    public bool IsSsl { get; set; } = false;
    public ProtocolType ProtocolType { get; set; } = ProtocolType.Tcp;
    public int BackLog { get; set; } = 100;
    public Func<Exception, Task> Logger { get; set; } = _ => Task.CompletedTask;
    public Func<Task> ProxyReceivePackage { get; set; } = () => Task.CompletedTask;
}
