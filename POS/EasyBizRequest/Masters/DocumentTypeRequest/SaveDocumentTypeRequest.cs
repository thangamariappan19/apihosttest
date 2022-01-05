using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DocumentTypeRequest
{
    [DataContract]
   [Serializable]
   public class SaveDocumentTypeRequest:BaseRequestType
    {
        [DataMember]
        public DocumentTypes DocumentTypeRecord { get; set; }
    }
}
