using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb;
using FocusOnFlying.Infrastructure.Services.PropertyMapping;
using FocusOnFlying.WebUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOnFlying.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<IFocusOnFlyingContext, FocusOnFlyingContext>();

            services.AddScoped<IAppSettingsService, AppSettingsService>();
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();

            return services;
        }
    }
}
