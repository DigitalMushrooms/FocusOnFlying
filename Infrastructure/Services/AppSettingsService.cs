using FocusOnFlying.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace FocusOnFlying.WebUI.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        public string FocusOnFlyingConnectionString => PobierzConnectionString(nameof(FocusOnFlyingConnectionString));

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
    }
}
