using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
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
        string Type { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        Double MinBillAmount { get; set; }
        int MinQuantity { get; set; }

        string Discount { get; set; }
        Double DiscountValue { get; set; }
        Boolean AllowMultiPromotion { get; set; }
        Boolean LowestValue { get; set; }
        Boolean ExculdeDiscountItems { get; set; }
        Boolean Prompt { get; set; }
        Boolean Active { get; set; }
        string Colors { get; set; }
        Double BuyOptionalCount { get; set; }
        Double GetOptionalCount { get; set; }
        Double GetItematFixedPrice { get; set; }
        List<StoreGroupMaster> StoreGroupList { get; set; }       
        List<StoreMaster> StoreMasterList { get; set; }       
        List<CustomerMaster> CustomerMasterList { get; set; }      
        List<CustomerGroupMaster> CustomerGroupMasterList { get; set; }
        List<AFSegamationMasterTypes> SegamationMasterList { get; set; }
        List<YearMaster> YearList { get; set; }

        List<BrandMaster> BrandList { get; set; }

        List<SubBrandMaster> SubBrandMasterList { get; set; }

        List<SeasonMaster> SeasonList { get; set; }

        List<ProductGroupMaster> ProductGroupList { get; set; }

        List<ProductSubGroupMaster> ProductSubGroupList { get; set; }

        List<CommonUtil> StoreCommonUtil { get; set; }



        List<CommonUtil> CommonAllDetailsCommonUtil { get; set; }

        List<CommonUtil> CustomerCommonUtil { get; set; }
        List<CommonUtil> ProductCommonUtil { get; set; }
        List<CommonUtil> BuyItemCommonUtil { get; set; }
        List<CommonUtil> GetItemCommonUtil { get; set; }
        string StoreType { get; set; }
        string DetailsTypeName { get; set; }

        List<PriceListType> PriceListLookUp { get; set; }
        int PriceListID { get; set; } 
    }
}
