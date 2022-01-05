using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static SKUMasterTypes StyleDetailsDeepCopy(SKUMasterTypes objStyleDetails)
        {
            SKUMasterTypes tempStyleDetails = new SKUMasterTypes();
            tempStyleDetails.ColorID = objStyleDetails.ColorID;
            tempStyleDetails.SKUCode = objStyleDetails.SKUCode;
            //tempStyleDetails.ProductDepartmentCode = objStyleDetails.ProductDepartmentCode;
            tempStyleDetails.Active = objStyleDetails.Active;
            tempStyleDetails.CreateBy = objStyleDetails.CreateBy;
            tempStyleDetails.CreatedByUserName = objStyleDetails.CreatedByUserName;
            tempStyleDetails.CreateOn = objStyleDetails.CreateOn;
            //tempStyleDetails.StyleName = objStyleDetails.StyleName;
            tempStyleDetails.DesignCode = objStyleDetails.DesignCode;
            tempStyleDetails.IsDeleted = objStyleDetails.IsDeleted;

            tempStyleDetails.SCN = objStyleDetails.SCN;
            tempStyleDetails.SizeID = objStyleDetails.SizeID;
            //tempStyleDetails.SizeDescription = objStyleDetails.SizeDescription;
            tempStyleDetails.StyleCode = objStyleDetails.StyleCode;
            //tempStyleDetails.StyleName = objStyleDetails.StyleName;
            tempStyleDetails.StyleID = objStyleDetails.StyleID;
            tempStyleDetails.BrandID = objStyleDetails.BrandID;
            tempStyleDetails.UpdateBy = objStyleDetails.UpdateBy;
            tempStyleDetails.UpdatedByUserName = objStyleDetails.UpdatedByUserName;
            tempStyleDetails.UpdateOn = objStyleDetails.UpdateOn;
            tempStyleDetails.ExchangeRate = objStyleDetails.ExchangeRate;
            tempStyleDetails.PurchasePriceListID = objStyleDetails.PurchasePriceListID;
            
            return tempStyleDetails;
        }
    }
   
}
