using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BinSubLevelRequest
{
    [DataContract]
    [Serializable]
    public class SelectByIDBinLevelRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int LevelID { get; set; }
        [DataMember]
        public string HeaderCode { get; set; }
    }
}
