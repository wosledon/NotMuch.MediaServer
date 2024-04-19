using Microsoft.AspNetCore.Mvc;
using NotMuch.MediaServer.Managers;
using NotMuch.MediaServer.SocketServices;
using SQLitePCL;

namespace NotMuch.MediaServer.Dashboard.Controllers;

/// <summary>
/// Socket服务信息
/// </summary>
public class SocketServerInfoController:ApiControllerBase
{
    public SocketServerInfoController()
    {
        
    }
}


/// <summary>
/// Socket服务
/// </summary>
public class SocketServerController:ApiControllerBase
{
    private readonly SocketServerManager _socketServerManager;

    public SocketServerController(SocketServerManager socketServerManager)
    {
        _socketServerManager = socketServerManager;
    }

    /// <summary>
    /// 获取所有SocketServer服务
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Servers(SocketServerInput input)
    {
        var res = _socketServerManager.GetAll(input);

        return Ok(res);
    }

    /// <summary>
    /// 打开服务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> StartAsync(Guid id)
    {
        var server = _socketServerManager.GetById(id);

        if(server is null)
        {
            return NotFound("未找到服务");
        }

        await server.StartAsync();

        return Ok();
    }

    /// <summary>
    /// 关闭服务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> StopAcync(Guid id)
    {
        var server = _socketServerManager.GetById(id);

        if(server is null)
        {
            return NotFound("未找到服务");
        }

        await server.StopAsync();

        return Ok();
    }

    /// <summary>
    /// 创建一个Socket服务
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]SocketServerInfo info)
    {
        return Ok();
    }
}

