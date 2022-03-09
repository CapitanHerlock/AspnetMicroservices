using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureLogging((hostingContext, loggingbuilder) =>
{
	loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
	loggingbuilder.AddConsole();
	loggingbuilder.AddDebug();
});

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json");
});
builder.Services.AddOcelot().AddCacheManager(x =>
{
	x.WithDictionaryHandle();
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
