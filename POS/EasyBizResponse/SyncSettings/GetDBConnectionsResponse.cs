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
    public class GetDBConnectionsResponse :BaseResponseType
    {
        [DataMember]
        public List<DBConnection> DBConnectionList { get; set; }
    }
}
