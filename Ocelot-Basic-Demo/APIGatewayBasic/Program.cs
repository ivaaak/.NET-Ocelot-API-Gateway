using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

// Run on port 9000   // http://localhost:9000
builder.WebHost.UseUrls("http://*:9000");

// Only the projects added to the ocelot config should be set to startup, the rest will be unreachable for the gateway.

// Add Ocelot config file
builder.Configuration
    .AddJsonFile("ocelotConfigs/ocelotBasic.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotConsulConfig.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotConsul.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotJWTAuth.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotLoadBalance.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotQualityOfService.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotRateLimit.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotSwagger.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("ocelotConfigs/ocelotEurekaSD.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddOcelot(builder.Configuration);


var app = builder.Build();

await app.UseOcelot();      

app.UseHttpsRedirection().UseAuthorization();
app.MapControllers();

app.Run();

// Currently unused:
// Consul Config
//    builder.Services.AddConsul().AddConfigStoredInConsul();   // Store the configuration in consul 


// Auth0
//    app.UseRouting();
//    app.UseAuthentication();
//    app.UseAuthorization();
//    app.UseEndpoints(endpoints =>
//    {
//        endpoints.MapControllers();
//    });
//
