using KaseyWebApi.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace KaseyWebApi.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class WeatherForecastController : Controller
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("GETTING WEATHER FORECAST");
        return Enumerable.Range(1, 11).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}