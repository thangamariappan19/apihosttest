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
    public class SubSeasonMaster : BaseType
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string SubSeasonCode { get; set; }
        [DataMember]
        public string SubSeasonName { get; set; }
        [DataMember]
        public long SeasonID { get; set; }
        [DataMember]
        public string SeasonName { get; set; }
        [DataMember]
        public List<SubSeasonMaster> SubSeasonlist { get; set; }
    }
}
