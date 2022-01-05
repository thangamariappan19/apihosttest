using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.FreightMasterRequest;
using EasyBizResponse.Masters.FreightMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class FreightMasterBLL
    {
        public SaveFreightMasterResponse SaveFreightMaster(SaveFreightMasterRequest objRequest)
        {
            SaveFreightMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseFreightMasterDAL = objFactory.GetDALRepository().GetFreightMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objFreightMaster = new FreightMaster();
                    objFreightMaster = (FreightMaster)objRequest.RequestDynamicData;
                    objRequest.FreightMasterData = objFreightMaster;
                }
                objResponse = (SaveFreightMasterResponse)objBaseFreightMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.FreightMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.FREIGHT;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.FreightMasterBLL", "SaveFreightMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveFreightMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Freight Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllFreightMasterResponse SelectAllFreightMaster(SelectAllFreightMasterRequest objRequest)
        {
            SelectAllFreightMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseFreightMasterDAL = objFactory.GetDALRepository().GetFreightMasterDAL();
                objResponse = (SelectAllFreightMasterResponse)objBaseFreightMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllFreightMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Freight Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDFreightMasterResponse SelectFreightMasterRecord(SelectByIDFreightMasterRequest objRequest)
        {
            SelectByIDFreightMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseFreightMasterDAL = objFactory.GetDALRepository().GetFreightMasterDAL();
                objResponse = (SelectByIDFreightMasterResponse)objBaseFreightMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDFreightMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Freight Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateFreightMasterResponse UpdateFreightMaster(UpdateFreightMasterRequest objRequest)
        {
            UpdateFreightMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseFreightMasterDAL = objFactory.GetDALRepository().GetFreightMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objFreightMaster = new FreightMaster();
                    objFreightMaster = (FreightMaster)objRequest.RequestDynamicData;
                    objRequest.FreightMasterData = objFreightMaster;
                }
                objResponse = (UpdateFreightMasterResponse)objBaseFreightMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.FreightMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.FREIGHT;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.FreightMasterBLL", "UpdateFreightMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateFreightMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Freight Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteFreightMasterResponse DeleteFreightMaster(DeleteFreightMasterRequest objRequest)
        {
            DeleteFreightMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseFreightMasterDAL = objFactory.GetDALRepository().GetFreightMasterDAL();
                objResponse = (DeleteFreightMasterResponse)objBaseFreightMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.FreightMasterData.ID);
                    //objRequest.DocumentType = Enums.DocumentType.FREIGHT;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.FreightMasterBLL", "DeleteFreightMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteFreightMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Freight Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectFreightMasterLookUpResponse SelectFreightMasterLookUp(SelectFreightMasterLookUpRequest objRequest)
        {
            SelectFreightMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseFreightMasterDAL = objFactory.GetDALRepository().GetFreightMasterDAL();
                objResponse = (SelectFreightMasterLookUpResponse)objBaseFreightMasterDAL.SelectFreightMasterLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectFreightMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Freight Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
