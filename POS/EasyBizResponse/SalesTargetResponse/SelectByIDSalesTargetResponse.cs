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
    public class SelectByIDSalesTargetResponse : BaseResponseType
    {
        [DataMember]
        public SalesTargetHeader SalesTargetHeaderData { get; set; }
    }
}
