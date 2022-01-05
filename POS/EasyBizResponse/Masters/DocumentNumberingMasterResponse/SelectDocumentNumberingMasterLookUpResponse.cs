using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DocumentNumberingMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectDocumentNumberingMasterLookUpResponse:BaseResponseType
    {

        [DataMember]
        public List<DocumentNumberingMaster> DocumentNumberingMasterList { get; set; }
    }
}
