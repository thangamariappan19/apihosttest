using EasyBizDBTypes.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Dashboard
{
    public interface IDashBoardReportListView : IBaseView
    {
        int ID { get; set; }
        List<RegisterDashboard> DashBoardReportsList { get; set; }
    }
}
