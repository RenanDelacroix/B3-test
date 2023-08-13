using System.Diagnostics.CodeAnalysis;
using App.CrossCutting.Ioc;

namespace App.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IocConfiguration
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration) =>
            services.SetupIoC(configuration);
    }
}
