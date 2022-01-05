using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.SalesReturnReceipt;
using EasyBizResponse.Transactions.POS.SalesReturnReceipt;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{

    public class SalesReturnReceiptBLL 
    {
        //#region SalesReturnReceipt

        //public SalesReturnReceiptResponse GetSalesReturnReceipt(SalesReturnReceiptRequest objRequest)
        //{
        //    SalesReturnReceiptResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {
        //        var BaseSalesreturn = objFactory.GetDALRepository().GetSalesReturReceiptnDAL();
        //        objResponse = (SalesReturnReceiptResponse)BaseSalesreturn.SalesReturnReceiptdtls(objRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SalesReturnReceiptResponse();
        //        objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }
        //    return objResponse;
        //}

        //#endregion 
    }

}