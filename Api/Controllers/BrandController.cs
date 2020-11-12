using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Timweb.Models;

namespace Timweb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IConfiguration _configuration;
        
        public BrandController(ILogger<BrandController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Brand>> Get()
        {
            await using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("Postgres"))) 
            {
                await conn.OpenAsync();
                return conn.Query<Brand>("select * from brands;");
            }
        }
    }
}