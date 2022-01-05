using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest
{
    [DataContract]
    [Serializable]
    public abstract class BaseRequestType
    {
        public BaseRequestType()
        {
            ConnectionString = System.Configuration.ConfigurationManager.AppSettings["db_connection"];
        }

        [DataMember]
        public long RequestedByUserID { get; set; }
        [DataMember]
        public bool ShowInActiveRecords { get; set; }     

        //For Data Sync Purpose
        [DataMember]
        public Enums.RequestFrom RequestFrom { get; set; }
        public string SourceFrom { get; set; }
        public string CheckLoggedIn { get; set; }
        public string FromDeliverCode { get; set; }
        [DataMember]
        public string ConnectionString { get; set; }     
        [DataMember]
        public bool DataSync { get; set; }
        [DataMember]
        public bool IsLoggedIn { get; set; }
        [DataMember]
        public Enums.DocumentType DocumentType { get; set; }

        [DataMember]
        public string DocumentIDs { get; set; }
        [DataMember]
        public string DocumentNos { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int FromOrToCountryID { get; set; }
        [DataMember]
        public int FromOrToStoreID { get; set; }
        [DataMember]
        public String FromOrToStoreCode { get; set; }
        [DataMember]
        public String FromOrToPOSCode { get; set; }
        [DataMember]
        public Enums.ProcessMode ProcessMode { get; set; }

        [DataMember]
        public long BaseID { get; set; }
        [DataMember]
        public string type { get; set; }

        [DataMember]
        public dynamic RequestDynamicData { get; set; }

        [DataMember]
        public int BaseIntegrateStoreID { get; set; }

        [DataMember]
        public string BaseIntegrateStoreCode { get; set; }

        [DataMember]
        public Enums.SyncMode SyncMode { get; set; }

        [DataMember]
        public string StoreIDs { get; set; }
        public int IDs { get; set; }


        [DataMember]
        public string SearchString { get; set; }
        [DataMember]
        public string IsActive { get; set; }
        [DataMember]
        public string Offset { get; set; }
        [DataMember]
        public string Limit { get; set; }
    }
}
