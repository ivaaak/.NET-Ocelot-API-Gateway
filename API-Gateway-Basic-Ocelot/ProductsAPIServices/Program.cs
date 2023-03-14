var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Run on port 9002
builder.WebHost.UseUrls("http://*:9002");

// http://localhost:9002/api/products

var app = builder.Build();

app.UseHttpsRedirection().UseAuthorization();
app.MapControllers();

app.Run();
