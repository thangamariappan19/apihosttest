using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BrandMasterRequest
{
    [DataContract]
    [Serializable]
   public class UpdateBrandRequest : BaseRequestType
    {
        [DataMember]
        public BrandMaster BrandRecord { get; set; }
    }
}
