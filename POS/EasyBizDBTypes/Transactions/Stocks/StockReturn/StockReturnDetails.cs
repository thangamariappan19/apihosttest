using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockReturn
{
    [DataContract]
    [Serializable]
    public class StockReturnDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StockReturnDetailID { get; set; }
        [DataMember]
        public int HeaderID { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int SKUID { get; set; }        
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SKUName { get; set; }
        [DataMember]
        public int FromStoreID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public string Size { get; set; }
        //[DataMember]
        //public List<SKUMasterTypes> SKUMasterList { get; set; }
        [DataMember]
        public int StockQty { get; set; }
        [DataMember]
        public int ExistingQty { get; set; }
        [DataMember]
        public string Tag_Id { get; set; }
        [DataMember]
        public string BinCode { get; set; }
    }
}
