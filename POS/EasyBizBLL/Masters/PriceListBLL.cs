using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Masters.PriceListResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class PriceListBLL
    {

        public SelectPriceListLookUPResponse PriceListLookUp(SelectPriceListLookUPRequest objRequest)
        {
            SelectPriceListLookUPResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectPriceListLookUPResponse)objPriceListDAL.SelectPriceListLookUPResponse(objRequest);
               
            }
            catch (Exception ex)
            {
                objResponse = new SelectPriceListLookUPResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectPriceListLookUPResponse API_SelectPriceListMasterLookUp(SelectPriceListLookUPRequest requestData)
        {
            SelectPriceListLookUPResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectPriceListLookUPResponse)objPriceListDAL.API_SelectPriceListMasterLookUp(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectPriceListLookUPResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectSalePriceListLookupResponse SalePriceListLookUp(SelectSalePriceListLookupRequest objRequest)
        {
            SelectSalePriceListLookupResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectSalePriceListLookupResponse)objPriceListDAL.SelectSalePriceListLookUP(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectSalePriceListLookupResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SavePriceListResponse SavePriceList(SavePriceListRequest objRequest)
        {
            SavePriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPriceListType = new PriceListType();
                    objPriceListType = (PriceListType)objRequest.RequestDynamicData;
                    objRequest.PriceListTypeRecords = objPriceListType;
                }
                objResponse = (SavePriceListResponse)objPriceListDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.PriceListTypeRecords.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PRICELIST;
                    objRequest.ProcessMode = Enums.ProcessMode.New;
                    
                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PriceListBLL", "SavePriceList");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SavePriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdatePriceListResponse UpdatePriceList(UpdatePriceListRequest objRequest)
        {
            UpdatePriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPriceListType = new PriceListType();
                    objPriceListType = (PriceListType)objRequest.RequestDynamicData;
                    objRequest.PriceListTypeRecords = objPriceListType;
                }
                objResponse = (UpdatePriceListResponse)objPriceListDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.PriceListTypeRecords.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRICELIST;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PriceListBLL", "UpdatePriceList");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdatePriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeletePriceListResponse DeletePriceList(DeletePriceListRequest objRequest)
        {
            DeletePriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (DeletePriceListResponse)objPriceListDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.PriceListTypeRecords.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRICELIST;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PriceListBLL", "DeletePriceList");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeletePriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllPriceListResponse SelectAllPriceList(SelectAllPriceListRequest objRequest)
        {
            SelectAllPriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectAllPriceListResponse)objPriceListDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPriceListResponse API_SelectAllPriceList(SelectAllPriceListRequest objRequest)
        {
            SelectAllPriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectAllPriceListResponse)objPriceListDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDPriceListResponse SelectByIDPriceList(SelectByIDPriceListRequest objRequest)
        {
            SelectByIDPriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectByIDPriceListResponse)objPriceListDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDPriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectByIDsPriceListResponse SelectByIDsPriceList(SelectByIDsPriceListRequest objRequest)
        {
            SelectByIDsPriceListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceListDAL = objFactory.GetDALRepository().GetPriceListDAL();
                objResponse = (SelectByIDsPriceListResponse)objPriceListDAL.SelectByIDs(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDsPriceListResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Price List Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
