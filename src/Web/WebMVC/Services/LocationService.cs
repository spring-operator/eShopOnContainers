﻿using Microsoft.eShopOnContainers.WebMVC;
using Microsoft.eShopOnContainers.WebMVC.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class LocationService : ILocationService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CampaignService> _logger;
        private readonly string _remoteServiceBaseUrl;

        public LocationService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<CampaignService> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.MarketingUrl}/api/v1/l/locations/";
        }

        public async Task CreateOrUpdateUserLocation(LocationDTO location)
        {
            var uri = API.Locations.CreateOrUpdateUserLocation(_remoteServiceBaseUrl);
            _logger.LogTrace("Making request to location api at {url}", uri);
            var locationContent = new StringContent(JsonConvert.SerializeObject(location), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, locationContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
