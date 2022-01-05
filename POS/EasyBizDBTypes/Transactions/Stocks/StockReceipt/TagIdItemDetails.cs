using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.StockReceipt
{
    [DataContract]
    [Serializable]
    public class TagIdItemDetails
    {
        [DataMember]
        public long ID {get;set;}
        [DataMember]
        public string Tag_ID {get;set;}
        [DataMember]
        public string Itemcode {get;set;}
        [DataMember]
        public string Description { get;set;}
        [DataMember]
        public string Supplier_barcode { get;set;}
        [DataMember]
        public string Orjwan_Barcode { get;set;}
        [DataMember]
        public string Brand_code { get; set; }
        [DataMember]
        public string Design_code { get;set;}
        [DataMember]
        public string Department_code { get;set;}
        [DataMember]
        public string Product_code { get;set;}
        [DataMember]
        public string Color_code { get;set;}
        [DataMember]
        public string Size_code { get;set;}
        [DataMember]
        public string Color_Name { get;set;}
        [DataMember]
        public string Scale { get;set;}
        [DataMember]
        public string Season { get;set;}
        [DataMember]
        public string Theme { get;set;}
        [DataMember]
        public string Department { get;set;}
        [DataMember]
        public int Quantity { get;set;}
        [DataMember]
        public string DocumentNo { get;set;}
        [DataMember]
        public DateTime DocumentDate { get;set;}
        [DataMember]
        public string ItemStatus{ get;set;}
    }
}
