using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Oracle.ManagedDataAccess.Client;
using Work360.Infrastructure.Context;
using Work360.Infrastructure.Health;
using Work360.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços de controllers
builder.Services.AddControllers();

builder.Services.AddScoped<IHateoasService, HateoasService>();



builder.Services.AddHealthChecks()
    .AddCheck("Oracle", new OracleHealthCheck(
        builder.Configuration.GetConnectionString("Oracle")
    ));


// Adicione versionamento de API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Adicione ApiExplorer para HATEOAS ou Swagger
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


// Adicionar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o DbContext
builder.Services.AddDbContext<Work360Context>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            timestamp = DateTime.UtcNow,
            duration = report.TotalDuration,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                duration = e.Value.Duration,
                exception = e.Value.Exception?.Message,
                data = e.Value.Data
            })
        }, new JsonSerializerOptions { WriteIndented = true });
        await context.Response.WriteAsync(result);
    }
});


// Habilitar Swagger apenas em desenvolvimento (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Work360 API V1");
        c.RoutePrefix = string.Empty; // Para acessar em http://localhost:5000/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();



app.Run();
