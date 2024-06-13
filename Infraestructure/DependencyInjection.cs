using Domain.Dto;
using Infraestructure.Infraestructure;
using Infraestructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    using Microsoft.EntityFrameworkCore;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings:TodoDatabase").Value;

            serviceCollection.AddDbContext<TodoDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            serviceCollection.AddScoped<IRepositoryBase<TodoDto>, TodoRepository>();

            return serviceCollection;
        }
    }
}
