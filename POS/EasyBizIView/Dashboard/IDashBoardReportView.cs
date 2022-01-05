using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Dashboard
{
    public interface IDashBoardReportView : IBaseView
    {
        int ID { get; set; }
        string ReportName { get; set; }
        string Remarks { get; set; }
        Byte[] ReportFile { get; set; }
        bool IsActive { get; set; }
    }
}
