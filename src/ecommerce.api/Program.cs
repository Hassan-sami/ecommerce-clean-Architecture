
using System.Globalization;
using ecommerce.Application;
using ecommerce.infra;
using Microsoft.AspNetCore.Localization;

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

            builder.Services.AddApplicationDependanies();
            builder.Services.AddInfraDependency(builder.Configuration);
            
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
