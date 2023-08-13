using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using App.CrossCutting.Settings.Options;
using App.AplicationCore.Interfaces;
using App.AplicationCore.Services;

namespace App.CrossCutting.Ioc
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {
        public static void SetupIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterCoreServices();
            services.RegisterOptionsSettings(configuration);

        }

        private static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IInvestmentCalculatorService, InvestmentCalculatorService>();
        }

        private static void RegisterOptionsSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var feeOptions = new FeePercentageOptions();
            configuration.GetSection("FeePercentage").Bind(feeOptions);
            services.Configure<FeePercentageOptions>(configuration.GetSection("FeePercentage"));
            services.AddSingleton(feeOptions);

            var taxOptions = new TaxPercentageOptions();
            configuration.GetSection("TaxPercentageTable").Bind(feeOptions);
            services.Configure<TaxPercentageOptions>(configuration.GetSection("TaxPercentageTable"));
            services.AddSingleton(feeOptions);
        }

    }
}
