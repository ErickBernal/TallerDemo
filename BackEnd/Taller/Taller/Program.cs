using Microsoft.EntityFrameworkCore;
using Taller.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog;
using System.Configuration;
//using Taller.Migrations;

var builder = WebApplication.CreateBuilder(args);


//var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//logger.Debug("holi");

////configure NLog
//builder.Services.AddLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.SetMinimumLevel(LogLevel.Trace);
//});
//// Add NLog as the logger provider
//builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();



//---------------------- Nlog ----------------------
var LoggingConfig = new ConfigurationBuilder()
.SetBasePath(System.IO.Directory.GetCurrentDirectory())
.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
.Build();
NLog.LogManager.Configuration = new NLogLoggingConfiguration(LoggingConfig.GetSection("Logging"));
var logger = LogManager.Setup()
                       .LoadConfigurationFromAppSettings()
                       .GetCurrentClassLogger();
try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    );

    var app = builder.Build();

    // Crea el contexto de datos dentro del �mbito del middleware
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataContext>();
        context.Database.EnsureCreated();
        context.Database.Migrate();
    }


    app.UseCors(options =>
    {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    //app.UseMiddleware<RequestResponseLoggingMiddleware>();
    app.UseMiddleware<EndpointLoggerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

    //comands
    //add - migration initial - o Data / migrations
    //EntityFrameworkCore\add-migration initiaD -o Data/migrations
    //update-database
    //EntityFrameworkCore\Update-Database
}
catch (Exception e)
{
    logger.Error(e, "se encontro un error");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
