using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StoreMasterRequest
{
   public class StoreBrandMapRequest : BaseRequestType
    {

       [DataMember]
       public string StoreIDs;
    }
}
