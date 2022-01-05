using EasyBizDBTypes.Transactions.PaymentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.PaymentDetails
{
    [DataContract]
    [Serializable]
    public class SaveCashInCashOutRequest : BaseRequestType
    {
        [DataMember]
        public CashInCashOutMaster CashInCashOutMasterRecord { get; set; }
        [DataMember]
        public List<CashInCashOutDetails> CashInCashOutDetailsList { get; set; }
    }
}
