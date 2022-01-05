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
    public class SelectNewZReportByDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<NewZReportDetails1> ZReportList1 { get; set; }
        [DataMember]
        public List<NewZReportDetails2> ZReportList2 { get; set; }
        [DataMember]
        public List<NewZReportDetails3> ZReportList3 { get; set; }
    }
}
