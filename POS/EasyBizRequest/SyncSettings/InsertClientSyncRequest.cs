using EasyBizDBTypes.SyncSettings;
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
    public class InsertClientSyncRequest :BaseRequestType
    {
        [DataMember]
        public ClientSyncFailed ClientSyncData { get; set; }
    }
}
