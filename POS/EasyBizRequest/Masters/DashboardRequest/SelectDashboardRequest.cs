using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.Common;

namespace EasyBizRequest.Masters.DashboardRequest
{
    [DataContract]
    [Serializable]
    public class SelectDashboardRequest : BaseRequestType
    {
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public string report_type { get; set; }
        [DataMember]
        public int country_id { get; set; }

    }
}