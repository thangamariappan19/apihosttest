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
    public class DocumentNumberingDetails : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int DocNumID { get; set; }
        [DataMember]
        public string DocumentName { get; set; }

        [DataMember]
        public string Prefix { get; set; }

        [DataMember]
        public string Suffix { get; set; }

        [DataMember]
        public int StartNumber { get; set; }

        [DataMember]
        public int EndNumber { get; set; }
        [DataMember]
        public int NumberOfCharacter { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public int DetailID { get; set; }
    }
}
