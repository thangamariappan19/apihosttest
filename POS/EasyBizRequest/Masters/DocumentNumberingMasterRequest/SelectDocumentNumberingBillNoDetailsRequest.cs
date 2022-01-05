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
    public class SelectDocumentNumberingBillNoDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StateID { get; set; }
        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int POSID { get; set; }

        [DataMember]
        public string StoreCode { get; set; }

        [DataMember]
        public string POSCode { get; set; }
    }
}
