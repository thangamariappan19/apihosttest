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
    public class StockReceiptTransactionResponse : BaseResponseType
    {
        [DataMember]
        public List<StockReceiptHeaderTransaction> StockReceiptHeaderTransactionList { get; set; }
       
        [DataMember]
        public List<StockReceiptDetailTransaction> StockReceiptDetailsTransactionList { get; set; }
        
        [DataMember]
        public string storename { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string ReceiptNumber { get; set; }

        [DataMember]
        public String ID { get; set; }

        [DataMember]
        public DataTable ReportDataTable { get; set; }

        [DataMember]
        public DataSet ReportDataSet { get; set; }
    }
}
