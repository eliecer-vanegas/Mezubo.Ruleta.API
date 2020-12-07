using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mezubo.Roulette.API.Messages.Request
{
    public class CloseBetRequest
    {
        [Required]
        public Guid RoulettetoClose { get; set; }
    }
}
