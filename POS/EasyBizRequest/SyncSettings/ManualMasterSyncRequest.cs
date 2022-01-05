using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using EasyBizDBTypes.SyncSettings;

namespace EasyBizRequest.SyncSettings
{
    [DataContract]
    [Serializable]
    public class ManualMasterSyncRequest : BaseRequestType
    {
        [DataMember]
        public ManualMasterSyncDBType SyncSKU { get; set; }
    }
}
