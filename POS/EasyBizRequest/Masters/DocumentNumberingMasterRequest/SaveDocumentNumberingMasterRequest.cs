using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DocumentNumberingMasterRequest
{
    [Serializable]
    [DataContract]
    public class SaveDocumentNumberingMasterRequest:BaseRequestType
    {
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StateID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public DocumentNumberingMaster DocumentNumberingMasterRecord { get; set; }

        [DataMember]
        public List<DocumentNumberingDetails> DocumentNumberingDetailsList { get; set; }
    }
}
