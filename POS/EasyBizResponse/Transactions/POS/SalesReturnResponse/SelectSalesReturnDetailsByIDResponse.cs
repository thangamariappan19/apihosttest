using EasyBizDBTypes.Transactions.POS.SalesReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.SalesReturnResponse
{
    [DataContract]
    [Serializable]
    public class SelectSalesReturnDetailsByIDResponse : BaseResponseType
    {
        [DataMember]
        public List<SalesReturnDetail> SalesReturnDetailData { get; set; }
    }
}
