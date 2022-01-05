using EasyBizDBTypes.Transactions.PaymentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.PaymentDetails.CashInCashOut
{
    [DataContract]
    [Serializable]
    public class SelectByCashInCashOutIDResponse : BaseResponseType
    {
        [DataMember]
        public CashInCashOutMaster CashInCashOutMasterRecord { get; set; }
        [DataMember]
        public List<CashInCashOutDetails> CashInCashOutDetailsRecord { get; set; }
    }
}
