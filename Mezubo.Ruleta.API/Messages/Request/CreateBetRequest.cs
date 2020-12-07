using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Mezubo.Roulette.API.Enums;

namespace Mezubo.Roulette.API.Messages.Request
{
    public class CreateBetRequest
    {
        [Required]
        public Guid RouletteId { get; set; }
        [Range(0, 16)]
        public int? NumbertoBet { get; set; }
        public BetColor Color { get; set; }
        [Range(1, 10000)]
        public double MoneytoBet { get; set; }
        [Required]
        public BetType TypeofBet{get;set;}
    }
}
