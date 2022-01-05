using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Reports
{
    [DataContract]
    [Serializable]
    public class CommonReportRequest : BaseRequestType
    {
        [DataMember]
        public int MODE { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string StoreCode { get; set; }

        [DataMember]
        public int StockReturnID { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StockReceiptID { get; set; }
    }
}
