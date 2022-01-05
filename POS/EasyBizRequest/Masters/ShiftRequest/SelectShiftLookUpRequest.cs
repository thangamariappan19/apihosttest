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
   public class SelectShiftLookUpRequest : BaseRequestType
    {
        [DataMember]
        public long CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public int CashierID { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}
