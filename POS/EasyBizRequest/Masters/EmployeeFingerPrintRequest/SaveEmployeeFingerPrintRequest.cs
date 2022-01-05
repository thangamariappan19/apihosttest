using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.EmployeeFingerPrintRequest
{
   public class SaveEmployeeFingerPrintRequest : BaseRequestType
    {
        [DataMember]
        public List<EmployeeFingerPrintMaster> EmployeeFingerPrintList { get; set; }

        [DataMember]
        public EmployeeFingerPrintMaster EmployeeFingerPrintRecord { get; set; }
    }
}
