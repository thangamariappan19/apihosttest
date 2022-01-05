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
    public class WarehouseTypeMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string WarehouseTypeCode { get; set; }
        [DataMember]
        public string WarehouseTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Remarks { get; set; }
    }
}
