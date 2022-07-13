using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGExchanges.Services.Model
{
    public class ExchangeRates
    { 
        public string asset_id_base { get; set; }
        public ExchangeRate[] rates { get; set; }
    }

}
