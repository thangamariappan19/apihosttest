using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface IStockMovementReportView : IBaseView
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        int CountryID { get; set; }
        List<CountryMaster> CountryLookUp { set; }
        int StoreGroupID { get; set; }
        List<StoreGroupMaster> StoreGroupLookUp { set; }
        int StoreMasterID { get; set; }
        List<StoreMaster> StoreMasterLookUp { set; }
        List<PosMaster> POSMasterLookUp { set; }
        List<StyleMaster> SelectStyleLookUp { set; }
        List<DocumentTypes> DocumentTypesLookUp { set; }
        List<StockMovementReport> StockMovementReport { get; set; }
    }
}
