using Kyzmat.BLL.Interfaces;
using Kyzmat.BLL.Services;
using Kyzmat.DAL;
using Kyzmat.DAL.Implementations;
using Kyzmat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kyzmat.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("AppDbContext"));
            });
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPaymentService, PaymentService>();

            return services;
        }
    }
}
