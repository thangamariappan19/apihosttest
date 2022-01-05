using EasyBizDBTypes.Common;
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
    public class BinLogTypes
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string RFID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
        [DataMember]
        public DateTime CreateOn { get; set; }
        [DataMember]
        public DateTime UpdateOn { get; set; }
        [DataMember]
        public int BinID { get; set; }
        [DataMember]
        public string BinCode { get; set; }
        [DataMember]
        public string BinSubLevelCode { get; set; }
    }
}
