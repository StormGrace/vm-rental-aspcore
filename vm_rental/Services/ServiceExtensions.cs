using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using vm_rental.Data;
 

namespace vm_rental.Models.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services,
                                                      IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<vmDbContext>(o => o.UseMySql(connectionString));
        }
    }
}