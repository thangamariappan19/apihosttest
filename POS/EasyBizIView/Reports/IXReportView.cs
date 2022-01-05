using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Reports
{
    public interface IXReportView : IBaseView
    {
        UsersSettings UserInformation { get; }
        int StoreID { get; set; }
        int ShiftID { get; set; }
        int UserID { get; set; }
        int POSID { get; set; }
        DateTime BusinessDate { get; set; }
        List<XreportTypes> XreportTypesList { get; set; }
        List<XSubreportTypes> XSubreportList { get; set; }

        List<UsersSettings> UsersLookUp { set; }
        List<ShiftMaster> ShiftMasterLookUp { set; }
        List<PosMaster> PosMasterLookUp { set; }
        StoreMaster StoreMasterRecord { get; set; }
    }
}
