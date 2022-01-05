using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS.ISalesOrder
{
    public interface ISalesOrderView : IBaseView
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
        SalesOrderHeader SalesOrderRecord { get; set; }
        SKUMasterTypes SKURecord { set; }
        List<SalesOrderDetail> SalesOrderDetailsList { get; set; }
        List<StylePricing> StylePricingList { get; set; }
        string PriceListIDs { get; set; }
        List<PriceListType> PriceListType { get; set; }
        UsersSettings UserInformation { get; }
        Enums.ProcessMode ProcessMode { get; }
    }
}
