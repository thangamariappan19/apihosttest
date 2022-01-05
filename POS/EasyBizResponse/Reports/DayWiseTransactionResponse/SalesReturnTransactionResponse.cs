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
   public class SalesReturnTransactionResponse : BaseResponseType
    {
        [DataMember]
        public List<SalesReturnHeaderTransaction> SalesReturnHeaderTransactionList { get; set; }

        [DataMember]
        public List<SalesReturnDetailTransaction> SalesReturnDetailsTransactionList { get; set; }

        [DataMember]
        public String InvStoreName { get; set; }

        [DataMember]
        public String InvFromDate { get; set; }

        [DataMember]
        public String InvToDate { get; set; }

        [DataMember]
        public String InvDStoreName { get; set; }

        [DataMember]
        public String InvDInvNumber { get; set; }

        [DataMember]
        public DataTable ReportDataTable { get; set; }

        [DataMember]
        public DataSet ReportDataSet { get; set; }
    }
}
