using EasyBizFactory;
using EasyBizRequest.Masters.CountTypeMasterRequest;
using EasyBizResponse.Masters.CountTypeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class CountTypeMasterBLL
    {
        public SelectCountTypeMasterLookUpResponse SelectCountTypeMasterLookUp(SelectCountTypeMasterLookUpRequest objRequest)
        {
            SelectCountTypeMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseCountTypeMasterDAL = objFactory.GetDALRepository().GetCountTypeMasterDAL();
                objResponse = (SelectCountTypeMasterLookUpResponse)objBaseCountTypeMasterDAL.SelectCountTypeMasterLookUp(objRequest);
            
            }
            catch (Exception ex)
            {
                objResponse = new SelectCountTypeMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CountType Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
