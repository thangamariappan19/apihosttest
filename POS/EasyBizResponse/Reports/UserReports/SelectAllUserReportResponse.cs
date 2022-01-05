using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Reports.UserReports
{
    [DataContract]
    [Serializable]
    public class SelectAllUserReportResponse :BaseResponseType
    {
        [DataMember] 
        public List<UserReport> UserReportList { get; set; }
    }
}
