
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace SteamWebApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontendApp", policy =>
                {
                    policy.WithOrigins("https://localhost:4200") // Replace with your frontend URL
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "Steam";
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "SteamAuth";
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            })
            .AddSteam(options =>
            {
                options.Authority = new Uri(SteamAuthenticationDefaults.Authority);
                options.CallbackPath = SteamAuthenticationDefaults.CallbackPath;
                options.ClaimsIssuer = SteamAuthenticationDefaults.DisplayName;
                options.SaveTokens = true;

                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                IConfiguration configuration = configurationBuilder.AddUserSecrets<Program>().Build();

                options.ApplicationKey = configuration.GetValue<string>("SteamAPIKey");
                options.SaveTokens = true;
                options.Events.OnTicketReceived = context_ =>
                {
                    return Task.CompletedTask;
                };
                options.Events.OnAuthenticated = context_ =>
                {
                    var steamUserAsClaims = context_.Identity;

                    //Example: get steamid from claims. Leaving for example. We now have static methods to do this.
                    //var nameIdentifier = steamUserAsClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    //Example: get steam username from claims. Leaving for example. We now have static methods to do this.
                    var name = steamUserAsClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                    //context_.HttpContext.SignInAsync(new ClaimsPrincipal(steamUserAsClaims), new AuthenticationProperties()).Wait(TimeSpan.FromSeconds(5));


                    return Task.CompletedTask;
                };
            });

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<SteamService>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var apiKey = configuration["SteamAPIKey"];
                var httpClient = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();

                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new InvalidOperationException("Steam API key is not configured.");
                }

                return new SteamService(apiKey, httpClient);
            });

            var app = builder.Build();

            app.UseCors("AllowFrontendApp");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
