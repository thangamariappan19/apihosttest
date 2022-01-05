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
    public class ClientSyncFailed
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string FailedServer { get; set; }

        [DataMember]
        public int SyncTypeID { get; set; } 

        [DataMember]
        public int DocumentTypeID { get; set; }

        [DataMember]
        public string DocumentIDs { get; set; }

        [DataMember]
        public string DocumentNos { get; set; }

        [DataMember]
        public int ProcessModeID { get; set; }

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

        [DataMember]
        public string SyncTypeName { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public string ProcessModeName { get; set; }

        [DataMember]
        public bool Selected { get; set; }

        [DataMember]
        public Enums.RequestFrom RequestFrom { get; set; }

        [DataMember]
        public Enums.DocumentType DocumentType { get; set; }

        [DataMember]
        public Enums.SyncMode SyncMode { get; set; }

        [DataMember]
        public string BaseConnectionString { get; set; }

        [DataMember]
        public string TargetConnectionString { get; set; }
    }
    public class DataSyncLog
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string ProcessMode { get; set; }
        [DataMember]
        public int BaseStoreID { get; set; }
        [DataMember]
        public string BaseStoreCode { get; set; }
        [DataMember]
        public int ToStoreID { get; set; }
        [DataMember]
        public string ToStoreCode { get; set; }
        [DataMember]
        public bool IsSynced { get; set; }
        [DataMember]
        public DateTime SyncTime { get; set; }
    }
}
