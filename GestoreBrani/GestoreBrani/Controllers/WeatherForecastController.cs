using Microsoft.AspNetCore.Mvc;

namespace GestoreBrani.Controllers;

[ApiController] 
[Route("[controller]")] // permette specificare il percorso base verso i nostri endpoints
public class WeatherForecastController : ControllerBase // la naming convention dice che deve avere il sufisso Controller e deve reditare della classe ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) // constructor ha un parametro d'ingresso ILogger. Questo parametro lo passa e valorizza
                                                                                // la dependency injection (desacoppiare chiamante e chiamato per configurare questa cosa)
    {
        _logger = logger; // salvare questo parametro in una variabile locale
    }

    [HttpGet(Name = "GetWeatherForecast")] // questo endpoint deve essere chiamato con il verbo get e non vuole parametri
    public IEnumerable<WeatherForecast> Get() // metodo che viene trasformato in un endpoint tramitte l'atributto di riga 21
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

