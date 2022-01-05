using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    [DataContract]
    [Serializable]
    public class InvoiceHeaderTransaction : BaseType
    {
        [DataMember]
        public long ID { get; set; }
       
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
       
        public String InvoiceNo { get; set; }
        [DataMember]
        public int TotalQty { get; set; }
        [DataMember]
       
        public Decimal TaxAmount { get; set; }
        [DataMember]
      
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public string TotalDiscountType { get; set; }
        [DataMember]
        public Decimal TotalDiscountAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscountPercentage { get; set; }
        [DataMember]
        public String BusinessDate { get; set; }
        [DataMember]
      
        public Decimal SubTotalAmount { get; set; }
        [DataMember]
        public Decimal SubTotalWithTaxAmount { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public Decimal ReceivedAmount { get; set; }
        [DataMember]
        public Decimal ReturnAmount { get; set; }

       
       
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string SalesEmployeeCode { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
      

        public string StoreCode { get; set; }
        public string PosCode { get; set; }

      
        
        [DataMember]
        public String EmployeeName { get; set; }

        [DataMember]
        public Decimal VisaAmount { get; set; }

        [DataMember]
        public Decimal KnetAmount { get; set; }

        [DataMember]
        public Decimal MasterAmount { get; set; }

        [DataMember]
        public Decimal MasteroAmount { get; set; }

        [DataMember]
        public Decimal AmexAmount { get; set; }

        [DataMember]
        public Decimal CashAmount { get; set; }

        [DataMember]
        public String InvoiceTime { get; set; }
    }
}
