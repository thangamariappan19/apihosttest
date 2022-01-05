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
    public class ItemMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string ItemDescription { get; set; }
        [DataMember]
        public string ForeignName { get; set; }
        [DataMember]
        public int StyleCode { get; set; }
        [DataMember]
        public long ColorCode { get; set; }
        [DataMember]
        public long SizeCode { get; set; }
        [DataMember]
        public int PriceListCode { get; set; }
        
    }
}
