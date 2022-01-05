using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS.SalesReturn
{
    [DataContract]
    [Serializable]
    public class SalesReturnDetail : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SalesReturnID { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public int InvoiceDetailID { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public int ReturnQty { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }
        [DataMember]
        public int FromCountryID { get; set; }
        [DataMember]
        public int FromStoreID { get; set; }
        [DataMember]
        public bool IsDataSyncToCountryServer { get; set; }
        [DataMember]
        public bool IsDataSyncToMainServer { get; set; }
        [DataMember]
        public DateTime CountryServerSyncTime { get; set; }
        [DataMember]
        public DateTime MainServerSyncTime { get; set; }
        [DataMember]
        public string SyncFailedReason { get; set; }
        [DataMember]
        public int SoldQty { get; set; }  // For Validation
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int PosID { get; set; }
        [DataMember]
        public int CashierID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string PosCode { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public int TaxID { get; set; }
        [DataMember]
        public Decimal TaxAmount { get; set; }
        [DataMember]
        public string Tag_Id { get; set; }
    }
}
