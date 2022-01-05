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
    public class StockReturnTransactionResponse : BaseResponseType
    {
        [DataMember]
        public List<StockReturnHeaderTransaction> StockReturnHeaderTransactionList { get; set; }

        [DataMember]
        public List<StockReturnDetailTransaction> StockReturnDetailsTransactionList { get; set; }

        [DataMember]
        public string storename { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public string ReturnNumber { get; set; }

        [DataMember]
        public String ID { get; set; }

       
        public DataTable ReportDataTable { get; set; }

  
    }
}
