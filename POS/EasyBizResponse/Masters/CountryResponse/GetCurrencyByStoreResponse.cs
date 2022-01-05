using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CountryResponse
{
   public class GetCurrencyByStoreResponse : BaseResponseType
    {
        public CountryMaster CurrencyByStore { get; set; }

        public CountryMaster CurrencyData { get; set; }
    }
}
