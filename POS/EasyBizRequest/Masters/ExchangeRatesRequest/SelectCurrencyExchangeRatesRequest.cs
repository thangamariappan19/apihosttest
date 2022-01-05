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
    public class SelectCurrecnyExchangeRatesRequest : BaseRequestType
    {
        [DataMember]
        public DateTime Exchangedate { get; set; }
    }
}
