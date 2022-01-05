using EasyBizDBTypes.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.DashBoardRequest
{
    [DataContract]
    [Serializable]
   public class UpdateDashBoardRequest : BaseRequestType
    {
        [DataMember]
        public RegisterDashboard DashBoardReportsRecord { get; set; }
    }
}
