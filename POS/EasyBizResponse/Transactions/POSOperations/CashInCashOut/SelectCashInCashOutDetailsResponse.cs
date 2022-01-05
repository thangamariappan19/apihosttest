using EasyBizDBTypes.Transactions.PaymentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.PaymentDetails.CashInCashOut
{
    [Serializable]
    [DataContract]
    public class SelectCashInCashOutDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<CashInCashOutDetails> CashInCashOutDetailsRecord { get; set; }
    }
}
