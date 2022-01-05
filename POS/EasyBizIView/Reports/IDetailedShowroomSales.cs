using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
   public interface IDetailedShowroomSales : IBaseView
    {
        List<StoreMaster> StoreList { set; }
        DateTime BusinessDate { get; }
        int StoreID { get; set; }
        DataTable DetailedShowroomSalesReportTable { set; }
        int SelectedStoreId { get; set; }

        StoreMaster StoreMasterRecord { get; set; }
    }

}
