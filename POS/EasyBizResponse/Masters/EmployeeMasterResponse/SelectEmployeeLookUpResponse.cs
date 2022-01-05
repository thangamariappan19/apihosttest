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
    public class SelectEmployeeLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<EmployeeMaster> EmployeeList { get; set; }
    }
}
