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
   public class StyleDetails : BaseType
    {
        [DataMember]
        public int StyleID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string StyleName { get; set; }
        [DataMember]
        public string ShortDesignName { get; set; }
        [DataMember]
        public int StyleSegmentation { get; set; }

        [DataMember]
        public int DesignCode { get; set; }
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
        public int PurchaseCurrencyID { get; set; }
        [DataMember]
        public Decimal RRPPrice { get; set; }
        [DataMember]
        public int RRPCurrencyID { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public string SizeDescription { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public int ScaleID { get; set; }
        [DataMember]
        public Boolean ScaleSelect { get; set; }
    }
}
