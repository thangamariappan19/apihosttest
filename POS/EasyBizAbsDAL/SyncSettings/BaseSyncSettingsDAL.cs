using EasyBizAbsDAL.Common;
using EasyBizRequest.Common;
using EasyBizRequest.SyncSettings;
using EasyBizResponse.Common;
using EasyBizResponse.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.SyncSettings
{
    public abstract class BaseSyncSettingsDAL :BaseDAL
    {
        public abstract GetClientSyncFailedRecordsResponse GetClientSyncFailedRecords(GetClientSyncFailedRecordsRequest RequestObj);
        public abstract CommonUpdateResponse UpdateBaseID(CommonUpdateRequest RequestObj);
        public abstract GetDBConnectionsResponse GetDBConnectionList(GetDBConnectionsRequest RequestObj);
    }
}
