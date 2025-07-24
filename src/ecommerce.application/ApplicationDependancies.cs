using ecommerce.Application.Behaviors;
using ecommerce.Application.Interfaces;
using ecommerce.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;


namespace ecommerce.Application
{
    public  static class ApplicationDependancies
    {
        public static IServiceCollection AddApplicationDependanies(this IServiceCollection services)
        {
            
            
            // Add AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // Add FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            // Add Application Services
            services.AddScoped<IProductService, PorductService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthenticationSerive, AuthenticationSerivice>();
            services.AddScoped<IUserSerivce, UserService>();
            services.AddHttpContextAccessor();
            services.AddMvc();
            return services;
        }
    }
}
