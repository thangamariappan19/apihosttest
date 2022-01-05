using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizFactory;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Promotions
{
    public class WNPromotionBLL
    {
        public SaveWNPromotionResponse SaveWNPromotion(SaveWNPromotionRequest objRequest)
        {
            SaveWNPromotionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var WNPromotionRecord = new WNPromotion();
                    WNPromotionRecord = (WNPromotion)objRequest.RequestDynamicData;
                    objRequest.WNPromotionData = WNPromotionRecord;
                }
                objResponse = (SaveWNPromotionResponse)objBasePromotionPriorityDAL.InsertRecord(objRequest);
                /*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.WNPROMOTION;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Promotions.WNPromotionBLL", "SaveWNPromotion");
                }*/
            }
            catch (Exception ex)
            {
                objResponse = new SaveWNPromotionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WN Promotion");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllWNPromotionResponse SelectAllWNPromotion(SelectAllWNPromotionRequest objRequest)
        {
            SelectAllWNPromotionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                objResponse = (SelectAllWNPromotionResponse)objBasePromotionPriorityDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWNPromotionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WN Promotion");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllWNPromotionResponse API_SelectALLWN(SelectAllWNPromotionRequest requestData)
        {
            SelectAllWNPromotionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                objResponse = (SelectAllWNPromotionResponse)objBasePromotionPriorityDAL.API_SelectALLWN(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllWNPromotionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WN Promotion");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectWNPromotionByIDResponse SelectWNPromotionRecord(SelectWNPromotionByIDRequest objRequest)
        {
            SelectWNPromotionByIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectWNPromotionByIDResponse)objBasePromotionPriorityDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectWNPromotionByIDResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WN Promotion");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectWNPromotionLookUpResponse API_SelectALL(SelectWNPromotionLookUpRequest requestData)
        {
            SelectWNPromotionLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                objResponse = (SelectWNPromotionLookUpResponse)objBasePromotionPriorityDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectWNPromotionLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "WNPromotion ");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectWNPromotionLookUpResponse WNPromotionLookUp(SelectWNPromotionLookUpRequest objRequest)
        {
            SelectWNPromotionLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                objResponse = (SelectWNPromotionLookUpResponse)objBasePromotionPriorityDAL.SelectWNPromotionLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectWNPromotionLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "WNPromotion ");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectWNPromotionDetailsResponse GetWNPrice(SelectWNPromotionDetailsRequest objRequest)
        {
            SelectWNPromotionDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionPriorityDAL = objFactory.GetDALRepository().GetWNPromotionDAL();
                objResponse = (SelectWNPromotionDetailsResponse)objBasePromotionPriorityDAL.GetWNPrice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectWNPromotionDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "WN Promotion");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
