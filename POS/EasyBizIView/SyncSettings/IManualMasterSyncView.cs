using EasyBizDBTypes.Masters;
using EasyBizDBTypes.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.SyncSettings
{
    public interface IManualMasterSyncView : IBaseView
    {
        List<CountryMaster> CountryList { get; set; }
        List<StoreMaster> StoreList { get; set; }
        int CountryID { get; }
        int StoreID { get; }
        string Module { get; }
        string StoreDBConnection { get; set; }
    }
}
