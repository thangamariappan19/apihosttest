using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPromotion
{
    public interface IWNPromotionView :IBaseView
    {
        int Mode { get; }
        string PromotionCode { get; set; }
        string PromotionName { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        //int PricePointID { get; set; }
        int DefaultPriceListID { get; set; }
        string Countries { get; set; }
        List<PricePoint> PricePointList { get; set; }
        List<PriceListType> PriceList { get; set; }
        List<CountryMaster> CountryList { get; set; }
        string UploadType { get; set; }        
        WNPromotion WNPromotionData { set; }
        List<WNPromotionDetails> WNPromotionDetailsList { get; set; }
        List<StyleMaster> StyleList { get; set; }
        int WNPromotionID { get; set; }

        List<BrandMaster> BrandList { get; set; }

        List<StylePricing> StylePricingList { get; set; }

        bool PricePointApplicable { get; set; }

        bool Active { get;set; }

        string StyleCode { get; set; }
        int SalePriceListID { get; set; }
        int DefaultCountryID { get; set; }
        //string PricePointCode { get; }
        //string PriceListCode { get; }
    }
}
