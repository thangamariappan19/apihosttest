using EasyBizFactory;
using EasyBizRequest.Transactions.POS.TransactionStatusRequest;
using EasyBizResponse.Transactions.POS.TransactionStatusResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class TransactionStatusBLL
    {
        public SelectAllTransactionStatusResponse SelectAllTransactionStatus(SelectAllTransactionStatusRequest objRequest)
        {
            SelectAllTransactionStatusResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objTransactionStatus = objFactory.GetDALRepository().GetTransactionStatusDAL();
                objResponse = (SelectAllTransactionStatusResponse)objTransactionStatus.SelectAll(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllTransactionStatusResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Transaction Status");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
