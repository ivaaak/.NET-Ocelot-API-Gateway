using APIGatewayBasic;
using Ocelot.JwtAuthorize;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Run on port 9000   // http://localhost:9000
builder.WebHost.UseUrls("http://*:9000");


// Add Ocelot config file
builder.Configuration
    .AddJsonFile("appsettings/appsettings-jwtAuth.json")
    .AddJsonFile("ocelotConfigs/ocelotJWTAuth.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services
    .AddOcelotWithJWTAuth(builder.Configuration)
    .AddOcelotJwtAuthorize();


var app = builder.Build();
await app.UseOcelot();

app.UseAuthentication();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
