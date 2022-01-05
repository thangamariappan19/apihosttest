using EasyBizDBTypes.Common;
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
    public interface IShiftTiles : IBaseView
    {
        DayClosing DayClosingRecord { get; set; }      

        DateTime BusinessDate { get; set; }

      
        UsersSettings UserInformation { get; }
        
        List<ShiftMaster> AllShiftMasterandLogList { get; set; }
        List<ShiftMaster> DayInList { get; set; }
        List<ShiftMaster> MaxShiftList { get; set; }
        ShiftMaster MaxShiftMasterID { get; set; }
        ShiftMaster MaxShiftMasterID1 { get; set; }
        ShiftLOGTypes shiftlog { get; set; }
        RetailSettingsType RetailSetting { get; set; }
        ManagerOverride DefaultManagerOverrideSetting { get; set; }
        long ManagerOverrideID { get; set; }
        ManagerOverride ManagerOverrideSetting { get; set; }
        List<ZReport> ZReportList { get; set; }
        List<ZSubReport> ZReportList1 { get; set; }
        List<Zreport2> ZReportList2 { get; set; }
      
    }
}
