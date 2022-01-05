using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Dashboard
{
   public interface DashboardViewerView : IBaseView
    {
        int ID { get; set; }

        Byte[] ReportData { set; }
    }
}
