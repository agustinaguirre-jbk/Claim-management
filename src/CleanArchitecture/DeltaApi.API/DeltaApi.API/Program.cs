using DeltaApi.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configurar pipeline
app.ConfigurePipeline();

app.Run();
