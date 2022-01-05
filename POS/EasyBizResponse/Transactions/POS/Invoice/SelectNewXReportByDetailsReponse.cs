using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class SelectNewXReportByDetailsReponse: BaseResponseType
    {
        [DataMember]
        public List<XReportHeaderTypes> XReportList { get; set; }
        [DataMember]
        public List<XReportDetailsTypes> XReportList1 { get; set; }
        [DataMember]
        public List<XReportSummaryTypes> XReportList2 { get; set; }

       
    }
}
