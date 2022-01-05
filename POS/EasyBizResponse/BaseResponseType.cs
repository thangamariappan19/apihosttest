using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse
{
    [DataContract]
    [Serializable]
    public abstract class BaseResponseType
    {
        [DataMember]
        public Enums.OpStatusCode StatusCode { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string IDs { get; set; }
        [DataMember]
        public string WMSIDs { get; set; }

        [DataMember]
        public dynamic ResponseDynamicData { get; set; }

        [DataMember]

        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }

        [DataMember]
        public int RecordCount { get; set; }
    }
}
