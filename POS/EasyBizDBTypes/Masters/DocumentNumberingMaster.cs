using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class DocumentNumberingMaster : BaseType
    {

        [DataMember]
        public int ID { get; set; }

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
        public string StateName { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string PosName { get; set; }

        [DataMember]
        public string StoreName { get; set; }


        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public List<DocumentNumberingDetails> DocumentNumberingDetails { get; set; }

        [DataMember]
        public string MaxDate { get; set; }

        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StateCode { get; set; }
        [DataMember]
        public string PosCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        
    }
}
