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
    public class SelectAllUserReportRequest :BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ReportName { get; set; }
    }
}
