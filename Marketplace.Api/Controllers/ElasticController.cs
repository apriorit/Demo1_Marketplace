using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Marketplace.Contracts;
using Marketplace.Data.Models;
using Marketplace.Integrations.ElasticSearch.Services;
using Marketplace.Models.Constants;
using Marketplace.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ElasticController : ControllerBase
    {
        private readonly IFilterableDomainService<int, ElasticEntity> _filterService;
        private readonly IDomainService<int, ElasticDto> _domainService;
        private readonly ILogger<ElasticDto> _logger;

        public ElasticController(IDomainService<int, ElasticDto> domainService, IFilterableDomainService<int, ElasticEntity> filterService, ILogger<ElasticDto> logger)
        {
            _logger = logger;
            _domainService = domainService;
            _filterService = filterService;
        }

        [HttpPost("/SendLogs")]
        public async Task<IActionResult> SendLogs()
        {
            try
            {
                await _domainService.FindAsync(-1);
            }
            catch (Exception ex)
            {
                _logger.LogError("SendLogs - Error", ex);
            }
            finally
            {
                _logger.LogCritical("SendLogs - Critical");
                _logger.LogInformation("SendLogs - Information");
                _logger.LogWarning("SendLogs - Warning");
                _logger.LogTrace("SendLogs - Trace");
                _logger.LogDebug("SendLogs - Debug");
            }
            return Ok();
        }

        /// <summary>
        /// Executes query:
        /// POST my_index/_search
        ///    "query": {
        ///        "bool": {
        ///            "filter": [
        ///                { "term": { "isactive": true }},
        ///                { "query_string": { "query": _query_ }}
        ///            ]
        ///        }
        ///    }
        /// Please see the syntax for "query" here: https://www.elastic.co/guide/en/elasticsearch/reference/current/query-dsl-query-string-query.html#query-string-syntax
        /// </summary>
        /// <param name="query"></param>
        /// <returns>matched documents</returns>
        [HttpGet("/Filter/{query}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<ElasticDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyCollection<ElasticDto>>> Get(string query)
        {
            var result = await _filterService.Filter(query);

            return result.IsNullOrEmpty()
                ? NotFound()
                : Ok(_domainService.EntityConverter.ConvertTo<IReadOnlyCollection<ElasticEntity>, IReadOnlyCollection<ElasticDto>>(result));
        }

        [HttpPost("/AddEntity")]
        public Task<ElasticDto> Post(ElasticDto model)
        {
            return _domainService.CreateAsync(model);
        }

        [HttpPut("/UpdateEntity/{id}")]
        public Task<ElasticDto> Update(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int id,
            ElasticDto model)
        {
            return _domainService.UpdateAsync(id, model);
        }

        [HttpDelete("/DeactivateEntity/{id}")]
        [ProducesResponseType(typeof(ElasticDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ElasticDto>> Delete(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int id)
        {
            var response = await _domainService.DeleteAsync(id);
            return response == null
                ? NotFound()
                : Ok(response);
        }
    }
}
