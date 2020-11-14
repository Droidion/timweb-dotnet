using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sentry;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Brand>>> Get([FromServices] IHub sentry)
        {
            _logger.LogInformation("GET Brand");
            try
            {
                return await _db.QueryDb<Brand>(Sql.SelectBrands);
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return BadRequest(e.Message);
            }
            
        }
    }
}