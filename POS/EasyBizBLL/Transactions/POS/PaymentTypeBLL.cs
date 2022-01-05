using EasyBizFactory;
using EasyBizRequest.Transactions.POS.PaymentTypeRequest;
using EasyBizResponse.Transactions.POS.PaymentTypeResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class PaymentTypeBLL
    {

        public SelectAllPaymentTypeResponse SelectAllPaymentType(SelectAllPaymentTypeRequest objRequest)
        {
            SelectAllPaymentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPaymentTypeDAL = objFactory.GetDALRepository().GetPaymentTypeDAL();
                objResponse = (SelectAllPaymentTypeResponse)objPaymentTypeDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllPaymentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Status");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
