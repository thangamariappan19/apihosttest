using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterRequest
{
    [Serializable]
    [DataContract]
   public  class SaveSKUMasterRequest:BaseRequestType
    {
        [DataMember]
        public int ID;
        [DataMember]
        public List<SKUMasterTypes> SKUMasterTypesRecord { get; set; }

        [DataMember]
        public  string  SKUCode { get; set; }
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
        public bool Active { get; set; }
        [DataMember]
        public long BarCodeRunningNo { get; set; }
        [DataMember]
        public int BarCodeID { get; set; }
        [DataMember]
        public SKUMasterTypes SkuMasterData { get; set; }
        [DataMember]
        public List<SKUMasterTypes> ImportExcelList { get; set; }
    }
}
