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
    public class SelectByIDExchangeRatesResponse : BaseResponseType
    {
        [DataMember]
        public ExchangeRates ExchangeRatesRecord { get; set; }
        [DataMember]
        public List<ExchangeRates> ExchangeRatesList = new List<ExchangeRates>();
    }
}
