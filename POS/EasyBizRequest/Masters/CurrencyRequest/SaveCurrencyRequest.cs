using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CurrencyRequest
{
    [Serializable]
    [DataContract]
    public class SaveCurrencyRequest : BaseRequestType
    {
        [DataMember]
        public CurrencyMaster CurrencyMasterData { get; set; }
    }
}
