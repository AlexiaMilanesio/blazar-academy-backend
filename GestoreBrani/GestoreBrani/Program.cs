using GestoreBrani;
using GestoreBrani.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


// CONFIGURATION
    // AddJsonFile --> parametro 2: bool optional: file opzionale o no?
    // AddJsonFile --> parametro 3: bool reloadOnChange: il sistema monitora questo file
    // Build --> converte questo oggetto in un IConfigurationRoot
var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", false, true).Build();


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
    // builer.Services --> contenitore di oggetti che utilizza la dependency injection


// CONFIGURATION MY SETTINGS
//MySettings testSettings = new MySettings();
builder.Services.Configure<MySettings>(options => configuration.GetSection("MySettings").Bind(options));


// CONTEXT
    // quando aggiungo qualcosa della dependency injection aggiungo un servizio, quindi devo passare il nostro data-context qui
builder.Services.AddDbContext<SongManagerContext>(
    // options è passata per la dependency injection stessa
    // questa stringa in realtà va messa nel file di configurazione (properties)
    options => options.UseSqlServer(configuration.GetConnectionString("SongManagerDb"))
); ;



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


