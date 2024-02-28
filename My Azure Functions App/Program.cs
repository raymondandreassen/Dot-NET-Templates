using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Orginalt 
//
//var host = new HostBuilder()
//    .ConfigureFunctionsWebApplication()
//    .ConfigureServices(services =>
//    {
//        services.AddApplicationInsightsTelemetryWorkerService();
//        services.ConfigureFunctionsApplicationInsights();
//    })
//    .Build();
//host.Run();

// En periode gjorde vi det slik
//var host = new HostBuilder()
//    .ConfigureFunctionsWebApplication()
//    .ConfigureHostConfiguration(c =>
//    {

//    })

//    .ConfigureAppConfiguration((hostingContext, configuration) =>
//    {
//        configuration.AddUserSecrets<Program>();
//        configuration.AddEnvironmentVariables();
//        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//    })

//    .ConfigureLogging(logger =>
//    {
//        logger.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
//    })

//    .ConfigureServices((hostingcontext, services) =>
//    {
//        //services.AddDbContext<MyDbContext>(options => { options.UseSqlServer(Environment.GetEnvironmentVariable("FellaDb")); });
//        services.AddHttpClient("h_client", config =>
//        {
//            config.BaseAddress = new Uri(Environment.GetEnvironmentVariable("Setting"));
//            config.Timeout = new TimeSpan(0, 0, 120);
//            config.DefaultRequestHeaders.Clear();
//            config.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//            var authString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{Environment.GetEnvironmentVariable("Username")}:{Environment.GetEnvironmentVariable("Password")}"));
//            config.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);
//        });

//    })

//    .Build();
//host.Run();

var host = new HostBuilder();
host.ConfigureFunctionsWebApplication();

host.ConfigureHostConfiguration(configBuilder =>
    {
    });

host.ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.AddUserSecrets<Program>();
        configuration.AddEnvironmentVariables();
        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    });

host.ConfigureLogging(logger =>
    {
        logger.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
    });

//host.ConfigureMetrics( configureMetrics =>
//    {
//    });

host.ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    });

host.Build().Run();

