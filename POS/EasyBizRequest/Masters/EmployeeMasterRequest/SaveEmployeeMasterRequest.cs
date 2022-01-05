using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.EmployeeMasterRequest
{
    [DataContract]
    [Serializable]
   public class SaveEmployeeMasterRequest:BaseRequestType
    {
        [DataMember]
        public EmployeeMaster EmployeeMasterRecord { get; set; }
    }
}
