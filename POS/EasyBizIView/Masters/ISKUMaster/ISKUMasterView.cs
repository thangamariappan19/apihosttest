using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ISKUMaster
{
    public interface ISKUMasterView:IBaseView
    {
      
          int ID { get; set; }
       
          string SKUCode { get; set; }

      
          string SKUName { get; set; }

          Boolean Active { get; set; }
        
          int StyleID { get; set; }
       
          int DesignID { get; set; }

       
          int BrandID { get; set; }

          string barcode { get; set; }


        
           int ScaleID { get; set; }

       
           int ColorID { get; set; }

      
           int SizeID { get; set; }
          int SubBrandID { get; set; }
          int CollectionID { get; set; }


          int ArmadaCollectionID { get; set; }


          int DivisionID { get; set; }

          int ProductGroupID { get; set; }

          int ProductSubGroupID { get; set; }

          int SeasonID { get; set; }

          int YearID { get; set; }


          int ProductLineID { get; set; }


          int StyleStatusID { get; set; }


          int DesignerID { get; set; }


          int PurchasePriceListID { get; set; }

          Decimal PurchasePrice { get; set; }


          int PurchaseCurrencyID { get; set; }


          Decimal RRPPrice { get; set; }

          Decimal ExchangeRate { get; set; }


          int RRPCurrencyID { get; set; }

        
          string Remarks { get; set; }
          string StyleCode { get; set; }

          string SupplierBarcode { get; set; }
          List<ArmadaCollectionsMaster> ArmadaCollectionsLookUp { set; }


          List<SKUMasterTypes> SKUMasterList { get; set; }

          List<ScaleDetailMaster> ScaleDetailList { get; set; }
          List<ScaleMaster> ScaleMasterLookUp { set; }
          List<ColorMaster> ColorList { get; set; }


         List<BrandMaster> BrandMasterLookUp { set; }

         List<SubBrandMaster> SubBrandMasterLookUp { set; }


         List<DesignMasterTypes> DesignLookUp { set; }

         List<CollectionMasterTypes> CollectionLookUp { set; }

         List<PriceListType> PriceListTypeLookUp { set; }

         List<DivisionMaster> DivisionLookUp { get; set; }

         List<ProductGroupMaster> ProductGroupLookUp { get; set; }

         List<ProductSubGroupMaster> ProductSubGroupLookUp { get; set; }

         List<YearMaster> YearLookUp { get; set; }

         List<ProductLineMaster> ProductLineMasterLookUp { get; set; }

         List<SeasonMaster> SeasonLookUp { get; set; }

         List<EmployeeMaster> DesignerLookUp { get; set; }

         List<StyleStatusMasterType> StyleStatusMasterLookUp { set; }

         List<CurrencyMaster> SalesCurrencyLookUp { set; }      
         List<CurrencyMaster> PurchaseCurrencyLookUp { set; }

         List<StyleMaster> StyleMasterTypesLookUp { set; }

         List<AFSegamationMasterTypes> SegamationMasterLookUp { get; set; }

         int SegamentationID { get; set; }

         List<ItemImageMaster> ItemImageMasterList { get; set; }
         List<TransactionLog> StockList { get; set; }
         string SumOfPrefixSuffix { get; set; }
         long BarCodeRunningNo { get; set; }
         int BarCodeID { get; set; }
         string SKUBarCode { get; set; }

         string ArabicSKU { get; set; }

       //  string BrandCode { get; }
        // string SubBrandCode { get; }
        // string CollectionCode { get; }
        //string ArmadaCollectionCode { get; }
       //  string DivisionCode { get; }
       //  string ProductGroupCode { get; }
       //  string SeasonCode { get; }
       //  string YearCode { get; }
       //  string ProductLineCode { get; }
         //string StyleStatusCode { get; }
      //   string DesignerCode { get; }
      //   string DesignCode { get; }
        // string PurchasePriceListCode { get; }
      //   string PurchaseCurrencyCode { get; }
       //  string RRPCurrencyCode { get; }
       //  string ScaleCode { get; }
        // int SegamentationCode { get; }
        // string ProductSubGroupCode { get; }
        // string SizeCode { get; }
    }
}
