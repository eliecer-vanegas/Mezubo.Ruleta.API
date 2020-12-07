using Mezubo.Roulette.API.Helper;
using Mezubo.Roulette.API.Messages.Request;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mezubo.Roulette.API.Services
{
    public class RouletteService
    {
        private readonly RedisHelper _redisHelper;
        public RouletteService(IDatabase database)
        {
            _redisHelper = new RedisHelper(database);
        }

        public void OpenRoulette(OpenRouletteRequest openRoulette)
        {
            Entities.Roulette roulette = _redisHelper.FindRoulette(openRoulette.RoulettetoOpen);
            roulette.IsOpen = true;
            _redisHelper.UpdateRoulette(roulette);
        }

        public bool ExistRoulette(Guid rouletteToOpen)
        {
           var result = _redisHelper.FindRoulette(rouletteToOpen);
            return result != null;
        }

        public Entities.Roulette CreateRoulette()
        {
            return _redisHelper.CreateRoulete();
        }

        public List<Entities.Roulette> GetAllRoulettes()
        {
            return _redisHelper.GetRoulettes();
        }
    }
}
