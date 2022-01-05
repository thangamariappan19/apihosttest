using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CustomerMasterRequest
{

    [DataContract]
    [Serializable]
    public class SaveCustomerMasterRequest : BaseRequestType
    {
        [DataMember]
        public CustomerMaster CustomerMasterData { get; set; }
        public long RunningNo { get; set; }
        [DataMember]
        public long DocumentNumberingID { get; set; }
    }
   
}
