using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BrandMasterRequest
{
  public  class SelectBrandLookUpRequest : BaseRequestType
    {
      [DataMember]
       public int BrandID { get; set; }
      [DataMember]
      public int StoreID { get; set; } // For report purpose
      [DataMember]
      public string Type { get; set; } // For report purpose
    }
}
