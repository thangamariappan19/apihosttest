using EasyBizDBTypes.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.SyncSettings
{
    [DataContract]
    [Serializable]
    public class GetClientSyncFailedRecordsResponse :BaseResponseType
    {
        [DataMember]
        public List<ClientSyncFailed> SyncList { get; set; }
    }
}
