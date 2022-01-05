using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.ExchangeRatesResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllExchangeRatesResponse : BaseResponseType
    {
        public List<ExchangeRates> ExchangeRatesList { get; set; }
    }
}
