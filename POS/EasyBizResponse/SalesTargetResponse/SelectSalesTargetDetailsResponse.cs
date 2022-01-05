using EasyBizDBTypes.Transactions.POSOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.SalesTargetResponse
{
    [Serializable]
    [DataContract]
    public class SelectSalesTargetDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<SalestargetDetails> SalestargetDetailsRecord { get; set; }
    }
}
