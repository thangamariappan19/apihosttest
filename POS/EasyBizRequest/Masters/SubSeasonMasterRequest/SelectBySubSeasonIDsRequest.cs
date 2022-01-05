using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubSeasonMasterRequest
{
      [DataContract]
    [Serializable]
    public class SelectBySubSeasonIDsRequest:BaseRequestType
    {
          [DataMember]
          public int IDs { get; set; }
    }
}
