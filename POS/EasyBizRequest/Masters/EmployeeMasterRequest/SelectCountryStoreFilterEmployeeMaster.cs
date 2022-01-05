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
    public class SelectCountryStoreFilterEmployeeMaster:BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string Designation { get; set; }

    }
}
