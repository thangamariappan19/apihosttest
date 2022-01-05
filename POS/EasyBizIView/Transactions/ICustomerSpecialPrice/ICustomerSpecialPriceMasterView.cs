using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICustomerSpecialPriceMaster
{
    public interface ICustomerSpecialPriceMasterView : IBaseView
    {
        int PriceListID { get; set; }
        List<PriceListType> PriceListLookUp {  set; }

        int CustomerGroupID { get; set; }
        List<CustomerGroupMaster> CustomerGroupLookUp { set; }
        int CustomeMasterID { get; set; }
        List<CustomerMaster> CustomerMasterLookUp { set; }
        int ID { get; set; }
       
        DateTime DateFrom { get; set; }
        DateTime DateTo { get; set; }
        
        string DiscountType { get; set; }
        int DiscountValue { get; set; }
        bool Active { get; set; }
        bool CustomerGroupUsed { get; set; }
        bool CustomerMasterUsed { get; set; }

        string CategoryType { get; set; }

        List<StoreGroupMaster> StoreGroupMasterList { get; set; }
        List<CountryMaster> CountryMasterList { get; set; }
        List<CustomerMaster> CustomerMasterList { get; set; }

        List<AFSegamationMasterTypes> SegamationMasterList { get; set; }
        List<YearMaster> YearList { get; set; }
        List<BrandMaster> BrandList { get; set; }
        List<SeasonMaster> SeasonList { get; set; }
        List<ProductGroupMaster> ProductGroupList { get; set; }
        List<SubBrandMaster> SubBrandMasterList { get; set; }
        List<ProductSubGroupMaster> ProductSubGroupList { get; set; }
        List<StoreMaster> StoreMasterList { get; set; }
        List<CommonUtil> CategoryCommonUtil { get; set; }
        List<CommonUtil> StoreCommonUtil { get; set; }
       
        List<StoreMaster> StoreList{ get; set; }
        string DetailsType { get; set; }
        string ApplicablePriceListCode { get; }
        string CustomerGroupCode { get; }       
       
    }
}
