using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Masters;

namespace EasyBizResponse.Masters.DocumentNumberingMasterResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllDocumentNumberingMasterResponse : BaseResponseType
    {
        [DataMember]
        public List<DocumentNumberingMaster> DocumentNumberingMasterList { get; set; }
    }
}
