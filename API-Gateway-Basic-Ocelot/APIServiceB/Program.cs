using APIServiceB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
builder.Services.AddConsulConfig(builder.Configuration);

// Run on port 9998
builder.WebHost.UseUrls("http://*:9998");

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();
app.UseHttpsRedirection().UseAuthorization();

app.UseConsul();

app.MapControllers();

app.Run();
