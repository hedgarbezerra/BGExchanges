using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGExchanges.Services.Model
{
    public class ExchangeRate
    {
        public DateTime time { get; set; }
        public string asset_id_base { get; set; }
        public string asset_id_quote { get; set; }
        public float rate { get; set; }
    }

}
