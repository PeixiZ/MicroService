using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{  
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task Get()
    {
        await HttpContext.Response.WriteAsync(HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + ":" + HttpContext.Connection.RemotePort.ToString() + "=>");
        await HttpContext.Response.WriteAsync(HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + HttpContext.Connection.LocalPort.ToString());
    }
}
