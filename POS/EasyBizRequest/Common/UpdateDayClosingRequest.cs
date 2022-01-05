using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Common
{
    [DataContract]
    [Serializable]
    public class UpdateDayClosingRequest : BaseRequestType
    {
        [DataMember]
        public DayClosing DayClosingRecord { get; set; }
    }
}
