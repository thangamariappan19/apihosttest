using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleMasterRequest
{
    [DataContract]
    [Serializable]
   public class StyleCodeGeneratingRequest : BaseRequestType
    {
         [DataMember]
      public  string StyleCode { get; set; }
    }
}
