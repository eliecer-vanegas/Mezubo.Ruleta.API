using System;
using System.Collections.Generic;

namespace Mezubo.Roulette.API.Entities
{
    public class Roulette
    {
        public Roulette()
        {
            RouletteId = Guid.NewGuid();
            BetList = new List<Bet>();
        }
        public Guid RouletteId { get; set; }
        public bool IsOpen { get; set; }

        public List<Bet> BetList
        {
            get; set;
        }

    }
}
