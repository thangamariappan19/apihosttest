using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.EmployeeDiscountInfoResponse
{
    [DataContract]
    [Serializable]
    public class SelectEmployeeDiscountInfoResponseByCustCode : BaseResponseType
    {
        [DataMember]
        public List<EmployeeDiscountInfo> EmployeeDiscountInfoList { get; set; }
    }
}
