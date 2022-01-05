using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DocumentNumberingMasterResponse
{
    [Serializable]
    [DataContract]
    public class SelectDocumentNumberingDetailsResponse:BaseResponseType
    {
        public List<DocumentNumberingDetails> DocumentNumberingDetailsList { get; set; }
        public DocumentNumberingDetails DocumentNumberingDetailsRecord { get; set; }
    }
}
