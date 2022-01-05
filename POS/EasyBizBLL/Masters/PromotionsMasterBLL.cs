using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionCriteria;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Transactions.Promotions.PromotionCriteriaResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class PromotionsMasterBLL
    {
        public SavePromotionsResponse SavePromotions(SavePromotionsRequest objRequest)
        {
            SavePromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPromotions = new PromotionsMaster();
                    objPromotions = (PromotionsMaster)objRequest.RequestDynamicData;
                    objRequest.PromotionsRecord = objPromotions;
                    objRequest.StoreTypeList = objPromotions.StoreList;
                    objRequest.CustomerTypeList = objPromotions.CustomerList;
                    objRequest.ProductTypeList = objPromotions.ProductTypeList;
                    objRequest.BuyItemTypeList = objPromotions.BuyItemTypeList;
                    objRequest.GetItemTypeList = objPromotions.GetItemTypeList;
                   
                }
                objResponse = (SavePromotionsResponse)objBasePromotionsDAL.InsertRecord(objRequest);
                /*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PROMOTIONS;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PromotionsMasterBLL", "SavePromotions");
                }*/
            }
            catch (Exception ex)
            {
                objResponse = new SavePromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeletePromotionsResponse DeletePromotions(DeletePromotionsRequest objRequest)
        {
            DeletePromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (DeletePromotionsResponse)objBasePromotionsDAL.DeleteRecord(objRequest);
                /*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PROMOTIONS;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PromotionsMasterBLL", "DeletePromotions");
                }*/
            }
            catch (Exception ex)
            {
                objResponse = new DeletePromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPromotionsResponse SelectAllPromotionsRecords(SelectAllPromotionsRequest objRequest)
        {
            SelectAllPromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsMasterDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectAllPromotionsResponse)objBasePromotionsMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
             return objResponse;
        }

    //Yuva changes
        public SelectAllPromotionsResponse SelectPromotionWithPriorityRecords(SelectAllPromotionsRequest objRequest)
        {
            SelectAllPromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsMasterDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectAllPromotionsResponse)objBasePromotionsMasterDAL.SelectPromotionWithPriorityRecords(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPromotionsResponse API_SelectAllPromotionsRecords(SelectAllPromotionsRequest objRequest)
        {
            SelectAllPromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsMasterDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectAllPromotionsResponse)objBasePromotionsMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPromotionsResponse SelectAllStorePromotions(SelectAllPromotionsRequest objRequest)
        {
            SelectAllPromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsMasterDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectAllPromotionsResponse)objBasePromotionsMasterDAL.SelectAllStorePromotions(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdatePromotionsResponse UpdatePromotions(UpdatePromotionsRequest objRequest)
        {
            UpdatePromotionsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (UpdatePromotionsResponse)objBasePromotionsDAL.UpdateRecord(objRequest);
                /*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.PromotionsRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.PROMOTIONS;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PromotionsMasterBLL", "UpdatePromotions");
                }*/
            }
            catch (Exception ex)
            {
                objResponse = new UpdatePromotionsResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByPromotionsIDResponse SelectPromotionsRecord(SelectByPromotionsIDRequest objRequest)
        {
            SelectByPromotionsIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectByPromotionsIDResponse)objBasePromotionsDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByPromotionsIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectPromotionsLookUpResponse PromotionsLookUp(SelectPromotionsLookUpRequest objRequest)
        {
            SelectPromotionsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectPromotionsLookUpResponse)objBasePromotionsDAL.SelectPromotionsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPromotionsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByPromotionIDStoreDetailsResponse PromotionsWithStoreDetails(SelectByPromotionIDStoreDetailsRequest objRequest)
        {
            SelectByPromotionIDStoreDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectByPromotionIDStoreDetailsResponse)objBasePromotionsDAL.SelectByPromotionWithStoreDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByPromotionIDStoreDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectPromotionCriteriaResponse SelectPromotionCriteria(SelectPromotionCriteriaRequest objRequest)
        {
            SelectPromotionCriteriaResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionsDAL = objFactory.GetDALRepository().GetBasePromotionsMasterDAL();
                objResponse = (SelectPromotionCriteriaResponse)objBasePromotionsDAL.SelectPromotionCriteriaDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPromotionCriteriaResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Promotions Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
