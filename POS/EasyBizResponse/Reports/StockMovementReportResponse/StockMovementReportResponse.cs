using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Reports.StockMovementReportResponse
{
    [DataContract]
    [Serializable]
   public class StockMovementReportResponse : BaseResponseType
    {
        [DataMember]
        public List<StockMovementReport> StockMovementReportList { get; set; }
    }
}
