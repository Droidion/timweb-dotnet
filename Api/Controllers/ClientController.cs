using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sentry;
using Timweb.Api.Services;
using Timweb.Api.Services.Sql;
using Timweb.Models;

namespace Timweb.Api.Controllers
{
    /// <summary>
    ///     Client controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IDatabase _db;
        private readonly ILogger<BrandController> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientController" /> class.
        /// </summary>
        /// <param name="logger">Dependency injected logger</param>
        /// <param name="db">Dependency injected interface for making DB queries</param>
        public ClientController(ILogger<BrandController> logger, IDatabase db)
        {
            _logger = logger;
            _db = db;
        }
        
        /// <summary>
        ///     Retrieves clients from the database
        /// </summary>
        /// <param name="sentry">Dependency injected Sentry client</param>
        /// <param name="limit">How many results to show</param>
        /// <param name="skip">How many results to skip</param>
        /// <returns>HTTP response</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Client>>> Get(
            [FromServices] IHub sentry,
            [FromQuery(Name = "limit")] string? limit,
            [FromQuery(Name = "skip")] string? skip)
        {
            _logger.LogInformation("GET Client");
            try
            {
                return await _db.QueryDb<Client, Brand, Client>(ClientSql.Select(limit, skip), (client, brand) =>
                {
                    client.Brand = brand;
                    return client;
                },
                "logo");
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}