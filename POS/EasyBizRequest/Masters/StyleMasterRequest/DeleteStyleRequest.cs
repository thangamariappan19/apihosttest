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
   public class DeleteStyleRequest:BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int BrandID { get; set; }
    }
}
