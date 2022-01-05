using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface IFindItemView :IBaseView
    {
        string SearchString { get; set; }
        int CountryID { get; }
        List<TransactionLog> StockList { set; }
        List<StylePricing> StylePricingList { get; set; }
        string SkuCode { get; set; }
        UsersSettings UserInfo { get; }
        SKUMasterTypes SKURecord { set; }
        string MainServerConnection { get; }
    }
}
