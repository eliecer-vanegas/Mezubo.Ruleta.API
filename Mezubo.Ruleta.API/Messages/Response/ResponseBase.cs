using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mezubo.Roulette.API.Messages.Response
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public bool Successful { get; set; }
    }
}
