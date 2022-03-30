﻿using Catalog.API.Data.Entities;
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

        [HttpGet("{id}", Name = "GetCategoryAssortment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryAssortment), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryAssortment>> GetProductById(int id)
        {
            var categoryAssortment = await _repository.GetCategoryAssortment(id);

            if (categoryAssortment == null)
            {
                _logger.LogError($"Category with id: {id}, not found.");
                return NotFound();
            }

            return Ok(categoryAssortment);
        }
    }
}