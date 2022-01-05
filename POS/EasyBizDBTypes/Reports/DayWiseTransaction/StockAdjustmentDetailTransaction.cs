using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    [DataContract]
    [Serializable]
    public class StockAdjustmentDetailTransaction : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string SystemQuantity { get; set; }
        [DataMember]
        public string PhysicalQuantity { get; set; }
        [DataMember]
        public string DifferenceQty { get; set; }
  
    }
}
