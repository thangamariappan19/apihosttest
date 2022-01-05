using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesReturnReceipt
{
    [DataContract]
    [Serializable]
    public class SalesReturnReceiptRequest : BaseRequestType
    {     
        [DataMember]
        public string InvoiceNo { get; set; }
        
        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int Mode { get; set; }
    }

}