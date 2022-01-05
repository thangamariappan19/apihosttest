using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EasyBizResponse.Reports.DayWiseTransactionResponse
{
    [DataContract]
    [Serializable]
    public class StockAdjustmentTransactionResponse : BaseResponseType
    {
        [DataMember]
        public List<StockAdjustmentHeaderTransaction> StockAdjustmentHeaderTransactionList { get; set; }

        [DataMember]
        public List<StockAdjustmentDetailTransaction> StockAdjustmentDetailsTransactionList { get; set; }

        [DataMember]
        public string storename { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string AdjustmentNumber { get; set; }

        [DataMember]
        public String ID { get; set; }


        public DataTable ReportDataTable { get; set; }
    }
}
