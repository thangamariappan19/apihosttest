using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubBrandMasterRequest
{
    [DataContract]
    [Serializable]
    
   public class SaveSubBrandRequest : BaseRequestType
    {
        [DataMember]
        public SubBrandMaster SubBrandRecord { get; set; }
        [DataMember]
        public List<SubBrandMaster> SubBrandlist { get; set; }
    }
}
