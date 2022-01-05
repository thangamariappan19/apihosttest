using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS.API_SalesOrder
{
    [DataContract]
    [Serializable]
    public class API_SalesOrderPayments : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int SalesOrderID { get; set; }
        [DataMember]
        public string SalesOrderDocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string PaymentCurrency { get; set; }
        [DataMember]
        public string PaymentMode { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public decimal PaidAmount { get; set; }
        [DataMember]
        public decimal BalanceAmountToPay { get; set; }
        [DataMember]
        public string CardType { get; set; }
        [DataMember]
        public string CardHolderName { get; set; }
        [DataMember]
        public string ApprovalNo { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
    }
}
