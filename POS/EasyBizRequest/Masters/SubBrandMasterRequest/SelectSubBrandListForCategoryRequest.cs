using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubBrandMasterRequest
{
   public class SelectSubBrandListForCategoryRequest : BaseRequestType
    {
       [DataMember]
       public long BrandID { get; set; }
    }
}
