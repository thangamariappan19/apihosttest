using EasyBizIView.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.SyncSettings
{
    public class StoreToServerDataSyncPresenter
    {
        IStoreToServerDataSyncView _IStoreToServerDataSyncView;
        public StoreToServerDataSyncPresenter(IStoreToServerDataSyncView ViewObj)
        {
            _IStoreToServerDataSyncView = ViewObj;
        }
    }
}
