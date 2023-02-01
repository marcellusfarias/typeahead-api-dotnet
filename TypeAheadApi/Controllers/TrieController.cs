using Microsoft.AspNetCore.Mvc;

namespace TypeAheadApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TrieController : ControllerBase
{
    private readonly ILogger<TrieController> _logger;

    public TrieController(ILogger<TrieController> logger)
    {
        _logger = logger;
    }

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //         TemperatureC = Random.Shared.Next(-20, 55),
    //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     })
    //     .ToArray();
    // }
}