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
        /// <returns>HTTP response</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Brand>>> Get(
            [FromServices] IHub sentry,
            [FromQuery(Name = "limit")] string? limit,
            [FromQuery(Name = "skip")] string? skip)
        {
            _logger.LogInformation("GET Brand");
            try
            {
                return await _db.QueryDb<Brand>(BrandSql.Select(limit, skip));
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///     Inserts new brand
        /// </summary>
        /// <param name="sentry">Dependency injected Sentry client</param>
        /// <param name="brand">Brand DTO</param>
        /// <returns>Id of the created brand</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post(
            [FromServices] IHub sentry,
            [FromBody] Brand brand)
        {
            _logger.LogInformation("POST Brand");
            try
            {
                var id = await _db.InsertDb(BrandSql.Insert, brand);
                return StatusCode(201, id);
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///     Updates existing brand
        /// </summary>
        /// <param name="sentry">Dependency injected Sentry client</param>
        /// <param name="brand">Brand DTO</param>
        /// <returns>HTTP response</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(
            [FromServices] IHub sentry,
            [FromBody] Brand brand)
        {
            _logger.LogInformation("PUT Brand");
            try
            {
                var rowsAffected = await _db.ExecuteDb(BrandSql.Update, brand);
                if (rowsAffected == 1) return StatusCode(204);
                return StatusCode(400, "No update happened. Brand probably does not exist.");
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        ///     Deletes existing brand
        /// </summary>
        /// <param name="sentry">Dependency injected Sentry client</param>
        /// <param name="id">Id of the brand to delete</param>
        /// <returns>HTTP response</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(
            [FromServices] IHub sentry,
            int id)
        {
            _logger.LogInformation("DELETE Brand");
            try
            {
                var rowsAffected = await _db.ExecuteDb(BrandSql.Delete, new {Id = id});
                if (rowsAffected == 1) return StatusCode(204);
                return StatusCode(400, "No deletion happened. Brand probably does not exist.");
            }
            catch (Exception e)
            {
                sentry.CaptureException(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}