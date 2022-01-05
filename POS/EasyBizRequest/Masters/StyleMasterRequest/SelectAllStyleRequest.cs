using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectAllStyleRequest : BaseRequestType
    {
        public string StyleCode { get; set; }
    }
}
