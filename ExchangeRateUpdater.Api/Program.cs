using ExchangeRateUpdater.Api.Middleware;
using ExchangeRateUpdater.Application;
using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Hangfire;
using Hangfire.MySql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddRateLimiter(options =>
{
    options.AddConcurrencyLimiter("ConcurrencyPolicy", limiterOptions =>
    {
        limiterOptions.PermitLimit = 25;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 5;
    });
});

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration);

builder.Services.AddHangfire(config => config
    .UseStorage(new MySqlStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new MySqlStorageOptions
    {
        TablesPrefix = "Hangfire_"
    })));
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(_ => { });

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

var jobScheduler = app.Services.GetRequiredService<IExchangeRateJobScheduler>();
jobScheduler.ScheduleDailyExchangeRateUpdate();


app.Run();
