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
    public class SalesExchangeTransactionResponse : BaseResponseType
    {
        [DataMember]
        public List<SalesExchangeHeaderTransaction> SalesExchangeHeaderList { get; set; }
        [DataMember]
        public List<SalesExchangeDetailTransaction> SalesExchangeDetailsTransactionList { get; set; }
        [DataMember]
        public List<SalesExchangeWithTransaction> SalesExchangeWithTransactionList { get; set; }
        [DataMember]
        public String StoreName { get; set; }
        [DataMember]
        public String ExchangeNumber { get; set; }
        [DataMember]
        public DataTable ReportDataTable { get; set; }
        [DataMember]
        public DataSet ReportDataSet { get; set; }
    }
}
