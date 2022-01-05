using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOSOperations.ISalesTarget
{
    public interface IsalesTargetView : IBaseView
    {
        int ID { get; set; }
        List<CountryMaster> CountryMasterLookUp { set; }
        List<StoreMaster> StoreMasterList { set; }
        List<SalestargetDetails> SalestargetDetailsList { get; set; }     
        int CountryID { get; set; }
        int BrandID { get; set; }
        //int StoreID { get; set; }
        String Brand { get; set; }
        String StoreIDs { get; set; }
        String Year { get; set; }
        List<YearMaster> YearLookUp { set; }
        List<BrandMaster> BrandMasterLookUp { set; }

        List<SalestargetDetails> GetSalestargetDetailsList { get; set; }
    }
}
