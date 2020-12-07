using Mezubo.Roulette.API.Helper;
using Mezubo.Roulette.API.Messages.Request;
using Mezubo.Roulette.API.Messages.Response;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mezubo.Roulette.API.Services
{
    public class BetServices
    {
        private readonly RedisHelper _redisHelper;
        public BetServices(IDatabase database)
        {
            _redisHelper = new RedisHelper(database);
        }

        public CreateBetResponse MakeBet(CreateBetRequest betRequest, string clientId)
        {
            CreateBetResponse response = new CreateBetResponse();

            var currentRoulette = _redisHelper.FindRoulette(betRequest.RouletteId);

            if (!currentRoulette.IsOpen)
            {
                response.Message = "The roulette is Closed";
                return response;
            }

            Entities.Bet newBet = new Entities.Bet();

            if (betRequest.TypeofBet == Enums.BetType.Color) {
                newBet.Color = betRequest.Color;
                newBet.NumbertoBet = null;
            }
            else
            {
                newBet.Color = Enums.BetColor.None;
                newBet.NumbertoBet = betRequest.NumbertoBet;
            }

            newBet.RouletteId = betRequest.RouletteId;
            newBet.Type = betRequest.TypeofBet;
            newBet.MoneytoBet = betRequest.MoneytoBet;

            _redisHelper.CreateBet(newBet);
            response.Successful = true;
            response.Message = "Success";

            return response;
        }

        public Entities.Roulette CloseBet(Guid rouletteId)
        {
            Entities.Roulette result = _redisHelper.CloseBet(rouletteId);
            return result;
        }

    }
}
