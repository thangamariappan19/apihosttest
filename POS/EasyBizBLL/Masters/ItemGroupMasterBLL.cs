using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.Masters.ItemGroupMasterRequest;
using EasyBizRequest.Masters.ItemTypeMasterRequest;
using EasyBizResponse.Masters.ItemGroupMasterResponse;
using EasyBizResponse.Masters.ItemTypeMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ItemGroupMasterBLL
    {

        public SelectAllItemGroupMasterResponse SelectAllGroupTypeMaster(SelectAllGroupTypeMasterRequest objRequest)
        {
            SelectAllItemGroupMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objItemGroupMaster = objFactory.GetDALRepository().GetItemGroupMasterDAL();
                objResponse = (SelectAllItemGroupMasterResponse)objItemGroupMaster.SelectAll(objRequest);
              
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllItemGroupMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Item Group Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;

        }


    }
}
