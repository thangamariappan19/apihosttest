using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static OnAccountPayment OnAccountPaymentDeepCopy(OnAccountPayment objOnAccountPayment)
        {
            OnAccountPayment TempOnAccountPayment = new OnAccountPayment();
            TempOnAccountPayment.ID = objOnAccountPayment.ID;
            TempOnAccountPayment.StoreID = objOnAccountPayment.StoreID;
            TempOnAccountPayment.StoreCode = objOnAccountPayment.StoreCode;
            TempOnAccountPayment.CustomerCode = objOnAccountPayment.CustomerCode;
            TempOnAccountPayment.PaymentDate = objOnAccountPayment.PaymentDate;
            TempOnAccountPayment.BillingAmount = objOnAccountPayment.BillingAmount;
            TempOnAccountPayment.ReceivedAmount = objOnAccountPayment.ReceivedAmount;
            TempOnAccountPayment.ReturnAmount = objOnAccountPayment.ReturnAmount;
            TempOnAccountPayment.CreateBy = objOnAccountPayment.CreateBy;

            TempOnAccountPayment.OnAccountPaymentDetailsList = objOnAccountPayment.OnAccountPaymentDetailsList;
            TempOnAccountPayment.OnAcInvoiceWisePaymentList = objOnAccountPayment.OnAcInvoiceWisePaymentList;

            return TempOnAccountPayment;
        }
        public static List<OnAccountPayment> OnAccountPaymentListDeepCopy(List<OnAccountPayment> objOnAccountPaymentList)
        {
            List<OnAccountPayment> TemepOnAccountPaymentList = new List<OnAccountPayment>();

            foreach (OnAccountPayment objOnAccountPayment in objOnAccountPaymentList)
            {
                OnAccountPayment TempOnAccountPayment = new OnAccountPayment();
                TempOnAccountPayment.ID = objOnAccountPayment.ID;
                TempOnAccountPayment.StoreID = objOnAccountPayment.StoreID;
                TempOnAccountPayment.StoreCode = objOnAccountPayment.StoreCode;
                TempOnAccountPayment.CustomerCode = objOnAccountPayment.CustomerCode;
                TempOnAccountPayment.PaymentDate = objOnAccountPayment.PaymentDate;
                TempOnAccountPayment.BillingAmount = objOnAccountPayment.BillingAmount;
                TempOnAccountPayment.ReceivedAmount = objOnAccountPayment.ReceivedAmount;
                TempOnAccountPayment.ReturnAmount = objOnAccountPayment.ReturnAmount;
                TemepOnAccountPaymentList.Add(TempOnAccountPayment);
            }
            return TemepOnAccountPaymentList;
        }
        public static OnAccountPaymentDetails OnAccountPaymentDetailsDeepCopy(OnAccountPaymentDetails objOnAccountPaymentDetails)
        {
            OnAccountPaymentDetails TempOnAccountPaymentDetails = new OnAccountPaymentDetails();
            TempOnAccountPaymentDetails.ID = objOnAccountPaymentDetails.ID;
            TempOnAccountPaymentDetails.OnAccountPaymentID = objOnAccountPaymentDetails.OnAccountPaymentID;
            TempOnAccountPaymentDetails.StoreID = objOnAccountPaymentDetails.StoreID;
            TempOnAccountPaymentDetails.StoreCode = objOnAccountPaymentDetails.StoreCode;
            TempOnAccountPaymentDetails.PaymentType = objOnAccountPaymentDetails.PaymentType;
            TempOnAccountPaymentDetails.PaymentCurrency = objOnAccountPaymentDetails.PaymentCurrency;
            TempOnAccountPaymentDetails.ChangeCurrency = objOnAccountPaymentDetails.ChangeCurrency;
            TempOnAccountPaymentDetails.CardType = objOnAccountPaymentDetails.CardType;
            TempOnAccountPaymentDetails.CardNumber = objOnAccountPaymentDetails.CardNumber;
            TempOnAccountPaymentDetails.CardHolderName = objOnAccountPaymentDetails.CardHolderName;
            TempOnAccountPaymentDetails.ApprovalNumber = objOnAccountPaymentDetails.ApprovalNumber;
            TempOnAccountPaymentDetails.ReceivedAmount = objOnAccountPaymentDetails.ReceivedAmount;
            return TempOnAccountPaymentDetails;
        }
        public static List<OnAccountPaymentDetails> OnAccountPaymentDetailsListDeepCopy(List<OnAccountPaymentDetails> objOnAccountPaymentDetailsList)
        {
            List<OnAccountPaymentDetails> TempOnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
            foreach (OnAccountPaymentDetails objOnAccountPaymentDetails in objOnAccountPaymentDetailsList)
            {
                OnAccountPaymentDetails TempOnAccountPaymentDetails = new OnAccountPaymentDetails();
                TempOnAccountPaymentDetails.ID = objOnAccountPaymentDetails.ID;
                TempOnAccountPaymentDetails.OnAccountPaymentID = objOnAccountPaymentDetails.OnAccountPaymentID;
                TempOnAccountPaymentDetails.StoreID = objOnAccountPaymentDetails.StoreID;
                TempOnAccountPaymentDetails.StoreCode = objOnAccountPaymentDetails.StoreCode;
                TempOnAccountPaymentDetails.PaymentType = objOnAccountPaymentDetails.PaymentType;
                TempOnAccountPaymentDetails.PaymentCurrency = objOnAccountPaymentDetails.PaymentCurrency;
                TempOnAccountPaymentDetails.ChangeCurrency = objOnAccountPaymentDetails.ChangeCurrency;
                TempOnAccountPaymentDetails.CardType = objOnAccountPaymentDetails.CardType;
                TempOnAccountPaymentDetails.CardNumber = objOnAccountPaymentDetails.CardNumber;
                TempOnAccountPaymentDetails.CardHolderName = objOnAccountPaymentDetails.CardHolderName;
                TempOnAccountPaymentDetails.ApprovalNumber = objOnAccountPaymentDetails.ApprovalNumber;
                TempOnAccountPaymentDetails.ReceivedAmount = objOnAccountPaymentDetails.ReceivedAmount;
                TempOnAccountPaymentDetailsList.Add(TempOnAccountPaymentDetails);
            }
            return TempOnAccountPaymentDetailsList;
        }
        public static OnAcInvoiceWisePayment OnAcInvoiceWisePaymentDeepCopy(OnAcInvoiceWisePayment objOnAcInvoiceWisePayment)
        {
            OnAcInvoiceWisePayment TempOnAcInvoiceWisePayment = new OnAcInvoiceWisePayment();
            TempOnAcInvoiceWisePayment.SlNo = objOnAcInvoiceWisePayment.SlNo;
            TempOnAcInvoiceWisePayment.ID = objOnAcInvoiceWisePayment.ID;
            TempOnAcInvoiceWisePayment.OnAccountPaymentID = objOnAcInvoiceWisePayment.OnAccountPaymentID;
            TempOnAcInvoiceWisePayment.PurchaseStoreID = objOnAcInvoiceWisePayment.PurchaseStoreID;
            TempOnAcInvoiceWisePayment.PurchaseStoreCode = objOnAcInvoiceWisePayment.PurchaseStoreCode;
            TempOnAcInvoiceWisePayment.BusinessDate = objOnAcInvoiceWisePayment.BusinessDate;            
            TempOnAcInvoiceWisePayment.InvoiceNo = objOnAcInvoiceWisePayment.InvoiceNo;
            TempOnAcInvoiceWisePayment.CustomerCode = objOnAcInvoiceWisePayment.CustomerCode;
            TempOnAcInvoiceWisePayment.BillAmount = objOnAcInvoiceWisePayment.BillAmount;
            TempOnAcInvoiceWisePayment.CashPaid = objOnAcInvoiceWisePayment.CashPaid;
            TempOnAcInvoiceWisePayment.CardPaid = objOnAcInvoiceWisePayment.CardPaid;
            TempOnAcInvoiceWisePayment.TotalPaid = objOnAcInvoiceWisePayment.TotalPaid;
            TempOnAcInvoiceWisePayment.PendingAmount = objOnAcInvoiceWisePayment.PendingAmount;
            TempOnAcInvoiceWisePayment.IsSelect = objOnAcInvoiceWisePayment.IsSelect;
            TempOnAcInvoiceWisePayment.CloseBill = objOnAcInvoiceWisePayment.CloseBill;
            TempOnAcInvoiceWisePayment.DiscountAmount = objOnAcInvoiceWisePayment.DiscountAmount;
            TempOnAcInvoiceWisePayment.PaidAmount = objOnAcInvoiceWisePayment.PaidAmount;
            TempOnAcInvoiceWisePayment.StoreID = objOnAcInvoiceWisePayment.StoreID;
            TempOnAcInvoiceWisePayment.StoreCode = objOnAcInvoiceWisePayment.StoreCode;
            return TempOnAcInvoiceWisePayment;
        }
        public static List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentListDeepCopy(List<OnAcInvoiceWisePayment> objOnAcInvoiceWisePaymentList)
        {
            List<OnAcInvoiceWisePayment> TempOnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
            foreach (OnAcInvoiceWisePayment objOnAcInvoiceWisePayment in objOnAcInvoiceWisePaymentList)
            {
                OnAcInvoiceWisePayment TempOnAcInvoiceWisePayment = new OnAcInvoiceWisePayment();
                TempOnAcInvoiceWisePayment.SlNo = objOnAcInvoiceWisePayment.SlNo;
                TempOnAcInvoiceWisePayment.ID = objOnAcInvoiceWisePayment.ID;
                TempOnAcInvoiceWisePayment.OnAccountPaymentID = objOnAcInvoiceWisePayment.OnAccountPaymentID;
                TempOnAcInvoiceWisePayment.PurchaseStoreID = objOnAcInvoiceWisePayment.PurchaseStoreID;
                TempOnAcInvoiceWisePayment.PurchaseStoreCode = objOnAcInvoiceWisePayment.PurchaseStoreCode;
                TempOnAcInvoiceWisePayment.BusinessDate = objOnAcInvoiceWisePayment.BusinessDate;
                TempOnAcInvoiceWisePayment.InvoiceNo = objOnAcInvoiceWisePayment.InvoiceNo;
                TempOnAcInvoiceWisePayment.CustomerCode = objOnAcInvoiceWisePayment.CustomerCode;
                TempOnAcInvoiceWisePayment.BillAmount = objOnAcInvoiceWisePayment.BillAmount;
                TempOnAcInvoiceWisePayment.CashPaid = objOnAcInvoiceWisePayment.CashPaid;
                TempOnAcInvoiceWisePayment.CardPaid = objOnAcInvoiceWisePayment.CardPaid;
                TempOnAcInvoiceWisePayment.TotalPaid = objOnAcInvoiceWisePayment.TotalPaid;
                TempOnAcInvoiceWisePayment.PendingAmount = objOnAcInvoiceWisePayment.PendingAmount;
                TempOnAcInvoiceWisePayment.IsSelect = objOnAcInvoiceWisePayment.IsSelect;
                TempOnAcInvoiceWisePayment.CloseBill = objOnAcInvoiceWisePayment.CloseBill;
                TempOnAcInvoiceWisePayment.DiscountAmount = objOnAcInvoiceWisePayment.DiscountAmount;
                TempOnAcInvoiceWisePayment.PaidAmount = objOnAcInvoiceWisePayment.PaidAmount;
                TempOnAcInvoiceWisePayment.StoreID = objOnAcInvoiceWisePayment.StoreID;
                TempOnAcInvoiceWisePayment.StoreCode = objOnAcInvoiceWisePayment.StoreCode;

                TempOnAcInvoiceWisePaymentList.Add(TempOnAcInvoiceWisePayment);
            }
            return TempOnAcInvoiceWisePaymentList;
        }
    }
}