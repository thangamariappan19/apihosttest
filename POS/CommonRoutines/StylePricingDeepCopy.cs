using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static StylePricing StylePricingDeepCopy(StylePricing objStylePricing)
        {
            var TempStylePricing = new StylePricing();
            TempStylePricing.SKUID = objStylePricing.SKUID;
            TempStylePricing.SKUCode = objStylePricing.SKUCode;
            TempStylePricing.PriceListID = objStylePricing.PriceListID;
            TempStylePricing.PriceListCurrency = objStylePricing.PriceListCurrency;
            TempStylePricing.Price = objStylePricing.Price;
            TempStylePricing.IsManualEntry = objStylePricing.IsManualEntry;

            return TempStylePricing;
     
        }
    }
}
