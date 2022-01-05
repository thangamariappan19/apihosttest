using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.AFSegamationMasterRequest
{

    [DataContract]
    [Serializable]
    public class DeleteAFSegamationMasterRequest : BaseRequestType
    {
        [DataMember]
       public  int ID { get; set; }
    }
}
