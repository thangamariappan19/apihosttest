using EasyBizFactory;
using EasyBizRequest.Masters.ItemTypeMasterRequest;
using EasyBizResponse.Masters.ItemTypeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ItemTypeMasterBLL
    {

        public SelectAllItemTypeMasterResponse SelectAllItemTypeMaster(SelectAllItemTypeMasterRequest objRequest)
        {
            SelectAllItemTypeMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objItemTypeMaster = objFactory.GetDALRepository().GetItemTypeMasterDAL();
                objResponse = (SelectAllItemTypeMasterResponse)objItemTypeMaster.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllItemTypeMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Item Type Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }


    }
}
