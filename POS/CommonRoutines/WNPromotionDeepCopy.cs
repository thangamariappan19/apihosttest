using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class WNPromotionDeepCopyCreator
    {
        public static WNPromotion WNPromotionDeepCopy(WNPromotion objWNPromotion)
        {
            WNPromotion TempWNPromotion = new WNPromotion();
            TempWNPromotion.Active = objWNPromotion.Active;
            TempWNPromotion.Countries = objWNPromotion.Countries;
            TempWNPromotion.CreateBy = objWNPromotion.CreateBy;
            TempWNPromotion.CreatedByUserName = objWNPromotion.CreatedByUserName;
            TempWNPromotion.CreateOn = objWNPromotion.CreateOn;            
            TempWNPromotion.ID = objWNPromotion.ID;            
            TempWNPromotion.IsDeleted = objWNPromotion.IsDeleted;            
            TempWNPromotion.SCN = objWNPromotion.SCN;
            TempWNPromotion.PriceListID = objWNPromotion.PriceListID;
            TempWNPromotion.PricePointID = objWNPromotion.PricePointID;
            TempWNPromotion.PromotionCode = objWNPromotion.PromotionCode;
            TempWNPromotion.PromotionName = objWNPromotion.PromotionName;
            TempWNPromotion.UploadType = objWNPromotion.UploadType;
            TempWNPromotion.PricePointApplicable = objWNPromotion.PricePointApplicable;
            TempWNPromotion.DefaultCountryID = objWNPromotion.DefaultCountryID;
            TempWNPromotion.WNPromotionDetailsList = objWNPromotion.WNPromotionDetailsList;
            TempWNPromotion.UpdateBy = objWNPromotion.UpdateBy;
            TempWNPromotion.UpdatedByUserName = objWNPromotion.UpdatedByUserName;
            TempWNPromotion.UpdateOn = objWNPromotion.UpdateOn;           
            return TempWNPromotion;
        }
        public static WNPromotionDetails WNPromotionDetailsDeepCopy(WNPromotionDetails objWNPromotionDetails)
        {
            WNPromotionDetails TempWNPromotionDetails = new WNPromotionDetails();
            TempWNPromotionDetails.Active = objWNPromotionDetails.Active;
            TempWNPromotionDetails.Brand = objWNPromotionDetails.Brand;
            TempWNPromotionDetails.BrandID = objWNPromotionDetails.BrandID;
            TempWNPromotionDetails.Country = objWNPromotionDetails.Country;
            TempWNPromotionDetails.CountryID = objWNPromotionDetails.CountryID;            
            TempWNPromotionDetails.CreateBy = objWNPromotionDetails.CreateBy;
            TempWNPromotionDetails.CreatedByUserName = objWNPromotionDetails.CreatedByUserName;
            TempWNPromotionDetails.CreateOn = objWNPromotionDetails.CreateOn;
            TempWNPromotionDetails.Discount = objWNPromotionDetails.Discount;
            TempWNPromotionDetails.ErrorMsg = objWNPromotionDetails.ErrorMsg;
            TempWNPromotionDetails.NowPrice = objWNPromotionDetails.NowPrice;
            TempWNPromotionDetails.Status = objWNPromotionDetails.Status;
            TempWNPromotionDetails.StyleCode = objWNPromotionDetails.StyleCode;
            TempWNPromotionDetails.StyleID = objWNPromotionDetails.StyleID;
            TempWNPromotionDetails.WasPrice = objWNPromotionDetails.WasPrice;
            TempWNPromotionDetails.WNPromotionID = objWNPromotionDetails.WNPromotionID;
            TempWNPromotionDetails.ID = objWNPromotionDetails.ID;
            TempWNPromotionDetails.IsDeleted = objWNPromotionDetails.IsDeleted;
            TempWNPromotionDetails.SCN = objWNPromotionDetails.SCN;            
            TempWNPromotionDetails.UpdateBy = objWNPromotionDetails.UpdateBy;
            TempWNPromotionDetails.UpdatedByUserName = objWNPromotionDetails.UpdatedByUserName;
            TempWNPromotionDetails.UpdateOn = objWNPromotionDetails.UpdateOn;
            return TempWNPromotionDetails;
        }
        public static List<WNPromotionDetails> WNPromotionDetailListDeepCopy(List<WNPromotionDetails> objWNPromotionDetailList)
        {
            List<WNPromotionDetails> TempWNPromotionDetailList = new List<WNPromotionDetails>();
            foreach(WNPromotionDetails objWNPromotionDetails in objWNPromotionDetailList)
            {
                WNPromotionDetails TempWNPromotionDetails = new WNPromotionDetails();
                TempWNPromotionDetails = WNPromotionDetailsDeepCopy(objWNPromotionDetails);
                TempWNPromotionDetailList.Add(TempWNPromotionDetails);
            }
            return TempWNPromotionDetailList;
        }
    }
}
