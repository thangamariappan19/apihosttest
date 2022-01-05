using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Reports.DailySalesReport
{
    [DataContract]
    [Serializable]
   public class DailySalesReportRequest : BaseRequestType
   {
         [DataMember]
         public DateTime FromDate { get; set; }
         [DataMember]
         public DateTime ToDate { get; set; }
         [DataMember]
         public int CountryID { get; set; }
         [DataMember]
         public int StoreGroupID { get; set; }
         [DataMember]
         public int StoreID { get; set; }
         [DataMember]
         public int PosID { get; set; }
         [DataMember]
         public int StyleID { get; set; }
         [DataMember]
         public string StyleCode { get; set; }
        
         

    }
}
