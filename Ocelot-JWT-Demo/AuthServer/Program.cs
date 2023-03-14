using System.Text;
using AuthServer.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var logging = builder.Logging;
var services = builder.Services;

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(9001); 
});

configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();

services.Configure<Audience>(configuration.GetSection("Audience"));
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

services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseAuthentication().UseAuthorization();

app.MapControllers();
app.Run();
