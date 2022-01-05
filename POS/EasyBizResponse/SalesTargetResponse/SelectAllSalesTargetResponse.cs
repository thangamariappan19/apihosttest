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
    public class SelectAllSalesTargetResponse : BaseResponseType
    {
        [DataMember]
        public List<SalesTargetHeader> SalesTargetHeaderList { get; set; }
    }
}
