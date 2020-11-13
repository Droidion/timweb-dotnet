using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timweb.Api.Services;
using Timweb.Models;

namespace Timweb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IDatabase _db;
        
        public BrandController(ILogger<BrandController> logger, IDatabase db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Brand>> Get()
        {
            return await _db.QueryDb<Brand>(Sql.SelectBrands);
        }
    }
}