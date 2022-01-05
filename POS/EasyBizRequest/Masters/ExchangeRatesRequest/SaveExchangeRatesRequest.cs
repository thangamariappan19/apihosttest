using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ExchangeRatesRequest
{
    [DataContract]
    [Serializable]
    public class SaveExchangeRatesRequest : BaseRequestType
    {
        [DataMember]
        public DateTime ExchangeRatesDate { get; set; }
        [DataMember]       
       public List<ExchangeRates> ExchangeRateslist { get; set; }
    }
}
