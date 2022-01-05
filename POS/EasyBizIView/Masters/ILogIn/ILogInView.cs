using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DPUruNet;

namespace EasyBizIView.Masters.ILogIn
{
    public interface ILogInView :IBaseView
    {
        string UserName { get; set; }
        string Password { get; set; }
        UsersSettings UserInfo { get; set; }
        string AppliedForms { get; set; }
        List<POSScreenTypes> ScreenName { get; set; }
        List<StoreMaster> StoreMasterList { get; set; }
        int StoreID { get; set; }
        String StoreCode { get; set; }
        int POSID { get; set; }      
        List<PosMaster> PosMasterLookUp { get; set; }
        string MainServerConnection { get; }
        //string LocalServerConnection { get; }
        bool InitializeCompleted { get; set; }
        int ManagerOverrideID { set; }
        PosMaster PosMasterRecord { get; set; }
        StoreMaster StoreMasterData { get; set; }
        List<UserReport> UserReportList { get; set; }
        //CaptureResult captureResult { get; set; }
    }
}
