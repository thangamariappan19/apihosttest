using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PriceTypeMasterResponse;
using EasyBizRequest.Masters.PriceTypeRequest;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class PriceTypeBLL
    {

        public SavePriceTypeResponse SavePriceType (SavePriceTypeRequest objRequest)
        {

            SavePriceTypeResponse objResponse = null;
            var objFactory = new DALFactory();

            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BasePriceType = objFactory.GetDALRepository().GetPriceTypeDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPriceTypeMaster = new PriceTypeMasterTypes();
                    objPriceTypeMaster = (PriceTypeMasterTypes)objRequest.RequestDynamicData;
                    objRequest.PriceTypesRecord = objPriceTypeMaster;
                }
                objResponse = (SavePriceTypeResponse)BasePriceType.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.PriceTypesRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PRICETYPEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PriceTypeBLL", "SavePriceType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SavePriceTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public SelectAllPriceTypeResponse API_SelectALL(SelectAllPriceTypeRequest requestData)
        {
            SelectAllPriceTypeResponse objResponse = null;
            var objFactory = new DALFactory();

            try
            {
                var BasePriceType = objFactory.GetDALRepository().GetPriceTypeDAL();
                objResponse = (SelectAllPriceTypeResponse)BasePriceType.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPriceTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public UpdatePriceTypeResponse UpdatePriceType(UpdatePriceTypeRequest objRequest)
        {

            UpdatePriceTypeResponse objResponse = null;
            var objFactory = new DALFactory();

            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BasePriceType = objFactory.GetDALRepository().GetPriceTypeDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPriceTypeMaster = new PriceTypeMasterTypes();
                    objPriceTypeMaster = (PriceTypeMasterTypes)objRequest.RequestDynamicData;
                    objRequest.PriceTypeData = objPriceTypeMaster;
                }
                objResponse = (UpdatePriceTypeResponse)BasePriceType.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.PriceTypeData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRICETYPEMASTER;
                        objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PriceTypeBLL", "UpdatePriceType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdatePriceTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public DeletePriceTypeResponse DeletePriceType(DeletePriceTypeRequest objRequest)
        {

            DeletePriceTypeResponse objResponse = null;
            var objFactory = new DALFactory();

            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var BasePriceType = objFactory.GetDALRepository().GetPriceTypeDAL();
                objResponse = (DeletePriceTypeResponse)BasePriceType.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.PriceTypeData.ID);
                    objRequest.DocumentType = Enums.DocumentType.PRICETYPEMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.PriceTypeBLL", "DeletePriceType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeletePriceTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SelectAllPriceTypeResponse SelectAllPriceType(SelectAllPriceTypeRequest objRequest)
        {

            SelectAllPriceTypeResponse objResponse = null;
            var objFactory = new DALFactory();

            try
            {
                var BasePriceType = objFactory.GetDALRepository().GetPriceTypeDAL();
                objResponse = (SelectAllPriceTypeResponse)BasePriceType.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPriceTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SelectByIDPriceTypeResponse SelectByIDPriceType(SelectByIDPriceTypeRequest objRequest)
        {

            SelectByIDPriceTypeResponse objResponse = null;
            var objFactory = new DALFactory();

            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                var BasePriceType = objFactory.GetDALRepository().GetPriceTypeDAL();
                objResponse = (SelectByIDPriceTypeResponse)BasePriceType.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDPriceTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
