using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface IDaywiseReportView : IBaseView
    {
        List<StoreMaster> StoreMasterList { set; }
    }
}
