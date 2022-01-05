using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
   public interface  IDailySalesReportView: IBaseView
    {
       int CountryID { get; set; }
       List<CountryMaster> CountryLookUp { set; }
       int StoreGroupID { get; set; }
       List<StoreGroupMaster> StoreGroupLookUp { set; }
       int StoreID { get; set; }
       List<StoreMaster> StoreMasterLookUp {  set; }

       int PosID { get; set; }
       List<PosMaster> POSMasterLookUp { set; }
       int StyleID { get; set; }
       List<StyleMaster> SelectStyleLookUp { set; }
       List<DailySalesReport> DailySalesReport { get; set; }
       string StyleCode { get; set; }
       DateTime FromDate { get; set; }
       DateTime ToDate { get; set; }
    
    }
}
