using System.Security.Claims;
using System.Security.Cryptography;
using ecommerce.Application.Interfaces;
using ecommerce.Application.options;
using ecommerce.Domain.Enitities.Identities;
using ecommerce.Domain.Interfaces;
using ecommerce.infra.Context;
using ecommerce.infra.Repos;
using ecommerce.infra.Serivces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using GoogleOptions = ecommerce.Application.options.GoogleOptions;
using JwtOptions = ecommerce.Application.options.JwtOptions;


namespace ecommerce.infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services,
            IConfiguration configurationManager)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, Productrepository>();
            services.AddScoped<ITokenSerivce, TokenService>();
            services.AddScoped<IRefreshTokenRepo, RefreshTokenRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRespository>();
            services.AddScoped<IRefreshTokenService, RefreshTOkenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configurationManager.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            var jwtOptions = configurationManager.GetSection("jwt").Get<JwtOptions>();
            
            
            services.Configure<JwtOptions>(configurationManager.GetSection("jwt"));
            services.Configure<EmailSettings>(configurationManager.GetSection("emailSettings"));
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(jwtOptions.Key), out _);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.ClaimsIssuer = jwtOptions.Issuer;
                    opt.Audience = jwtOptions.Audience;
                    opt.SaveToken = false;
                    opt.MapInboundClaims = false;

                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new RsaSecurityKey(rsa),
                        RoleClaimType = ClaimTypes.Role
                    };
                });
            var googleOptions = configurationManager.GetSection("Google").Get<GoogleOptions>();
            services.AddAuthentication()
                .AddCookie("cookieg")
                .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
                {
                    options.ClientId = googleOptions.client_id;
                    options.ClientSecret = googleOptions.client_secret;
                    options.CallbackPath = googleOptions.redirect_uris[0];
                    options.SignInScheme = "cookieg";
                    
                });
            services.AddScoped<CreateTopSelletStoredProducre>();
            return services;
        }
    }
}