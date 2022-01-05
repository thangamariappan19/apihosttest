using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Style
{
    public interface IStyleView : IBaseView
    {
        int ID { get; set; }
        string StyleCode { get; set; }     
        string StyleName { get; set; }
        string ShortDesignName { get; set; }
        //int StyleSegmentation { get; set; }
        //string DesignCode { get; set; }
        string DesignName { get; set; }      
        int BrandID { get; set; }       
        List<BrandMaster> BrandLookUp { set; }
        int SubBrandID { get; set; }
        List<SubBrandMaster> SubBrandMasterLookUp { set; }       
        string ProductDepartmentCode { get; set; }
        List<StyleMaster> MasterList { get; set; }
        List<ScaleDetailMaster> ScaleDetailList { get; set; }      
        int ScaleID { get; set; }
        List<ScaleMaster> ScaleMasterLookUp { set; }
        List<ColorMaster> ColorList { get; set; }
        int NewDesignID { get; set; }
        List<DesignMasterTypes> DesignLookUp { get; set; }
        List<TransactionLog> StockList { get; set; }
        int CollectionID { get; set; }
        List<CollectionMasterTypes> CollectionLookUp { set; }
        int DivisionID { get; set; }
        List<DivisionMaster> DivisionLookUp { set; }
        int ProductGroupID { get; set; }
        List<ProductGroupMaster> ProductGroupLookUp { set; }
        int ProductSubGroupID { get; set; }
        List<ProductSubGroupMaster> ProductSubGroupLookUp { set; }       
        int SeasonID { get; set; }
        List<SeasonMaster> SeasonLookUp { set; }
        int YearCode { get; set; }
        List<YearMaster> YearLookUp { set; }
        int ProductLineID { get; set; }
        List<ProductLineMaster> ProductLineMasterLookUp { set; }
        int DesignerID { get; set; }
        List<EmployeeMaster> DesignerLookUp { set; }
        string SalesCurrencyType { get; set; }
        string PurchaseCurrencyType { get; set; }
        int SalesCurrencyID { get; set; }
        List<CurrencyMaster> SalesCurrencyLookUp { set; }
        int PurchaseCurrencyID { get; set; }
        List<CurrencyMaster> PurchaseCurrencyLookUp { set; }   
        int DocumentID { get; set; }       
        List<SKUMasterTypes> StyleDetailsList { get; set; }
        Decimal RRPPrice { get; set; }
        Decimal PurchasePrice { get; set; }
        int SegmentationID { get; set; }
        Decimal SalePrice { get; set; }
        List<AFSegamationMasterTypes> SegamationMasterLookUp { set; }
        Boolean Active { get; set; }
        Boolean Franchise { get; set; }
        List<SegmentMaster> SegmentDetailList { get; set; }
        int StyleStatusID { get; set; }
        List<StyleStatusMasterType> StyleStatusMasterTypeLookUp { set; }
        int ArmadaCollectionID { get; set; }
        List<ArmadaCollectionsMaster> ArmadaCollectionsLookUp { set; }
        Decimal ExchangeRate { get; set; }
        List<PriceListType> PriceListTypeLookUp { set; }
        List<PriceListType> SalesPriceListTypeLookUp { set; }

        List<StylePricing> SalesPriceLookUp { set; }

        int PurchasePriceListID { get; set; }
        int SalesPriceListID { get; set; }
        Decimal SalesPrice { get; set; }

        string ItemImage { get; set; }

        List<ProductGroupMaster> ProductGroupMasterList { get; set; }
        List<PricePoint> PricePointList { get; set; }
        List<SKUMasterTypes> SKUMasterTypesList { get; set; }
        List<ItemImageMaster> SKUItemImageMasterList { get; set; }


        int CountryID { get; }
        int StateID { get; }
        int StoreID { get; }
        int DocumentTypeID { get; }
        string DocumentNo { get; set; }
        long StyleRunningNum { get; set; }
        long RunningNum { get; set; }
        string ShortBrandCode { get; set; }
        string DesignCode { get; set; }
        string ShortDescription { get; set; }
        string SKUBarCode { get; set; }
        long BarCodeRunningNo { get; set; }
        int BarCodeID { get; set; }
        string SalesType { get; set; }
        List<TransactionLog> ColorWiseStockList { get; set; }
        List<TransactionLog> ScaleWiseStockList { get; set; }

        string Composition { get; set; }

        string SymbolGroup { get; set; }

        string Owner { get; set; }

        string CountryOfOrigin { get; set; }

        string ShortDescriptionn { get; set; }

        int DropID { get; set; }

        List<DropMasterTypes> DropMasterLookUp { set; }

        String Grade { get; set; }

        List<DesignGradeTypes> DesignGradeLookUp { set; }

        string DevelopmentOffice { get; set; }

        List<DesignDevelopmentOfficeTypes> DesignDevelopmentOfficeLookUp { set; }
        List<StylePricing> StylepricingList { get; set; }

        string ArabicStyle { get; set; }

        UsersSettings UserInformation { get; }
    }
}
