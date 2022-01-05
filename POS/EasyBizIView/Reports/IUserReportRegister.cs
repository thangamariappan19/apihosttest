using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface IUserReportRegister : IBaseView
    {
        UserReport UserReportRecord { get; set; }
        List<RoleMaster> RoleMasterList { set; }
    }
    public interface IUserReportRegisterView : IBaseView
    {
        List<UserReport> UserReportList { get; set; }
    }
}
