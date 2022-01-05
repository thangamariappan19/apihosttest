using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPricePoint
{
    public interface IPricePointView :IBaseView
    {
        int Mode { get; }
        List<CurrencyMaster> CurrencyList { get; set; }
        List<BrandMaster> BrandList { get; set; }
        List<ExpandoObject> ExpandoObjectList { get; set; }
        List<PricePointRange> PricePointRangeList { get; set; }
        //PricePoint PricePointData { get; set; }
        string BrandCode { get;}
        string BaseCurrencyCode { get;}
        List<PricePoint> PricePointList { get;set; }
        string PricePointCode { get; set; }
        string PricePointName { get; set; }
    } 
}
