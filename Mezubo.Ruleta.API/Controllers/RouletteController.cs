using System;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Mezubo.Roulette.API.Messages.Request;
using Mezubo.Roulette.API.Services;

namespace Mezubo.Roulette.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly RouletteService _rouletteService;

        public RouletteController(IDatabase database)
        {
            _rouletteService = new RouletteService(database);
        }


        [HttpPost]
        [Route("CreateRoulette")]
        public ActionResult<Guid> CreateRoulette()
        {
            var newRoulette = _rouletteService.CreateRoulette();
            return Ok(newRoulette);
        }

        [HttpPut]
        [Route("OpenRoulette")]
        public ActionResult<bool> OpenRoulette([FromBody] OpenRouletteRequest openRoulette)
        {
                
            bool rouletteExist = _rouletteService.ExistRoulette(openRoulette.RoulettetoOpen);
            if (!rouletteExist)
                return NotFound();
            else
            {
                _rouletteService.OpenRoulette(openRoulette);
            }
               
            return Ok(true);
        }

        [HttpGet()]
        [Route("GetRoulettes")]
        public ActionResult GetRoulettes()
        {
            var roulettesList = _rouletteService.GetAllRoulettes();
          
            if (roulettesList == null)
                return NotFound();

            return Ok(roulettesList);
        }
    }
}
