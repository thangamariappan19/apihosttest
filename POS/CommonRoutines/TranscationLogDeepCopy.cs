using EasyBizDBTypes.Transactions.Coupons;
using EasyBizDBTypes.Transactions.Stocks.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static TransactionLog TransactionLogDeepCopy(TransactionLog objTransactionLog)
        {
            TransactionLog TempTransactionLog = new TransactionLog();
            TempTransactionLog.TransactionType = objTransactionLog.TransactionType;
            TempTransactionLog.BusinessDate = objTransactionLog.BusinessDate;
            TempTransactionLog.ActualDateTime = objTransactionLog.ActualDateTime;
            TempTransactionLog.DocumentID = objTransactionLog.DocumentID;
            TempTransactionLog.StyleCode = objTransactionLog.StyleCode;
            TempTransactionLog.SKUCode = objTransactionLog.SKUCode;
            TempTransactionLog.CountryID = objTransactionLog.CountryID;
            TempTransactionLog.StoreID = objTransactionLog.StoreID;
            TempTransactionLog.OutQty = objTransactionLog.OutQty;
            TempTransactionLog.InQty = objTransactionLog.InQty;
            TempTransactionLog.TransactionPrice = objTransactionLog.TransactionPrice;
            TempTransactionLog.Currency = objTransactionLog.Currency;
            TempTransactionLog.ExchangeRate = objTransactionLog.ExchangeRate;
            TempTransactionLog.DocumentPrice = objTransactionLog.DocumentPrice;
            TempTransactionLog.UserID = objTransactionLog.UserID;
            TempTransactionLog.StoreID = objTransactionLog.StoreID;
            TempTransactionLog.StoreCode = objTransactionLog.StoreCode;
            TempTransactionLog.CountryID = objTransactionLog.CountryID;
            TempTransactionLog.CountryCode = objTransactionLog.CountryCode;
            TempTransactionLog.POSCode = objTransactionLog.POSCode;
            TempTransactionLog.DocumentNo = objTransactionLog.DocumentNo;
            TempTransactionLog.SupplierBarCode = objTransactionLog.SupplierBarCode;
            TempTransactionLog.Tag_Id = objTransactionLog.Tag_Id;

            return TempTransactionLog;
        }

        public static CouponTransaction CouponTransactionDeepCopy(CouponTransaction objCouponTransaction)
        {
            CouponTransaction TempCouponTransaction = new CouponTransaction();
            TempCouponTransaction.CouponID = objCouponTransaction.CouponID;
            TempCouponTransaction.CouponCode = objCouponTransaction.CouponCode;
            TempCouponTransaction.FromLocation = objCouponTransaction.FromLocation;
            TempCouponTransaction.TransactionDate = objCouponTransaction.TransactionDate;
            TempCouponTransaction.DocumentID = objCouponTransaction.DocumentID;

            TempCouponTransaction.CouponSerialCode = objCouponTransaction.CouponSerialCode;
            TempCouponTransaction.IssuedStatus = objCouponTransaction.IssuedStatus;
            TempCouponTransaction.PhysicalStore = objCouponTransaction.PhysicalStore;
            TempCouponTransaction.RedeemedStatus = objCouponTransaction.RedeemedStatus;
            TempCouponTransaction.IsSaved = objCouponTransaction.IsSaved;
            TempCouponTransaction.ToStore = objCouponTransaction.ToStore;
            return TempCouponTransaction;
        }


        public static int_stockrequestTypes int_stockrequestDeepCopy(int_stockrequestTypes objint_stockrequestTypes)
        {
            int_stockrequestTypes Tempint_stockrequest = new int_stockrequestTypes();
            Tempint_stockrequest.DocNum = objint_stockrequestTypes.DocNum;
            Tempint_stockrequest.DocDate = objint_stockrequestTypes.DocDate;
            Tempint_stockrequest.DelDate = objint_stockrequestTypes.DelDate;
            Tempint_stockrequest.LineId = objint_stockrequestTypes.LineId;
            Tempint_stockrequest.FromLocation = objint_stockrequestTypes.FromLocation;

            Tempint_stockrequest.ToLocation = objint_stockrequestTypes.ToLocation;
            Tempint_stockrequest.Priority = objint_stockrequestTypes.Priority;
            Tempint_stockrequest.ItemCode = objint_stockrequestTypes.ItemCode;
            Tempint_stockrequest.ItemName = objint_stockrequestTypes.ItemName;
            Tempint_stockrequest.BarCode = objint_stockrequestTypes.BarCode;
            Tempint_stockrequest.Quantity = objint_stockrequestTypes.Quantity;
            Tempint_stockrequest.Remarks = objint_stockrequestTypes.Remarks;
            Tempint_stockrequest.Flag = objint_stockrequestTypes.Flag;
            return Tempint_stockrequest;
        }
        public static int_stockreturn int_stockreturnDeepCopy(int_stockreturn objint_stockreturnTypes)
        {
            int_stockreturn Tempint_stockreturn = new int_stockreturn();
            Tempint_stockreturn.DocNum = objint_stockreturnTypes.DocNum;
            Tempint_stockreturn.DocDate = objint_stockreturnTypes.DocDate;

            Tempint_stockreturn.LineId = objint_stockreturnTypes.LineId;
            Tempint_stockreturn.FromLocation = objint_stockreturnTypes.FromLocation;

            Tempint_stockreturn.ToLocation = objint_stockreturnTypes.ToLocation;

            Tempint_stockreturn.SKUCode = objint_stockreturnTypes.SKUCode;
            Tempint_stockreturn.SKUName = objint_stockreturnTypes.SKUName;
            Tempint_stockreturn.BarCode = objint_stockreturnTypes.BarCode;
            Tempint_stockreturn.Quantity = objint_stockreturnTypes.Quantity;
            Tempint_stockreturn.Remarks = objint_stockreturnTypes.Remarks;
            Tempint_stockreturn.Flag = objint_stockreturnTypes.Flag;
            return Tempint_stockreturn;
        }


    }

}
