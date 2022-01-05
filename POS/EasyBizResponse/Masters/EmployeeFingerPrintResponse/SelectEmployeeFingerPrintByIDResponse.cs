using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.EmployeeFingerPrintResponse
{
    [DataContract]
    [Serializable]
    public class SelectEmployeeFingerPrintByIDResponse : BaseResponseType
    {
        //[DataMember]
        //public EmployeeFingerPrintMaster EmployeeFingerPrintRecord { get; set; }
        [DataMember]
        public String EmployeeCode { get; set; }
        [DataMember]
        public String EmployeeName { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public List<EmployeeFingerPrintMaster> EmployeeFingerPrintList { get; set; }

    }
}
