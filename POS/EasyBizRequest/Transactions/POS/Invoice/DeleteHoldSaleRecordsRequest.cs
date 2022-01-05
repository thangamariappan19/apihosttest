using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class DeleteHoldSaleRecordsRequest : BaseRequestType
    {
        [DataMember]
        public DateTime BusinessDate { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int StoreID { get; set; }
    }
}
