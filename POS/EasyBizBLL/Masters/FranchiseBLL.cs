using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.FranchiseRequest;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.FranchiseResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class FranchiseBLL
    {
       public saveFranchiseResponse SaveFranchise(SaveFranchiseMasterRequest objRequest)
        {
            saveFranchiseResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.FranchiseTypeData = (FranchiseType)objRequest.RequestDynamicData;
                }
                var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objFranchise = new FranchiseType();
                    objFranchise = (FranchiseType)objRequest.RequestDynamicData;
                    objRequest.FranchiseTypeData = objFranchise;

                }
                objResponse = (saveFranchiseResponse)objBaseFranchiseMasterDAL.InsertRecord(objRequest);

                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.FranchiseTypeData.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentNos = objRequest.FranchiseTypeData.FranchiseCode;
                //    objRequest.DocumentType = Enums.DocumentType.FRANCHISEMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.FranchiseBLL", "SaveFranchise");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new saveFranchiseResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Franchise Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllfranchiseResponse API_SelectALL(SelectAllFranchiseMasterRequest requestData)
        {
            SelectAllfranchiseResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
                objResponse = (SelectAllfranchiseResponse)objBaseFranchiseMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllfranchiseResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Franchise Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateFranchiseResponse UpdateFranchise(UpdateFranchiseMasterRequest objRequest)
        {
            UpdateFranchiseResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objFranchise = new FranchiseType();
                    objFranchise = (FranchiseType)objRequest.RequestDynamicData;
                    objRequest.FranchiseTypeRecord = objFranchise;

                }
                objResponse = (UpdateFranchiseResponse)objBaseFranchiseMasterDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.FranchiseTypeRecord.ID);
                //    objRequest.DocumentType = Enums.DocumentType.FRANCHISEMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.FranchiseBLL", "UpdateFranchise");
                //}

            }
            catch (Exception ex)
            {
                objResponse = new UpdateFranchiseResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Franchise Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
       public SelectAllfranchiseResponse SelectAllFranchise(SelectAllFranchiseMasterRequest objRequest)
       {
           SelectAllfranchiseResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
               objResponse = (SelectAllfranchiseResponse)objBaseFranchiseMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllfranchiseResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Franchise Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectFranchiseLookupResponse API_SelectFranchiseMasterLookUp(SelectFranchiseLookUpRequest requestData)
        {
            SelectFranchiseLookupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
                objResponse = (SelectFranchiseLookupResponse)objBaseFranchiseMasterDAL.API_SelectFranchiseMasterLookUp(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectFranchiseLookupResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectFranchiseLookupResponse FranchiseLookUp(SelectFranchiseLookUpRequest objRequest)
       {
           SelectFranchiseLookupResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
               objResponse = (SelectFranchiseLookupResponse)objBaseFranchiseMasterDAL.SelectFranchiseLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectFranchiseLookupResponse();
               objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tax Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectByIDFranchiseResponse SelectFranchiseRecord(SelectByIDFranchiseRequest objRequest)
       {
           SelectByIDFranchiseResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }
               var objBaseFranchiseMasterDAL = objFactory.GetDALRepository().GetFranchiseMasterDAL();
               objResponse = (SelectByIDFranchiseResponse)objBaseFranchiseMasterDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDFranchiseResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Franchise Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
