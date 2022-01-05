using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace EasyBizRequest.Masters.ComboOfferRequest
{
    [DataContract]
    [Serializable]
    public class SaveComboOfferRequest : BaseRequestType
    {
        [DataMember]
        public ComboOfferMaster ComboOfferRecord { get; set; }
        [DataMember]
        public CPOStyleDetails CPOStyleDetailsRecords { get; set; }
        [DataMember]
        public List<PriceListType> PriceListTypes { get; set; }
    }
}
