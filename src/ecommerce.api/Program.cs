
using System.Globalization;
using ecommerce.Application;
using ecommerce.Application.options;
using ecommerce.infra;
using ecommerce.infra.Context;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(80); // HTTP
                serverOptions.ListenAnyIP(443, listenOptions =>
                {
                    listenOptions.UseHttps("https/devcert.pfx", "YourPassword123");
                });
            });
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region other regions depenancyies
                builder.Services.AddApplicationDependanies();
                builder.Services.AddInfraDependency(builder.Configuration);
                
            #endregion
            builder.Services.AddAuthorization();
            #region localization

                builder.Services.AddLocalization(opt => opt.ResourcesPath = "");
                var cultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG"),
                };
                builder.Services.Configure<RequestLocalizationOptions>(opt =>
                {
                    opt. SupportedCultures = cultures;
                    opt.SupportedUICultures = cultures;
                    opt.DefaultRequestCulture = new RequestCulture("en-US");
                    opt.RequestCultureProviders = new List<IRequestCultureProvider>()
                    {
                        new AcceptLanguageHeaderRequestCultureProvider(),
                        new CookieRequestCultureProvider(),
                        new QueryStringRequestCultureProvider(),
                    };
                });

            #endregion
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
            var corsconfig = builder.Configuration.GetSection("cors").Get<Cors>();
            builder.Services.AddCors(opt =>
                {
                
                opt.AddPolicy("mainPolicy", policy =>
                {
                    policy.WithOrigins(corsconfig?.origins).AllowAnyHeader().AllowAnyMethod();

                });
            });
            
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); 
                db.Database.Migrate(); // Applies migrations at startup
                
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var config = app.Configuration.Get<RequestLocalizationOptions>();
            app.UseRequestLocalization(config);
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("mainPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
