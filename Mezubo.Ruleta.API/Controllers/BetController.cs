using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mezubo.Roulette.API.Messages.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Mezubo.Roulette.API.Services;
using Mezubo.Roulette.API.Messages.Response;

namespace Mezubo.Roulette.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly BetServices _betService;

        public BetController(IDatabase database)
        {
            _betService = new BetServices(database);
        }
     
        [HttpPut]
        [Route("CloseBets")]
        public IActionResult CloseBets([FromBody] CloseBetRequest closeBetRequest)
        {
            Entities.Roulette betResult = _betService.CloseBet(closeBetRequest.RoulettetoClose);

            return Ok(betResult);
        }

        [HttpPut]
        [Route("CreateBet")]
        public IActionResult CreateBet([FromBody] CreateBetRequest bet, [FromHeader] string clientId)
        {

            if (!string.IsNullOrEmpty(clientId))
            {
                CreateBetResponse response = _betService.MakeBet(bet, clientId);
                return Ok(response);
            }
            else
            {
                return BadRequest("You must enter a Client Id");
            }
        }
    }
}
