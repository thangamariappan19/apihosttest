using EasyBizDBTypes.SyncSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EasyBizResponse.SyncSettings
{
    [DataContract]
    [Serializable]
    public class ManualMasterSyncResponse : BaseResponseType
    {
        [DataMember]
        public List<ManualMasterSyncDBType> StoreID { get; set; }
    }
}
