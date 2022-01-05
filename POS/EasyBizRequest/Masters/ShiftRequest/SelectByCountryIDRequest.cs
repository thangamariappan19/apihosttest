using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ShiftRequest
{
    [DataContract]
    [Serializable]
    public class SelectByCountryIDRequest : BaseRequestType
    {
        [DataMember]
        public long CountryID { get; set; }

       
    }
}
