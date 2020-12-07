using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using Mezubo.Roulette.API.Entities;

namespace Mezubo.Roulette.API.Helper
{
    public class RedisHelper
    {
        private const string RouletteTable = "roulettes";
        private readonly IDatabase _database;

        public RedisHelper(IDatabase database)
        {
            _database = database;
        }

        public List<Entities.Roulette> GetRoulettes()
        {
            List<Entities.Roulette> rouletteList = null;
            string rouletes = _database.StringGet(RouletteTable);

            if (string.IsNullOrEmpty(rouletes))
            {
                rouletteList = new List<Entities.Roulette>();
            }
            else
            {
                rouletteList = JsonConvert.DeserializeObject<List<Entities.Roulette>>(rouletes);
            }
            return rouletteList;
        }

        public Entities.Roulette CreateRoulete()
        {
            var newRoulette = new Entities.Roulette();
            SaveRoulete(newRoulette);
            return newRoulette;
        }

        public void CreateBet(Entities.Bet bet)
        {
            var roulettes = GetRoulettes();
            Entities.Roulette currentRoulette = roulettes.Find(x => x.RouletteId == bet.RouletteId);
            currentRoulette.BetList.Add(bet);
            _database.StringSet(RouletteTable, JsonConvert.SerializeObject(roulettes));
        }

        public void SaveRoulete(Entities.Roulette roulette)
        {
            var roulettes = GetRoulettes();
            roulettes.Add(roulette);
            _database.StringSet(RouletteTable, JsonConvert.SerializeObject(roulettes));
        }

        public void UpdateRoulette(Entities.Roulette roulette)
        {
            var roulettes = GetRoulettes();
            var currentRoulette = roulettes.Find(x => x.RouletteId == roulette.RouletteId);

            if (currentRoulette != null)
            {
                currentRoulette.IsOpen = roulette.IsOpen;
            }

            _database.StringSet(RouletteTable, JsonConvert.SerializeObject(roulettes));
        }

        public Entities.Roulette FindRoulette(Guid rouletteId)
        {
            var roulettes = GetRoulettes();
            var result = roulettes.Find(x => x.RouletteId == rouletteId);
            return result;
        }

        public Entities.Roulette CloseBet(Guid rouletteId)
        {
            Random random = new Random();
            int winnerNumber = random.Next(0, 16);
            bool isEvenNumber = winnerNumber % 2 == 0;
            var roulettes = GetRoulettes();
            var currentRoulette = roulettes.Find(x => x.RouletteId == rouletteId);
            currentRoulette.IsOpen = false;
            SetWinnerByNumber(winnerNumber, currentRoulette);
            var winnersByColor = currentRoulette.BetList.FindAll(x => x.Type == Enums.BetType.Color);
            winnersByColor = SetWinnerByColor(isEvenNumber, winnersByColor);
            _database.StringSet(RouletteTable, JsonConvert.SerializeObject(roulettes));
            return currentRoulette;
        }

        private void SetWinnerByNumber(int winnerNumber, Entities.Roulette currentRoulette)
        {
            var winnersByNumber = currentRoulette.BetList.FindAll(x => x.Type == Enums.BetType.Number && x.NumbertoBet == winnerNumber);
            if (winnersByNumber != null)
                winnersByNumber.ForEach(x => { x.IsWinner = true; x.Prize = x.MoneytoBet * 5; });
        }

        private List<Bet> SetWinnerByColor(bool isEvenNumber, List<Bet> winnersByColor)
        {
            if (winnersByColor != null)
            {
                if (isEvenNumber)
                {
                    winnersByColor = winnersByColor.FindAll(x => x.Color == Enums.BetColor.Red);
                }
                else
                {
                    winnersByColor = winnersByColor.FindAll(x => x.Color == Enums.BetColor.Black);
                }
                winnersByColor.ForEach(x => { x.IsWinner = true; x.Prize = x.MoneytoBet * 1.8; });
            }

            return winnersByColor;
        }
    }
}
