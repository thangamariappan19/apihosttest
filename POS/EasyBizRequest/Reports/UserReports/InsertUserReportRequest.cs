using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Reports.UserReports
{
    [DataContract]
    [Serializable]
    public class InsertUserReportRequest :BaseRequestType
    {
        [DataMember]
        public UserReport UserReportRecord { get; set; }
    }
}
