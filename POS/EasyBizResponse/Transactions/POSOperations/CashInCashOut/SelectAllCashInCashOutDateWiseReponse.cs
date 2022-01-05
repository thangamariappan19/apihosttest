using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace EasyBizResponse.Transactions.POSOperations.CashInCashOut
{
    [DataContract]
    [Serializable]
    public class SelectAllCashInCashOutDateWiseReponse:BaseResponseType
    {
        [DataMember]
        public List<CashInCashOutReportDetails> CashInCashOutReportList { get; set; }

    }
}
