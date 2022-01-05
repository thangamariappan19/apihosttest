using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class FindStockByCountryResponse : BaseResponseType
    {
        [DataMember]       
        public List<FindStockByCountry> StockByCountryList { get; set; }
    }
}
