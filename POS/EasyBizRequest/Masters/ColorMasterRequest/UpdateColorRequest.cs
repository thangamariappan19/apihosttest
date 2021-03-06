using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ColorMasterRequest
{
    [DataContract]
    [Serializable]
   public class UpdateColorRequest : BaseRequestType
    {
        [DataMember]
        public ColorMaster ColorRecord { get; set; }
    }
}
