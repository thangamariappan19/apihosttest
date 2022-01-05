using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.SyncSettings
{
    [DataContract]
    [Serializable]
    public class GetClientSyncFailedRecordsRequest :BaseRequestType
    {
        [DataMember]
        public bool SyncStatus { get; set; }
    }
}
