using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotion.IDiscountMaster
{
    public interface IDiscountMasterView : IBaseView
    {
        int ID { get; set; }
        List<CustomerGroupMaster> CustomerGroupNameLookUp { set; }

        List<CountryMaster> CountryMasterLookUp { set; }
        List<StoreMaster> StoreMasterLookUp { set; }

        string CountryIDs { get; set; }
        string StoreIDs { get; set; }
        int CustomerGroupID { get; set; }
        string CustomerGroupCode { get; }

        string CountryCodes { get; set; }

        string StoreCodes { get; set; }

        List<StoreBrandMapping> StoreBrandMapList { set; }

        // List<StoreBrandMapping> StoreBrandRecord { get; set; }
        List<EmployeDiscountDetailTypes> EmployeeDiscountDetailsList { get; set; }
        List<FamilyDiscountDetailTypes> FamilyDiscountDetailsList { get; set; }

        string DiscountType { get; set; }

        DiscountMasterTypes DiscountMasterRecord { get; set; }
    }
}
