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
    public class SelectXReportByDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<XreportTypes> XReportList { get; set; }
        [DataMember]
        public List<XSubreportTypes> XSubReportList { get; set; }
    }
}
