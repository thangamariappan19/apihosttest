using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Reports.CurrentStockReportResponse
{
    [DataContract]
    [Serializable]
   public class CurrentStockReportResponse : BaseResponseType
    {
        [DataMember]
        public List<CurrentStockReport> CurrentStockReportList { get; set; }
    }
}
