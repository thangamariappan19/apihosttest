using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPromotions
{
    public interface IPromotionsView : IBaseView
    {        
        int ID { get; set; }
        string PromotionCode { get; set; }
        string PromotionName { get; set; }
        string PromotionType { get; set; }
        string Type { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        Decimal MinBillAmount { get; set; }
        int MinQuantity { get; set; }

        string Discount { get; set; }
        Decimal DiscountValue { get; set; }
        Boolean AllowMultiPromotion { get; set; }
        Boolean LowestValue { get; set; }
        Boolean LowestValueWithGroup { get; set; }
        Boolean ExculdeDiscountItems { get; set; }
        Boolean Prompt { get; set; }
        Boolean Active { get; set; }
        string Colors { get; set; }
        int BuyOptionalCount { get; set; }
        int GetOptionalCount { get; set; }
        Decimal GetItematFixedPrice { get; set; }
        List<StoreGroupMaster> StoreGroupList { get; set; }       
        List<StoreMaster> StoreMasterList { get; set; }       
        List<CustomerMaster> CustomerMasterList { get; set; }      
        List<CustomerGroupMaster> CustomerGroupMasterList { get; set; }        
        List<PriceListType> PriceListLookUp { get; set; }
        int PriceListID { get; set; }

        //List<LookUp> CommonCustomerLookUpList { get; set; }

        List<LookUp> CommonExclusionLookUpList { get; set; }

        List<AFSegamationMasterTypes> SegamationMasterList { get; set; }
        List<StyleMaster> StyleMasterList { get; set; }

        List<YearMaster> YearList { get; set; }

        List<SeasonMaster> SeasonList { get; set; }       
        List<BrandMaster> BrandList { get; set; }

        List<SubBrandMaster> SubBrandList { get; set; }

        List<ProductGroupMaster> ProductGroupList { get; set; }

        List<ProductSubGroupMaster> ProductSubGroupList { get; set; }
        List<CommonUtil> PromotionWithStoreList { get; set; }
        List<CommonUtil> PromotionWithCustomerList { get; set; }
        List<CommonUtil> PromotionWithProductList { get; set; }
        List<CommonUtil> PromotionWithBuyItemList { get; set; }
        List<CommonUtil> PromotionWithGetItemList { get; set; }
         string RecordType { get; set; }
         int CountryID { get; set; }
         List<CountryMaster> CountryList { get; set; }
         List<CouponMaster> CouponMasterList { get; set; }

         string AppliedType { get; set; }
    }
}
