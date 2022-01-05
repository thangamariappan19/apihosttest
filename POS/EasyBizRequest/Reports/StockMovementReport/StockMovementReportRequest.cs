using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Reports.StockMovementReport
{
    [DataContract]
    [Serializable]
   public  class StockMovementReportRequest : BaseRequestType
    {
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int StyleID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string PosNo { get; set; }
      
    }
}
