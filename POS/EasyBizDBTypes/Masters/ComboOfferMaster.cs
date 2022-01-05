using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class ComboOfferMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string ProductBarcode { get; set; }
        [DataMember]
        public string ProductSKUCode { get; set; }
        [DataMember]
        public string ProductStylecode { get; set; }
        [DataMember]
        public List<ComboOfferDetails> ComboOfferDetailsList { get; set; }
        public List<TransactionLog> TransactionLogList { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ComboOfferDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public string Barcode { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public string Stylecode { get; set; }
    }

    [DataContract]
    [Serializable]
    public class CPOStyleDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ProductstyleCode { get; set; }
        [DataMember]
        public int ColorID { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public int ScaleID { get; set; }
        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public int RowCount { get; set; }
    }
}
