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
  public  class SubCollectionMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string SubCollectionCode { get; set; }

        [DataMember]
        public string SubCollectionName { get; set; }

        [DataMember]
        public long CollectionID { get; set; }
        [DataMember]
        public string CollectionCode { get; set; }
        [DataMember]
        public string CollectionName { get; set; }
        [DataMember]
        public List<SubCollectionMaster> SubCollectionMasterlist { get; set; }
    }

}
