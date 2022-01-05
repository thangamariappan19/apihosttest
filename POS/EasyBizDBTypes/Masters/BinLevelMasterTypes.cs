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
    public class BinLevelMasterTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public int LevelNo { get; set; }
        [DataMember]
        public string LevelName { get; set; }
        [DataMember]
        public Boolean EnableBin { get; set; }
        [DataMember]
        public List<BinLevelMasterTypes> BinLevelMasterList { get; set; }
    }
}
