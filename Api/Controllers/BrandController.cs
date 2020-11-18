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
    ///     Company brand controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IDatabase _db;
        private readonly ILogger<BrandController> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BrandController" /> class.
        /// </summary>
        /// <param name="logger">Dependency injected logger</param>
        /// <param name="db">Dependency injected interface for making DB queries</param>
        public BrandController(ILogger<BrandController> logger, IDatabase db)
        {
            _logger = logger;
            _db = db;
        }

        /// <summary>
        ///     Retrieves company brands from the database
        /// </summary>
        /// <param name="sentry">Dependency injected Sentry client</param>
        /// <param name="limit">How many results to show</param>
        /// <param name="skip">How many results to skip</param>
        /// <returns>Company brands</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Brand>>> Get(
            [FromServices] IHub sentry,
            [FromQuery(Name = "limit")] string? limit,
            [FromQuery(Name = "skip")] string? skip)
        {
            _logger.LogInformation("GET Brand");
            try
            {
                return await _db.QueryDb<Brand>(Sql.SelectBrands(limit, skip));
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdContainer>> Post(
            [FromServices] IHub sentry,
            [FromBody] Brand brand)
        {
            _logger.LogInformation("GET Brand");
            try
            {
                return await _db.InsertDb(brand, Sql.InsertBrand());
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return BadRequest(e.Message);
            }
        }
    }
}