using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EasyBizDBTypes.Masters
{

    [DataContract]
    [Serializable]
    public class SKUMasterTypes:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string StyleName { get; set; }

        [DataMember]
        public string DesignCode { get; set; }

        [DataMember]
        public string SKUName { get; set; }

        [DataMember]
        public int StyleID { get; set; }


        [DataMember]
        public int DesignID { get; set; }

        [DataMember]
        public int BrandID { get; set; }


        [DataMember]
        public int SubBrandID { get; set; }


        [DataMember]
        public int CollectionID { get; set; }


        [DataMember]
        public int ArmadaCollectionID { get; set; }
        [DataMember]
        public string ArmadaCollectionCode { get; set; }
        [DataMember]
        public string CollectionCode { get; set; }
        [DataMember]
        public string DivisionCode { get; set; }
        [DataMember]
        public string ProductGroupCode { get; set; }

        [DataMember]
        public int DivisionID { get; set; }


        [DataMember]
        public int ProductGroupID { get; set; }


        [DataMember]
        public int ProductSubGroupID { get; set; }

        [DataMember]
        public int SeasonID { get; set; }

        [DataMember]
        public int YearID { get; set; }

        [DataMember]
        public int ProductLineID { get; set; }

        [DataMember]
        public int StyleStatusID { get; set; }

        [DataMember]
        public int DesignerID { get; set; }

        [DataMember]
        public int PurchasePriceListID { get; set; }        

        [DataMember]
        public Decimal PurchasePrice { get; set; }
      

        [DataMember]
        public int PurchaseCurrencyID { get; set; }

        [DataMember]
        public Decimal RRPPrice { get; set; }

        [DataMember]
        public int RRPCurrencyID { get; set; }

        [DataMember]
        public Decimal ExchangeRate { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int ScaleID { get; set; }
        [DataMember]
        public int SalesPriceListID { get; set; }
        [DataMember]
        public Decimal SalesPrice { get; set; }

        [DataMember]
        public int ColorID { get; set; }

         [DataMember]
        public int ScaleDetailMasterID { get; set; }
        

         [DataMember]
        public int SegamentationID { get; set; }
        

        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public string SizeCode { get; set; }

        [DataMember]
        public string ColorCode { get; set; }

        [DataMember]
        public string ColorName { get; set; }
        [DataMember]
        public string SizeName { get; set; }  
      


        [DataMember]
         public string CollectionName { get; set; }

        [DataMember]
        public string DivisionName { get; set; }

         [DataMember]
        public string ScaleName { get; set; }

         //[DataMember]
         //public string ItemImage { get; set; }


         [DataMember]
         public string BrandName { get; set; }



         [DataMember]
         public string AFSegamationName { get; set; }


         [DataMember]
         public string Year { get; set; }


         [DataMember]
         public string SeasonName { get; set; }


         [DataMember]
         public string ProductGroupName { get; set; }


         [DataMember]
         public string ProductSubGroupName { get; set; }


         [DataMember]
         public string SubBrandName { get; set; }


         [DataMember]
         public bool PromotionApplied { get; set; }
         [DataMember]
         public string ShortDescription { get; set; }
         [DataMember]
         public string Description { get; set; }
         [DataMember]
         public string BrandShortCode { get; set; }
         [DataMember]
         public string Origin { get; set; }
         [DataMember]
         public Decimal DefaultPrice { get; set; }
        [DataMember]
         public string SupplierBarcode { get; set; }

        [DataMember]
        public string ArabicSKU { get; set; }

        [DataMember]
        public byte[] SKUImage { get; set; }
        //DefaultPrice  Origin

        //[DataMember]
        //public ImageSource SKUImageSource { get; set; }
        [DataMember]
        public dynamic SKUImageSource { get; set; }

        [DataMember]
        public List<SKUMasterTypes> SKUMasterTypesRecord { get; set; }
        [DataMember]
        public List<SKUMasterTypes> ImportExcelList { get; set; }
        [DataMember]
        public string PriceListID { get; set; }
        [DataMember]
        public int SalePriceListID { get; set; }
        [DataMember]
        public string BaseEntry { get; set; }
        [DataMember]
        public List<StylePricing> StylePricingList { get; set; }
        [DataMember]
        public List<ItemImageMaster> ItemImageMasterList { get; set; }
        [DataMember]
        public long BarCodeRunningNo { get; set; }
        [DataMember]
        public int BarCodeID { get; set; }

        [DataMember]
        public Decimal StylePrice { get; set; }

        [DataMember]
        public string UseSeperator { get; set; }
        [DataMember]
        public string Tag_Id { get; set; }

        [DataMember]
        public bool IsNonTrading { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string SubBrandCode { get; set; }
        [DataMember]
        public int Stock { get; set; }
        [DataMember]
        public string SeasonCode { get; set; }
        [DataMember]
        public string YearCode { get; set; }
        [DataMember]
        public string ProductLineCode { get; set; }
        [DataMember]
        public string StyleStatusCode { get; set; }
        [DataMember]
        public string DesignerCode { get; set; }
        [DataMember]
        public string PurchasePriceListCode { get; set; }
        [DataMember]
        public string PurchasePriceCurrencyCode { get; set; }
        [DataMember]
        public string RRPCurrencyCode { get; set; }
        [DataMember]
        public string ScaleCode { get; set; }
        [DataMember]
        public string SegmentationCode { get; set; }
        [DataMember]
        public string ProductSubGroupCode { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string BinCode { get; set; }
        [DataMember]
        public bool IsComboItem { get; set; }
        [DataMember]
        public bool IsHeaderItem { get; set; }
    }
}
