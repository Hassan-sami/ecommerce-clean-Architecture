
using System.Globalization;
using ecommerce.Application;
using ecommerce.infra;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var config = app.Configuration.Get<RequestLocalizationOptions>();
            app.UseRequestLocalization(config);
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
