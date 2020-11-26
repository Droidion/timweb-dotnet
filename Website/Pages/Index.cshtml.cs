using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Timweb.Models;

namespace Timweb.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        public IEnumerable<Brand> Brands { get; private set; } = new List<Brand>();

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            var urlBase = _configuration["ApiUrl"];
            var request = new HttpRequestMessage(HttpMethod.Get, urlBase + "brand");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory");
            request.Headers.Add("Authorization", "Bearer " + GenerateJwtToken());

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                Brands = await JsonSerializer.DeserializeAsync<IEnumerable<Brand>>(responseStream) ?? new List<Brand>();
            }
        }
        
        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(signingCredentials: credentials);
            var encoded = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine(encoded);
            return encoded;
        }
    }
}