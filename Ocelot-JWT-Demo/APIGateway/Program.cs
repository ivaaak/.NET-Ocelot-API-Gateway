using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var logging = builder.Logging;
var services = builder.Services;
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(9000);
});

configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("gateway.json", false, true)
    .AddEnvironmentVariables();

var audienceConfig = configuration.GetSection("Audience");

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(audienceConfig["Secret"]));
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = audienceConfig["Issuer"],
    ValidateAudience = true,
    ValidAudience = audienceConfig["AudienceParameter"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = signingKey,
    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

services.AddAuthentication(o => { o.DefaultAuthenticateScheme = "TestKey"; })
    .AddJwtBearer("TestKey", x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = tokenValidationParameters;
    });

services.AddOcelot();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseOcelot().Wait();

app.MapControllers();
app.Run();