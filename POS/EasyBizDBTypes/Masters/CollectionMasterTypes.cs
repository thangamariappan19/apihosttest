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
    public class CollectionMasterTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CollectionCode { get; set; }
        [DataMember]
        public string CollectionName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        public List<SubCollectionMaster> SubCollectionMasterList { get; set; }


        public int UpdateFlag { get; set; }
    }
}
