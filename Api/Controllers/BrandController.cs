using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timweb.Api.Services;
using Timweb.Models;

namespace Timweb.Api.Controllers
{
    /// <summary>
    /// Company brand controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IDatabase _db;
        private readonly ILogger<BrandController> _logger;

        public BrandController(ILogger<BrandController> logger, IDatabase db)
        {
            _logger = logger;
            _db = db;
        }

        /// <summary>
        /// Retrieves company brands from the database
        /// </summary>
        /// <returns>Brands</returns>
        [HttpGet]
        public async Task<IEnumerable<Brand>> Get()
        {
            return await _db.QueryDb<Brand>(Sql.SelectBrands);
        }
    }
}