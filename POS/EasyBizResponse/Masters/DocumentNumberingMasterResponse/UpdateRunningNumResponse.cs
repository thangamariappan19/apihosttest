using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.DocumentNumberingMasterResponse
{
    [DataContract]
    [Serializable]
    public class UpdateRunningNumResponse : BaseResponseType
    {
        [DataMember]
        public DocumentNumberingDetails RunningNumSRecord { get; set; }
    }
}
