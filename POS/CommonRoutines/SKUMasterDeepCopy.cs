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
         public static SKUMasterTypes SKUMasterDeepCopy(SKUMasterTypes objSKUMasterTypes)
         {
             var TempSKUMasterTypes = new SKUMasterTypes();
             TempSKUMasterTypes.ID = objSKUMasterTypes.ID;
             TempSKUMasterTypes.SKUCode = objSKUMasterTypes.SKUCode;
             TempSKUMasterTypes.StyleName = objSKUMasterTypes.StyleName;
             TempSKUMasterTypes.DesignCode = objSKUMasterTypes.DesignCode;
             TempSKUMasterTypes.SKUName = objSKUMasterTypes.SKUName;
             TempSKUMasterTypes.StyleID = objSKUMasterTypes.StyleID;
             TempSKUMasterTypes.DesignID = objSKUMasterTypes.DesignID;
             TempSKUMasterTypes.BrandID = objSKUMasterTypes.BrandID;
             TempSKUMasterTypes.SubBrandID = objSKUMasterTypes.SubBrandID;
             TempSKUMasterTypes.CollectionID = objSKUMasterTypes.CollectionID;
             TempSKUMasterTypes.ArmadaCollectionID = objSKUMasterTypes.ArmadaCollectionID;
             TempSKUMasterTypes.DivisionID = objSKUMasterTypes.DivisionID;
             TempSKUMasterTypes.ProductGroupID = objSKUMasterTypes.ProductGroupID;
             TempSKUMasterTypes.ProductSubGroupID = objSKUMasterTypes.ProductSubGroupID;
             TempSKUMasterTypes.SeasonID = objSKUMasterTypes.SeasonID;
             TempSKUMasterTypes.YearID = objSKUMasterTypes.YearID;
             TempSKUMasterTypes.ProductLineID = objSKUMasterTypes.ProductLineID;
             TempSKUMasterTypes.StyleStatusID = objSKUMasterTypes.StyleStatusID;
             TempSKUMasterTypes.DesignerID = objSKUMasterTypes.DesignerID;
             TempSKUMasterTypes.PurchasePriceListID = objSKUMasterTypes.PurchasePriceListID;
             TempSKUMasterTypes.PurchasePrice = objSKUMasterTypes.PurchasePrice;
             TempSKUMasterTypes.PurchaseCurrencyID = objSKUMasterTypes.PurchaseCurrencyID;
             TempSKUMasterTypes.RRPPrice = objSKUMasterTypes.RRPPrice;
             TempSKUMasterTypes.RRPCurrencyID = objSKUMasterTypes.RRPCurrencyID;
             TempSKUMasterTypes.ExchangeRate = objSKUMasterTypes.ExchangeRate;
             TempSKUMasterTypes.Remarks = objSKUMasterTypes.Remarks;
             TempSKUMasterTypes.ScaleID = objSKUMasterTypes.ScaleID;
             TempSKUMasterTypes.ColorID = objSKUMasterTypes.ColorID;
             TempSKUMasterTypes.SegamentationID = objSKUMasterTypes.SegamentationID;
             TempSKUMasterTypes.SizeID = objSKUMasterTypes.SizeID;
             TempSKUMasterTypes.SizeCode = objSKUMasterTypes.SizeCode;
             TempSKUMasterTypes.ColorCode = objSKUMasterTypes.ColorCode;
             TempSKUMasterTypes.CollectionName = objSKUMasterTypes.CollectionName;
             TempSKUMasterTypes.DivisionName = objSKUMasterTypes.DivisionName;
             TempSKUMasterTypes.ScaleName = objSKUMasterTypes.ScaleName;
             //TempSKUMasterTypes.ItemImage = objSKUMasterTypes.ItemImage;
             TempSKUMasterTypes.BrandName = objSKUMasterTypes.BrandName;
             TempSKUMasterTypes.AFSegamationName = objSKUMasterTypes.AFSegamationName;

             TempSKUMasterTypes.Year = objSKUMasterTypes.Year;
             TempSKUMasterTypes.SeasonName = objSKUMasterTypes.SeasonName;
             TempSKUMasterTypes.ProductGroupName = objSKUMasterTypes.ProductGroupName;

             TempSKUMasterTypes.ProductSubGroupName = objSKUMasterTypes.ProductSubGroupName;
             TempSKUMasterTypes.SubBrandName = objSKUMasterTypes.SubBrandName;
             TempSKUMasterTypes.PromotionApplied = objSKUMasterTypes.PromotionApplied;
             TempSKUMasterTypes.StylePrice = objSKUMasterTypes.StylePrice;

            TempSKUMasterTypes.StyleCode = objSKUMasterTypes.StyleCode;
            TempSKUMasterTypes.BarCode = objSKUMasterTypes.BarCode;
            TempSKUMasterTypes.SupplierBarcode = objSKUMasterTypes.SupplierBarcode;

            TempSKUMasterTypes.UseSeperator = objSKUMasterTypes.UseSeperator;

            TempSKUMasterTypes.Tag_Id = objSKUMasterTypes.Tag_Id;

            return TempSKUMasterTypes;

         }


         public static List<SKUMasterTypes> SKUMasterListDeepCopy(List<SKUMasterTypes> objSKUMasterTypesList)
         {
             var TempSKUMasterTypesList = new List<SKUMasterTypes>();

             foreach(SKUMasterTypes objSKUMasterTypes in  objSKUMasterTypesList)
             {
                 var TempSKUMasterTypes = new SKUMasterTypes();
                 TempSKUMasterTypes.ID = objSKUMasterTypes.ID;
                 TempSKUMasterTypes.SKUCode = objSKUMasterTypes.SKUCode;
                 TempSKUMasterTypes.StyleCode = objSKUMasterTypes.StyleCode;
                 TempSKUMasterTypes.StyleName = objSKUMasterTypes.StyleName;
                 TempSKUMasterTypes.DesignCode = objSKUMasterTypes.DesignCode;
                 TempSKUMasterTypes.SKUName = objSKUMasterTypes.SKUName;
                 TempSKUMasterTypes.StyleID = objSKUMasterTypes.StyleID;
                 TempSKUMasterTypes.DesignID = objSKUMasterTypes.DesignID;
                 TempSKUMasterTypes.BrandID = objSKUMasterTypes.BrandID;
                 TempSKUMasterTypes.SubBrandID = objSKUMasterTypes.SubBrandID;
                 TempSKUMasterTypes.CollectionID = objSKUMasterTypes.CollectionID;
                 TempSKUMasterTypes.ArmadaCollectionID = objSKUMasterTypes.ArmadaCollectionID;
                 TempSKUMasterTypes.DivisionID = objSKUMasterTypes.DivisionID;
                 TempSKUMasterTypes.ProductGroupID = objSKUMasterTypes.ProductGroupID;
                 TempSKUMasterTypes.ProductSubGroupID = objSKUMasterTypes.ProductSubGroupID;
                 TempSKUMasterTypes.SeasonID = objSKUMasterTypes.SeasonID;
                 TempSKUMasterTypes.YearID = objSKUMasterTypes.YearID;
                 TempSKUMasterTypes.ProductLineID = objSKUMasterTypes.ProductLineID;
                 TempSKUMasterTypes.StyleStatusID = objSKUMasterTypes.StyleStatusID;
                 TempSKUMasterTypes.DesignerID = objSKUMasterTypes.DesignerID;
                 TempSKUMasterTypes.PurchasePriceListID = objSKUMasterTypes.PurchasePriceListID;
                 TempSKUMasterTypes.PurchasePrice = objSKUMasterTypes.PurchasePrice;
                 TempSKUMasterTypes.PurchaseCurrencyID = objSKUMasterTypes.PurchaseCurrencyID;
                 TempSKUMasterTypes.RRPPrice = objSKUMasterTypes.RRPPrice;
                 TempSKUMasterTypes.RRPCurrencyID = objSKUMasterTypes.RRPCurrencyID;
                 TempSKUMasterTypes.ExchangeRate = objSKUMasterTypes.ExchangeRate;
                 TempSKUMasterTypes.Remarks = objSKUMasterTypes.Remarks;
                 TempSKUMasterTypes.ScaleID = objSKUMasterTypes.ScaleID;
                 TempSKUMasterTypes.ColorID = objSKUMasterTypes.ColorID;
                 TempSKUMasterTypes.SegamentationID = objSKUMasterTypes.SegamentationID;
                 TempSKUMasterTypes.SizeID = objSKUMasterTypes.SizeID;
                 TempSKUMasterTypes.SizeCode = objSKUMasterTypes.SizeCode;
                 TempSKUMasterTypes.ColorCode = objSKUMasterTypes.ColorCode;
                 TempSKUMasterTypes.CollectionName = objSKUMasterTypes.CollectionName;
                 TempSKUMasterTypes.DivisionName = objSKUMasterTypes.DivisionName;
                 TempSKUMasterTypes.ScaleName = objSKUMasterTypes.ScaleName;
                 //TempSKUMasterTypes.ItemImage = objSKUMasterTypes.ItemImage;
                 TempSKUMasterTypes.BrandName = objSKUMasterTypes.BrandName;
                 TempSKUMasterTypes.AFSegamationName = objSKUMasterTypes.AFSegamationName;

                 TempSKUMasterTypes.Year = objSKUMasterTypes.Year;
                 TempSKUMasterTypes.SeasonName = objSKUMasterTypes.SeasonName;
                 TempSKUMasterTypes.ProductGroupName = objSKUMasterTypes.ProductGroupName;

                 TempSKUMasterTypes.ProductSubGroupName = objSKUMasterTypes.ProductSubGroupName;
                 TempSKUMasterTypes.SubBrandName = objSKUMasterTypes.SubBrandName;
                 TempSKUMasterTypes.PromotionApplied = objSKUMasterTypes.PromotionApplied;

                TempSKUMasterTypes.StylePrice = objSKUMasterTypes.StylePrice;

                TempSKUMasterTypes.StyleCode = objSKUMasterTypes.StyleCode;
                TempSKUMasterTypes.BarCode = objSKUMasterTypes.BarCode;
                TempSKUMasterTypes.SupplierBarcode = objSKUMasterTypes.SupplierBarcode;

                TempSKUMasterTypes.UseSeperator = objSKUMasterTypes.UseSeperator;

                TempSKUMasterTypes.Tag_Id = objSKUMasterTypes.Tag_Id;

                TempSKUMasterTypesList.Add(TempSKUMasterTypes);

             }
             return TempSKUMasterTypesList;    
         }

     }
   
}
