using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Timweb.Website.Services
{
    public interface IRequestMaker
    {
        Task<IEnumerable<T>> Get<T>(string endpoint);
    }

    public class RequestMaker : IRequestMaker
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        
        public RequestMaker(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }
        
        public async Task<IEnumerable<T>> Get<T>(string endpoint)
        {
            var urlBase = _configuration["ApiUrl"];
            var request = new HttpRequestMessage(HttpMethod.Get, urlBase + endpoint);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory");
            request.Headers.Add("Authorization", "Bearer " + GenerateJwtToken());

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(responseStream) ?? new List<T>();
            }

            throw new InvalidOperationException("Could not load data from API");
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