using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Contracts;
using Marketplace.Models.Constants;
using Marketplace.Models.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.Api.Controllers.Stock
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IDomainService<int, ProductCategoryDto> _domainService;

        public ProductCategoryController(IDomainService<int, ProductCategoryDto> domainService)
        {
            _domainService = domainService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> Get()
        {
            var response = await _domainService.FindAsync();
            return response.IsNullOrEmpty()
                ? NotFound()
                : Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductCategoryDto>> Get(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int id)
        {
            var response = await _domainService.FindAsync(id);
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpPost]
        public Task<ProductCategoryDto> Create(ProductCategoryDto model)
        {
            return _domainService.CreateAsync(model);
        }

        [HttpPut("{id}")]
        public Task<ProductCategoryDto> Update(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int id,
            ProductCategoryDto model)
        {
            return _domainService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductCategoryDto>> Delete(
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
