using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.LanguageRequest
{
    [DataContract]
    [Serializable]
   public class SelectByLanguageIDRequest:BaseRequestType
    {
        [DataMember]
        public long ID { get; set; }
    }
}
