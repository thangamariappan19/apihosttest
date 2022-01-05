using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.EmployeeMasterResponse
{
    [DataContract]
    [Serializable]
   public class GetEmployeeByStoreResponse:BaseResponseType
    {
        [DataMember]
        public EmployeeMaster EmployeeMasterRecord { get; set; }
    }
}
