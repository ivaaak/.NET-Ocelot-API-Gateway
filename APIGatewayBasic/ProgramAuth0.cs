/*
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Run on port 9000   // http://localhost:9000
builder.WebHost.UseUrls("http://*:9000");



// Add Ocelot config file
builder.Configuration.AddJsonFile("ocelotJWTAuth.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddOcelot(builder.Configuration);

var app = builder.Build();
await app.UseOcelot();

// Auth0
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection().UseAuthorization();
app.MapControllers();

app.Run();
*/