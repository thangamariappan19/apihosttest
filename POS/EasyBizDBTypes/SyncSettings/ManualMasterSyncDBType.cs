using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.SyncSettings
{
    [DataContract]
    [Serializable]

    public class ManualMasterSyncDBType
    {
        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string Module { get; set; }

        [DataMember]
        public string StoreDBConnection { get; }
    }
}
