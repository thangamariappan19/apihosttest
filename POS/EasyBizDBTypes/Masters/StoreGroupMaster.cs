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
    public class StoreGroupMaster : BaseType
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string StoreGroupCode { get; set; }
        [DataMember]
        public string StoreGroupName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public List<StoreGroupDetails> StoreGroupDetailsList { get; set; }
    }
}
