using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{

    [DataContract]
    [Serializable]
    public class StyleMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string StyleName { get; set; }
        [DataMember]
        public string ShortDesignName { get; set; }
        [DataMember]
        public int StyleSegmentation { get; set; }
        [DataMember]
        public string ItemImage { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int DesignID { get; set; }
        [DataMember]
        public string DesignName { get; set; }
        [DataMember]
        public string ProductDepartmentCode { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public int SubBrandID { get; set; }
        [DataMember]
        public int CollectionID { get; set; }
        [DataMember]
        public int ArmadaCollectionID { get; set; }
        [DataMember]
        public int DivisionID { get; set; }
        [DataMember]
        public int ProductGroupID { get; set; }
        [DataMember]
        public int ProductSubGroupID { get; set; }
        [DataMember]
        public int SeasonID { get; set; }
        [DataMember]
        public int YearCode { get; set; }
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
        public int SalesPriceListID { get; set; }
        [DataMember]
        public Decimal SalesPrice { get; set; }
        [DataMember]
        public int PurchaseCurrencyID { get; set; }
        [DataMember]
        public Decimal RRPPrice { get; set; }
        [DataMember]
        public int RRPCurrencyID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public int ScaleID { get; set; }
        [DataMember]
        public Boolean Franchise { get; set; }

        [DataMember]
        public Decimal ExchangeRate { get; set; }

        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int StateID { get; set; }
        [DataMember]
        public int SizeID { get; set; }
        [DataMember]
        public string SizeName { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public int VisualOrder { get; set; }

        [DataMember]
        public int ColorID { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string ColorName { get; set; }

        [DataMember]

        public List<ScaleDetailMaster> ScaleDetailMasterList { get; set; }
        [DataMember]
        public List<ColorMaster> ColorMasterList { get; set; }
        [DataMember]
        public List<ItemImageMaster> ItemImageMasterList { get; set; }
        [DataMember]
        public long StyleRunningNum { get; set; }
    
        [DataMember]
        public String SalesType { get; set; }
        [DataMember]
        public String ReturnIDs { get; set; }

        [DataMember]
        public string Composition { get; set; }

        [DataMember]
        public string SymbolGroup { get; set; }

        [DataMember]
        public string Owner { get; set; }

        [DataMember]
        public string CountryOfOrigin { get; set; }

        [DataMember]
        public string ShortDescriptionn { get; set; }

        [DataMember]
        public int DropID { get; set; }
        [DataMember]
        public string Grade { get; set; }
        [DataMember]
        public string DevelopmentOffice { get; set; }
        [DataMember]
        public string SupplierBarcode { get; set; }
        [DataMember]
        public string ArabicStyle { get; set; }
        [DataMember]
        public string ArabicSKU { get; set; }
        [DataMember]
        public List<StyleMaster> ImportExcelList { get; set; }
        [DataMember]
        public List<StyleMaster> ImportcolorExcelList { get; set; }
        [DataMember]
        public List<StyleMaster> ImportScaleExcelList { get; set; }
        [DataMember]
        public string SeasonCode { get; set; }
        [DataMember]
        public string SeasonName { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string SubBrandCode { get; set; }
        [DataMember]
        public string CollectionCode { get; set; }
        [DataMember]
        public string ArmadaCollectionCode { get; set; }
        [DataMember]
        public string DivisionCode { get; set; }
        [DataMember]
        public string ProductGroupCode { get; set; }
        [DataMember]
        public string ProductLineCode { get; set; }
        [DataMember]
        public string StyleStatusCode { get; set; }
        [DataMember]
        public string DesignerCode { get; set; }
        [DataMember]
        public string PurchasePriceListCode { get; set; }
        [DataMember]
        public string PurchaseCurrencyCode { get; set; }
        [DataMember]
        public string RRPCurrencyCode { get; set; }
        [DataMember]
        public string ScaleCode { get; set; }
        [DataMember]
        public string SegmentationCode { get; set; }
        [DataMember]
        public string DesignCode { get; set; }
        [DataMember]
        public string ProductSubGroupCode { get; set; }
    }
}
