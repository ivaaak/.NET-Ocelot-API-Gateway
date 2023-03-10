using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace APIGatewayBasic.Properties
{
    public class StartupExtensions
    {
        public StartupExtensions(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                //add configuration.json
                .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }

        public void ConfigureJWTAuth(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "TestKey";
            }).AddOAuth("provider", options =>
            {
                options.ClientId = "your-client-id";
                options.ClientSecret = "your-client-secret";
                options.CallbackPath = "/signin-google"; 
                // the path to the Google callback handler in your app
            });
            /*
            .AddJwtBearer("TestKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            */

            services.AddOcelot(Configuration);
        }

        public async void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseAuthentication();
            await app.UseOcelot();
        }
    }
}
