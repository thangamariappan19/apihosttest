using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOS
{
    public interface ICloseSales : IBaseView
    {
        UsersSettings UserInformation { get; }
        int ShiftID { get; set; }
        int POSID { get; set; }
        DateTime BusinessDate { get; set; }
        RetailSettingsType RetailSetting { get; set; }
        Decimal ShiftAmount { get; set; }
        Decimal amount { get; set; }
        Decimal Cardamount { get; set; }
        Decimal Shiftlogamount { get; set; }
        Decimal SalesRetunAmount { get; set; }
        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        long ManagerOverrideID { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }        
        List<XreportTypes> XreportTypesList { get; set; }
        List<ZReport> ZReportList { get; set; }
        List<ZSubReport> ZReportList1 { get; set; }
        List<ShiftMaster> AllShiftMasterandLogList { get; set; }
        Decimal CashIn { get; set; }
        Decimal ReturnAmount { get; set; }
        Decimal CashOut { get; set; }
        List<XSubreportTypes> XSubreportList { get; set; }
        int StoreID { get; }       
    }
}
