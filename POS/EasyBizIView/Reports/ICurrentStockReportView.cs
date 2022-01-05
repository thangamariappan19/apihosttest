using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface ICurrentStockReportView : IBaseView
    {
        int CountryID { get; set; }
        List<CountryMaster> CountryLookUp { set; }
        int StoreMasterID { get; set; }
        List<StoreMaster> StoreMasterLookUp { set; }
        int BrandID { get; set; }
        List<BrandMaster> BrandMasterLookUp { set; }
        int StyleID { get; set; }
        string StyleCode { get; set; }

        List<StyleMaster> SelectFromStyleLookUp { set; }
       // List<StyleMaster> SelectToStyleLookUp { set; }
        List<CurrentStockReport> CurrentStockReport { get; set; }

    }
}
