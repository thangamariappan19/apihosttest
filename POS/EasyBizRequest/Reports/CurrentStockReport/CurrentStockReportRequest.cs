using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Reports.CurrentStockReport
{

    [DataContract]
    [Serializable]
  public class CurrentStockReportRequest : BaseRequestType
    {
         [DataMember]
         public int CountryID { get; set; }
         [DataMember]
         public string BrandName { get; set; }
         [DataMember]
         public int StoreID { get; set; }
         [DataMember]
         public int StyleID { get; set; }
         [DataMember]
         public string StyleCode { get; set; }
    }
}
