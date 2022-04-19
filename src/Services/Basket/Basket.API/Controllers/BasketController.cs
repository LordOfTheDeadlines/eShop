using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService service, ILogger<BasketController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{userId}", Name = "GetBasket")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> GetBasket(int userId)
        {
            return await _service.GetBasket(userId);
        }

        [HttpGet("{userId}/{productId}", Name = "AddToBasket")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> AddToBasket(int userId, int productId)
        {
            return Ok(await _service.AddToBasket(userId,productId));
        }

        [HttpDelete("{userId}/{productId}", Name = "DeleteFromBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteFromBasket(int userId, int productId)
        {
            await _service.DeleteFromBasket(userId,productId);
            return Ok();
        }
    }
}
