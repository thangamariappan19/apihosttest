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
  public  class SubBrandMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string SubBrandCode { get; set; }

        [DataMember]
        public string SubBrandName { get; set; }

        [DataMember]
        public long BrandID { get; set; }
        [DataMember]
        public string BrandName { get; set; }

        public int UpdateFlag { get; set; }
        [DataMember]
        public List<SubBrandMaster> SubBrandlist { get; set; }
    }
}
