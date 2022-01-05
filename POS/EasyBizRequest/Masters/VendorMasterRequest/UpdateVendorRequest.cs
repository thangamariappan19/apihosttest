using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.VendorMasterRequest
{
    [DataContract]
    [Serializable]
   public class UpdateVendorRequest : BaseRequestType
    {
        public VendorMaster VendorRecord { get; set; }
    }
}
