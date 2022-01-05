using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.VendorMasterRequest;
using EasyBizResponse.Masters.VendorMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class VendorMasterBLL
    {
        public SaveVendorResponse SaveVendor(SaveVendorRequest objRequest)
        {
            SaveVendorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseVendorMasterDAL = objFactory.GetDALRepository().GetVendorMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objVendor = new VendorMaster();
                    objVendor = (VendorMaster)objRequest.RequestDynamicData;
                    objRequest.VendorRecord = objVendor;
                }
                objResponse = (SaveVendorResponse)objBaseVendorMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.VendorRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.VENDOR;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.VendorMasterBLL", "SaveVendor");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveVendorResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Vendor Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateVendorResponse UpdateVendor(UpdateVendorRequest objRequest)
        {
            UpdateVendorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseVendorMasterDAL = objFactory.GetDALRepository().GetVendorMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objVendor = new VendorMaster();
                    objVendor = (VendorMaster)objRequest.RequestDynamicData;
                    objRequest.VendorRecord = objVendor;
                }
                objResponse = (UpdateVendorResponse)objBaseVendorMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.VendorRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.VENDOR;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.VendorMasterBLL", "UpdateVendor");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateVendorResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Vendor Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteVendorResponse DeleteVendor(DeleteVendorRequest objRequest)
        {
            DeleteVendorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseVendorMasterDAL = objFactory.GetDALRepository().GetVendorMasterDAL();
                objResponse = (DeleteVendorResponse)objBaseVendorMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.VendorRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.VENDOR;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.VendorMasterBLL", "DeleteVendor");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteVendorResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Vendor Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllVendorResponse SelectAllVendorRecord(SelectAllVendorRequest objRequest)
        {
            SelectAllVendorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseVendorMasterDAL = objFactory.GetDALRepository().GetVendorMasterDAL();
                objResponse = (SelectAllVendorResponse)objBaseVendorMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllVendorResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Vendor Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByVendorIDResponse SelectVendorRecord(SelectByVendorIDRequest objRequest)
        {
            SelectByVendorIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }   
                var objBaseVendorMasterDAL = objFactory.GetDALRepository().GetVendorMasterDAL();
                objResponse = (SelectByVendorIDResponse)objBaseVendorMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByVendorIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Vendor Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

     
    }
}
