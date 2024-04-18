using System;
using System.Threading.Tasks;

namespace NotMuch.MediaServer;

public interface ISocketServer
{
    Guid Id { get; }
    Task StartAsync();
    Task StopAsync();
}