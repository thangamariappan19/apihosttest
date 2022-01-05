using EasyBizAbsDAL.Common;
using EasyBizRequest.DashBoardRequest;
using EasyBizRequest.Masters.DashboardRequest;
using EasyBizResponse.Masters.DashboardReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.DashBoard
{
    public abstract class BaseRegisterDashBoardDAL : BaseDAL
    {
        public abstract SelectDashboardResponse API_SelectBetweenDayDetails(SelectDashboardRequest requestData);
    }
}
