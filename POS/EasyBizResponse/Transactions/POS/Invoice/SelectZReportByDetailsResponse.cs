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
    public class SelectZReportByDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<ZReport> ZReportList { get; set; }
        [DataMember]
        public List<ZSubReport> ZReportList1 { get; set; }
        [DataMember]
        public List<Zreport2> Zreport2 { get; set; }
    }
}
