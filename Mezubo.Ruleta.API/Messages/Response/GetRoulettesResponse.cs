using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mezubo.Roulette.API.Messages.Response
{
    public class GetRoulettesResponse 
    {
        public Guid RouletteId { get; set; }
        public bool IsOpen { get; set; }
    }
}
