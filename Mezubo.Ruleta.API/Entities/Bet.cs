using System;
using Mezubo.Roulette.API.Enums;

namespace Mezubo.Roulette.API.Entities
{
    public class Bet
    {
        public Guid RouletteId { get; set; }
        public int? NumbertoBet { get; set; }
        public BetColor Color { get; set; }
        public double MoneytoBet { get; set; }
        public string clientId { get; set; }
        public bool IsWinner { get; set; }
        public BetType Type { get; set; }
        public double Prize { get; set; }
    }
}
