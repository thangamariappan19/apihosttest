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
    public class ProductLineMaster : BaseType
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ProductLineCode { get; set; }
        [DataMember]
        public string ProductLineName { get; set; }

        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
    }
}
