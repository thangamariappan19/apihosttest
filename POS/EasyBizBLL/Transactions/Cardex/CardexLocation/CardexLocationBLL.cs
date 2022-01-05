using EasyBizAbsDAL.Transactions.Cardex.CardexLocation;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Cardex.CardexLocation;
using EasyBizFactory;
using EasyBizRequest.Transactions.Cardex.CardexLocationRequest;
using EasyBizResponse.Transactions.Cardex.CardexLocationResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Cardex.CardexLocation
{
    public class CardexLocationBLL
    {
        public SelectAllCardexLocationResponse SelectAllCardexLocation(SelectAllCardexLocationRequest objRequest)
        {
            SelectAllCardexLocationResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCardexLocation = objFactory.GetDALRepository().GetCardexLocation();
                objResponse = (SelectAllCardexLocationResponse)objBaseCardexLocation.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllCardexLocationResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Cardex Location");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
