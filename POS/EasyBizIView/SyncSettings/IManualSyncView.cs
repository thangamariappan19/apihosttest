using EasyBizDBTypes.Masters;
using EasyBizDBTypes.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.SyncSettings
{
    public interface IManualSyncView
    {
        List<CountryMaster> CountryList { get; set; }
        List<StoreMaster> StoreList { get; set; }
        string Status { get; set; }
        string ExceptionMsg { get; set; }
        List<DBConnection> ToStoreList { get; set; }
        int CountryID { get; }
        List<ClientSyncFailed> SyncFailedList { get; set; }
    }
}
