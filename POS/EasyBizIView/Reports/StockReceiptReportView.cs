using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface StockReceiptReportView : IBaseView
    {
        List<StoreMaster> StoreList { set; }
        DateTime BusinessDate { get; }
        DateTime ToBusinessDate { get; }
        int StoreID { get; set; }
        DataTable StockReceiptReportTable { set; }
        int SelectedStoreId { get; set; }
        StoreMaster StoreMasterRecord { get; set; }
    }
}
