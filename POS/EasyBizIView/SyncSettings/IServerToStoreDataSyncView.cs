using EasyBizDBTypes.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.SyncSettings
{
    public interface IServerToStoreDataSyncView :IBaseView
    {
        List<ClientSyncFailed> SyncFailedList { get; set; }
        ClientSyncFailed ClientSyncData { get; set; }
    }
}
