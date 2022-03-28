using Admin.API.Data.Entites;
using Admin.API.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize("ClientIdPolicy")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryRepository repository, ILogger<CategoryController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repository.GetCategories();
            return Ok(categories);
        }
        
        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _repository.GetCategory(id);

            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, not found.");
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            await _repository.CreateCategory(category);

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            return Ok(await _repository.UpdateCategory(category));
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            return Ok(await _repository.DeleteCategory(id));
        }
    }
}
