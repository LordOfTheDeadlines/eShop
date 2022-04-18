using Catalog.API.Data.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryAssortment>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoryAssortment>>> GetCategories()
        {
            var categories = await _repository.GetCatalog();
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryAssortment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryAssortment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryAssortment>> GetCategoryAssortment(int id)
        {
            var categoryAssortment = await _repository.GetCategoryAssortment(id);

            if (categoryAssortment == null)
            {
                _logger.LogError($"Category with id: {id}, not found.");
                return NotFound();
            }

            return Ok(categoryAssortment);
        }


        [HttpGet("Product/{id}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }
    }
}
