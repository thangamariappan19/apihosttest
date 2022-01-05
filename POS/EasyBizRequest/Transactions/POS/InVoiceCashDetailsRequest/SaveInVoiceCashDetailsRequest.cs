using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest
{
    [DataContract]
    [Serializable]
   public class SaveInVoiceCashDetailsRequest:BaseRequestType
    {
        [DataMember]
        public InVoiceCashDetails InVoiceCashDetailsData { get; set; }

        
    }
}
