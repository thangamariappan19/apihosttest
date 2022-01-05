using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.SalesTargetResponse
{
    [DataContract]
    [Serializable]
    public class SalestargetHistoryResponse : BaseResponseType
    {
        [DataMember]
        public List<SalestargetDetails> SalestargetDetailsList { get; set; }
    }
}
