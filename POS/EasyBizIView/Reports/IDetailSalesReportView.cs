using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface IDetailSalesReportView : IBaseView
    {
        List<StoreMaster> StoreList { set; }
        string InvoiceNo { get; }
        int StoreID { get; set; }        
        DataTable DetailSalesDataTable { set; }
        int SelectedStoreId { get; set; }

        StoreMaster StoreMasterRecord { get; set; }
    }
}
