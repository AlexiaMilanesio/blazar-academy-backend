using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")] // Routing generale che si applica a tutti
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


    [HttpGet(Name = "GetWeatherForecast")] // Non è neccessario utilizzare "Name = ..."
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }


    // Se abbiamo anche il seguente metodo, senza modificarela rotta, la app non funziona perché sono due endpoint con lo stesso nome --> Soluzione: Route/Routing/Http
    //[HttpGet(Name = "GetWeatherForecast2")]
    //public IEnumerable<WeatherForecast> Get2()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}


    // Esercizio 1
    [HttpGet("{id}")]
    public string Get2(int id)
    {
        return $"Hai passato l'id: {id}";
    }


    // Cambio il atributto, il verbo. Ora sono due endpoint completamente diversi
    [HttpPost("{id}")]
    public async Task<string> Post2(int id)
    {
        // Ottener il body:
        // Request contiene tutta l'info che servono al controller per eseguire il suo lavoro, tra cui il body
        StreamReader readBody1 = new StreamReader(Request.Body); 
        string body1 = await readBody1.ReadToEndAsync();

        return $"Hai restituito l'id: {id}";
    }


    // Esercizio 2: endpoint che riceva una stringa e un intero senza passare i dati per query string o body
    [HttpGet("{stringa}/{intero}")]
    public string Get4(string stringa, int intero)
    {
        return $"Hai restituito: {stringa} {intero}";
    }


    // Prova
    [HttpPost("{stringa}/{intero}")]
    public async Task<string> Post4(string stringa, int intero)
    {
        StreamReader readBody2 = new StreamReader(Request.Body);
        string body2 = await readBody2.ReadToEndAsync();

        return $"Hai restituito: {stringa} {intero}";
    }   
}

