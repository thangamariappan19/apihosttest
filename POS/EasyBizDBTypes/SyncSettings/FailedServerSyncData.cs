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
    public class FailedServerSyncData
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int DocumentType { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public string DocumentIDs { get; set; }

        [DataMember]
        public int FromCountryID { get; set; }

        [DataMember]
        public int FromStoreID { get; set; }

        [DataMember]
        public int ProcessMode { get; set; }

        [DataMember]
        public string ProcessModeName { get; set; }

        [DataMember]
        public string BLLName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public bool SyncStatus { get; set; }

        [DataMember]
        public DateTime ProcessTime { get; set; }
    }
}
