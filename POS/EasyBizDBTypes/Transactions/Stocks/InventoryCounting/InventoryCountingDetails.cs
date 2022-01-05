using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.InventoryCounting
{
    [DataContract]
    [Serializable]
    public class InventoryCountingDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }        
        [DataMember]
        public int InventoryCountingID { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string RFID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int SystemQuantity { get; set; }
        [DataMember]
        public int PhysicalQuantity { get; set; }
         [DataMember]
        public int DifferenceQuantity { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        
    }
}
