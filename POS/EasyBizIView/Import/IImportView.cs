using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Import
{
    public interface IImportView : IBaseView
    {
        string ImportType { get; set; }
        List<DesignMasterTypes> ImportDesignMasterList { get; set; }
        List<SKUMasterTypes> ImportSKUMasterList { get; set; }
        List<StyleMaster> ImportStyleMasterList { get; set; }
        List<StyleMaster> ImportStyleWithColorMasterList { get; set; }
        List<StyleMaster> ImportStyleWithScaleMasterList { get; set; }
        List<StylePricing> ImportStylePricingList { get; set; }
        List<SKUMasterTypes> ImportBarCodeList { get; set; }
        List<BrandMaster> BrandLookUp { get; }
     
    }
}
