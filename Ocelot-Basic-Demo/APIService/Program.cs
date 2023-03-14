var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
// Run on port 9001
builder.WebHost.UseUrls("http://*:8989");

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();
app.UseHttpsRedirection().UseAuthorization();

app.MapControllers();

app.Run();
