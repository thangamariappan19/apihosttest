using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockStaging
{
    [DataContract]
    [Serializable]
    public class StockAdjustmentDetails
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SAHID { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }   
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public int SystemQuantity { get; set; }
        [DataMember]
        public int PhysicalQuantity { get; set; }
        [DataMember]
        public int AdjustableQuantity { get; set; }
        [DataMember]
        public int BinID { get; set; }
        [DataMember]
        public string BinCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string BinSubLevelCode { get; set; }
        
    }
}
