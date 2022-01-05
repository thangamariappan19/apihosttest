using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizFactory;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionPriority;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Transactions.Promotions.PromotionPriority;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Promotions
{
    public class PromotionPriorityBLL
    {

        public SavePromotionPriorityResponse SavePromotionPriority(SavePromotionPriorityRequest objRequest)
        {
            SavePromotionPriorityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetBasePromotionPriorityDAL();
                if (objRequest.RequestDynamicData != null)
                {
					var objPromotionPriorityType = new List<PromotionPriorityType>();
					objPromotionPriorityType = (List<PromotionPriorityType>) objRequest.RequestDynamicData;
					objRequest.PromotionPriorityTypeData = objPromotionPriorityType;
                }
                objResponse = (SavePromotionPriorityResponse)objBasePromotionPriorityDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PROMOTIONSPRIORITY;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Promotions.PromotionPriorityBLL", "SavePromotionPriority");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SavePromotionPriorityResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotion Priority");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllPromotionPriorityResponse SelectAllPromotionPriority(SelectAllPromotionPriorityRequest objRequest)
        {
            SelectAllPromotionPriorityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetBasePromotionPriorityDAL();
                objResponse = (SelectAllPromotionPriorityResponse)objBasePromotionPriorityDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionPriorityResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotion Priority");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDPromotionPriorityResponse SelectByIDs(SelectByIDPromotionPriorityRequest objRequest)
        {
            SelectByIDPromotionPriorityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetBasePromotionPriorityDAL();
                objResponse = (SelectByIDPromotionPriorityResponse)objBasePromotionPriorityDAL.SelectByIDs(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDPromotionPriorityResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotions Priority");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

		//// Changed by Senthamil @ 11.09.2018
        //public SelectAllPromotionsResponse SelectAllPromotionsRecords(SelectAllPromotionsRequest objRequest)
        //{
        //    SelectAllPromotionsResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {
        //        var objBasePromotionsMasterDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
        //        objResponse = (SelectAllPromotionsResponse)objBasePromotionsMasterDAL.SelectAll(objRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SelectAllPromotionsResponse();
        //        objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }
        //    return objResponse;
        //}
        public SelectAllPromotionPriorityResponse SelectAllPromotionsRecords(SelectAllPromotionPriorityRequest objRequest)
        {
            SelectAllPromotionPriorityResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetBasePromotionPriorityDAL();
                objResponse = (SelectAllPromotionPriorityResponse)objBasePromotionPriorityDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionPriorityResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Priority");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
