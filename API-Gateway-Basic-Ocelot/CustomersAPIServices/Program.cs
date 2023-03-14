using Ocelot.JwtAuthorize;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTokenJwtAuthorize();

// Run on port 9001
builder.WebHost.UseUrls("http://*:9001");

// http://localhost:9001/api/customers

var app = builder.Build();

app.UseHttpsRedirection().UseAuthorization();
app.MapControllers();

app.Run();
