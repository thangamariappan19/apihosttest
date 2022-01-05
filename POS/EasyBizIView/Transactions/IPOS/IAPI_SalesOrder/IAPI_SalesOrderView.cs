using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.IAPI_SalesOrder
{
    public interface IAPI_SalesOrderView
    {
        string DocumentNo { set; }
        string CustomerSearchString { get; }
        int CustomerID { get; set; }
        List<CustomerMaster> CustomerMasterList { get; set; }
        string SKUSearchString { get; }
        long ManagerOverrideID { get; set; }
        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }
        int ID { get; }
        API_SalesOrderHeader SalesOrderRecord { get; set; }
        SKUMasterTypes SKURecord { set; }
        List<API_SalesOrderDetails> SalesOrderDetailsList { get; set; }
        List<StylePricing> StylePricingList { get; set; }
        string PriceListIDs { get; set; }
        List<PriceListType> PriceListType { get; set; }
        UsersSettings UserInformation { get; }
        Enums.ProcessMode ProcessMode { get; }
    }
}
