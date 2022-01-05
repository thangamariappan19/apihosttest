using EasyBizDBTypes.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.DashBoard
{
    [DataContract]
    [Serializable]
    public class SelectAllRegisterDashBoardResponse : BaseResponseType
    {
        [DataMember]
        public List<RegisterDashboard> DashBoardReportsList { get; set; }
    }
}
