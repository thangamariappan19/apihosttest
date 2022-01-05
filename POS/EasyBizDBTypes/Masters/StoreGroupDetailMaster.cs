using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class StoreGroupDetails : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public int StoreGroupID { get; set; }
        [DataMember]
        public int ProductGroupID { get; set; }        
        [DataMember]
        public int Min { get; set; }
        [DataMember]
        public int Max { get; set; }
        [DataMember]
        public int Avg { get; set; } 

        [DataMember]
        public string ProductGroupCode { get; set; }

        [DataMember]
        public string ProductGroupName { get; set; }

        [DataMember]
        public int CreateBy { get; set; }


    }
}
