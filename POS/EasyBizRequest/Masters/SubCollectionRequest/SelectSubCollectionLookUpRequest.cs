using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SubCollectionRequest
{
  

    [DataContract]
    [Serializable]

    public class SelectSubCollectionLookUpRequest : BaseRequestType
    {
        [DataMember]
        public int CollectionID { get; set; }
        [DataMember]
        public string CollectionName { get; set; }
    }
}
