using FocusOnFlying.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace FocusOnFlying.WebUI.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        public string FocusOnFlyingConnectionString => PobierzConnectionString(nameof(FocusOnFlyingConnectionString));
        public string IdentityProviderAddress => PobierzWartosc(nameof(IdentityProviderAddress));
        public string IdentityProviderUsersPath => PobierzWartosc(nameof(IdentityProviderUsersPath));

        private readonly IConfiguration _configuration;

        public AppSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string PobierzConnectionString(string nazwa)
        {
            return _configuration.GetConnectionString(nazwa) ??
                throw new NullReferenceException($"Nie znaleziono connection string o nazwie {nazwa}");
        }

        private string PobierzWartosc(string nazwa)
        {
            return _configuration[nazwa] ??
                throw new NullReferenceException($"Nie znaleziono wartości o nazwie {nazwa}");
        }
    }
}
