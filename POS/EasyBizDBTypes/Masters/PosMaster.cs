using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class PosMaster : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public string PosCode { get; set; }
        [DataMember]
        public string PosName { get; set; }

        [DataMember]
        public int StoreID { get; set; }      
        [DataMember]
        public int CountryID { get; set; }        

        [DataMember]
        public int StoreGroupID { get; set; }

        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string StoreGroupName { get; set; }

        [DataMember]
        public string StoreName { get; set; }  

        [DataMember]
        public string PrinterDeviceName { get; set; }

        [DataMember]
        public int DefaultCustomer { get; set; }
        [DataMember]
        public string PoleDisplayPort { get; set; }
        [DataMember]
        public string DisplayLineMsgOne { get; set; }
        [DataMember]
        public string DisplayLineMsgTwo { get; set; }
        [DataMember]
        public string DiskID { get; set; }
        [DataMember]
        public string CPUID { get; set; }

        [DataMember]
        public String StoreCode { get; set; }
        [DataMember]
        public String CountryCode { get; set; }

        [DataMember]
        public String StoreGroupCode { get; set; }
        [DataMember]
        public String DefaultCustomerCode { get; set; }
        
    }
}
