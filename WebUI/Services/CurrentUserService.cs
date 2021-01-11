using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppSettingsService _appSettingsService;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor, 
            IHttpClientFactory httpClientFactory,
            IAppSettingsService appSettingsService)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _appSettingsService = appSettingsService;
        }

        public string Login => _httpContextAccessor.HttpContext?.User?.FindFirstValue("nickname");
        public string Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");

        public async Task<UserDto> PobierzInformacje()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, AdresHttp());

            HttpClient httpClient = _httpClientFactory.CreateClient();
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDto>>(json).Single(x => x.Username == Login);
        }

        private Uri AdresHttp()
        {
            Uri baseUri = new Uri(_appSettingsService.IdentityProviderAddress + _appSettingsService.IdentityProviderUsersPath);
            return baseUri;
        }
    }
}
