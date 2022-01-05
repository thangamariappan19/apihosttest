using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Cardex.CardexLocationRequest
{
    [DataContract]
    [Serializable]
    public class SelectAllCardexLocationRequest : BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public DateTime FromDate { get; set; }

        [DataMember]
        public DateTime ToDate { get; set; }

        public string CardexLocationInfo { get; set; }
    }
}
