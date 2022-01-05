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
  public class SeasonMaster:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SeasonCode { get; set; }
        [DataMember]
        public string SeasonName { get; set; }
        [DataMember]
        public int SeasonDrop { get; set; }
        [DataMember]
        public DateTime SeasonStartDate { get; set; }
        [DataMember]
        public DateTime SeasonEndDate { get; set; }
        [DataMember]
        public int NoOfWeeks { get; set; }
        [DataMember]
        public int NoOfDays { get; set; }
        [DataMember]
        public Boolean IsSelected{ get; set; }
        public List<SubSeasonMaster> SubSeasonList { get; set; }

        [DataMember]
        public string SeasonCodeName { get; set; }

        public int UpdateFlag { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int UpdateBy { get; set; }
    }
}
