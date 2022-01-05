using EasyBizDBTypes.Transactions.POS.SalesExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.SalesExchangeResponse
{
    [DataContract]
    [Serializable]
    public class SelectAllSalesExchangeResponse :BaseResponseType
    {
        [DataMember]
        public List<SalesExchangeHeader> SalesExchangeList { get; set; }
    }
}
