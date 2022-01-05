using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    public class StockAdjustmentHeaderTransaction
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public String DocumentDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }

    }
}
