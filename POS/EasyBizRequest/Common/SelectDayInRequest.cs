using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Common
{
    [DataContract]
    [Serializable]
    public class SelectDayInRequest : BaseRequestType
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public string Mode { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}
