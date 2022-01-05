using EasyBizDBTypes.Reports.DayWiseTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace EasyBizResponse.Reports.DayWiseTransactionResponse
{
    [DataContract]
    [Serializable]
    public class InvoiceHeaderTransactionReponse : BaseResponseType
    {
        [DataMember]
        public List<InvoiceHeaderTransaction> InvoiceHeaderTransactionList { get; set; }

        [DataMember]
        public List<InvoiceDetailTransaction> InvoiceDetailsTransactionList { get; set; }

        [DataMember]
        public List<InvoicePaymentTransaction> InvoicePaymentTransactionList { get; set; }

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
