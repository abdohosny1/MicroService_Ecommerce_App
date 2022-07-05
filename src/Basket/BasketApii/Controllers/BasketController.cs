using AutoMapper;
using BasketApii.Entities;
using BasketApii.Repositories.interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BasketApii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IBasketRepository _repository;
      //  private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<BasketController> _logger;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository,  IMapper mapper, ILogger<BasketController> logger)
        {
            _repository = repository;
        //    _eventBus = eventBus;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{userName}",Name ="Get Basket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket= await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));

        }


        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCart>> updateBasket([FromBody] ShoppingCart shoppingCart)
        {
            var basket =await _repository.UpdateBasket(shoppingCart);
            return Ok(basket);

        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get total price of the basket
            // remove the basket 
            // send checkout event to rabbitMq 

            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                _logger.LogError("Basket not exist with this user : {EventId}", basketCheckout.UserName);
                return BadRequest();
            }

            await _repository.DeleteBasket(basketCheckout.UserName);

            // var basketRemoved = await _repository.DeleteBasket(basketCheckout.UserName);
            //if (!basketRemoved)
            //{
            //    _logger.LogError("Basket can not deleted");
            //    return BadRequest();
            //}

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process

          ///  var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
          //  eventMessage.RequestId = Guid.NewGuid();
           // eventMessage.TotalPrice = basket.TotalPrice;

            try
            {
              //  _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.RequestId, "Basket");
                throw;
            }

            return Accepted();
        }

    }
}
