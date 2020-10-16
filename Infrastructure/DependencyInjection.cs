using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb;
using FocusOnFlying.WebUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FocusOnFlying.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAppSettingsService, AppSettingsService>();
            services.AddDbContext<IFocusOnFlyingContext, FocusOnFlyingContext>();

            return services;
        }
    }
}
