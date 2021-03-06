using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DocumentTypeResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllDocumentTypeResponse:BaseResponseType
    {
        [DataMember]
        public List<DocumentTypes> DocumentTypeList { get; set; }
    }
}
