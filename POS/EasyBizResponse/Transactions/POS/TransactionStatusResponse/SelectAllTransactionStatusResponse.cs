using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.TransactionStatusResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllTransactionStatusResponse:BaseResponseType
    {
        [DataMember]
        public List<TransactionStatusTypes> TransactionStatusList { get; set; }

    }
}
