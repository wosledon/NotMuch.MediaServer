using System;
using System.Threading.Tasks;
using NotMuch.MediaServer.Enums;

namespace NotMuch.MediaServer.SocketServices;

public class SocketServerInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = "Any";
    public int Port { get; set; } = 0;
    public bool IsSsl { get; set; } = false;
    public ProtocolType ProtocolType { get; set; } = ProtocolType.Tcp;
    public int BackLog { get; set; } = 100;
}