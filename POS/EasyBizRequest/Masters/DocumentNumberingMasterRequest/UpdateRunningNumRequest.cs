using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DocumentNumberingMasterRequest
{
    [Serializable]
    [DataContract]
    public class UpdateRunningNumRequest : BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int DetailID { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public DocumentNumberingDetails RunningNumRecord { get; set; }
    }
}
