using Microsoft.EntityFrameworkCore;
using MoneyMeAPI.Data;
using MoneyMeAPI.Interfaces;
using MoneyMeAPI.Services.Repayments;
using MoneyMeAPI.Services.URLGenerator;
using System.Diagnostics.CodeAnalysis;

namespace MoneyMeAPI.Extensions
{
    [ExcludeFromCodeCoverage]

    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IURLGeneratorService, URLGeneratorService>();
            services.AddScoped<IPMTCalculator, PMTCalculator>();
            services.AddScoped<IRepaymentsProviderFactory, RepaymentsProviderFactory>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IRepaymentRepository, RepaymentsRepository>();
            services.AddScoped<IBlacklistRepository, BlacklistRepository>();
            services.AddScoped<IRepaymentValidator, RepaymentValidator>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                // Use connection string from file.
                var connStr = config.GetConnectionString("DefaultConnection");
                options.UseSqlite(connStr);
            });

            return services;
        }
    }
}
