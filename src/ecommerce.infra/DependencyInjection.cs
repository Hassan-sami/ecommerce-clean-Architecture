using ecommerce.Domain.Interfaces;
using ecommerce.infra.Context;
using ecommerce.infra.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace ecommerce.infra
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfraDependency(this IServiceCollection services, IConfiguration configurationManager)
        {
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, Productrepository>();
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configurationManager.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }

    


}
