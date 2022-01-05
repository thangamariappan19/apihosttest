using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Common
{
    [DataContract]
    [Serializable]
    public abstract class BaseType
    {
        [DataMember]
        public bool Active { get; set; }

        public bool IsDeleted { get; set; }

        [DataMember]
        public Nullable<int> SCN { get; set; }

        //the properties below will be ignored during Save operations
        [DataMember]
        public Nullable<long> CreateBy { get; set; }

        [DataMember]
        public Nullable<DateTime> CreateOn { get; set; }

        [DataMember]
        public Nullable<long> UpdateBy { get; set; }

        [DataMember]
        public Nullable<DateTime> UpdateOn { get; set; }

        [DataMember]
        public string CreatedByUserName { get; set; }

        [DataMember]
        public string UpdatedByUserName { get; set; }

        [DataMember]
        public bool IsStoreSync { get; set; }

        [DataMember]
        public bool IsCountrySync { get; set; }

        [DataMember]
        public string AppVersion { get; set; }
        [DataMember]
        public bool IsServerSync { get; set; }
        
    }
}
