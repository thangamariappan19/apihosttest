using EasyBizBLL.Masters;
using EasyBizIView.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports
{
  public class StockMovementReportPresenter
    {
        
        CountryBLL _CountryBLL = new CountryBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        PosMasterBLL _PosMasterBLL = new PosMasterBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
    }
}
