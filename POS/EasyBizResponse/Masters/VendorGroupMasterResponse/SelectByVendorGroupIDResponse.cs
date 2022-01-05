using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.VendorGroupMasterResponse
{
    [DataContract]
    [Serializable]
  public class SelectByVendorGroupIDResponse : BaseResponseType
    {
        [DataMember]
        public VendorGroupMaster VendorGroupRecord { get; set; }
    }
}
