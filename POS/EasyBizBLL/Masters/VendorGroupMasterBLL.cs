using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class VendorGroupMasterBLL
    {
       public SaveVendorGroupResponse SaveVendorGroup(SaveVendorGroupRequest objRequest)
       {
           SaveVendorGroupResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseVendorGroupMasterDAL = objFactory.GetDALRepository().GetVendorGroupMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objVendorGroup = new VendorGroupMaster();
                   objVendorGroup = (VendorGroupMaster)objRequest.RequestDynamicData;
                   objRequest.VendorGroupRecord = objVendorGroup;
               }
               objResponse = (SaveVendorGroupResponse)objBaseVendorGroupMasterDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.VendorGroupRecord.ID = Convert.ToInt32(objResponse.IDs);
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.VENDORGROUP;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.VendorGroupMasterBLL", "SaveVendorGroup");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveVendorGroupResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "VendorGroup Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public UpdateVendorGroupResponse UpdateVendorGroup(UpdateVendorGroupRequest objRequest)
       {
           UpdateVendorGroupResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseVendorGroupMasterDAL = objFactory.GetDALRepository().GetVendorGroupMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objVendorGroup = new VendorGroupMaster();
                   objVendorGroup = (VendorGroupMaster)objRequest.RequestDynamicData;
                   objRequest.VendorGroupRecord = objVendorGroup;
               }
               objResponse = (UpdateVendorGroupResponse)objBaseVendorGroupMasterDAL.UpdateRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objRequest.VendorGroupRecord.ID);
                   objRequest.DocumentType = Enums.DocumentType.VENDORGROUP;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.VendorGroupMasterBLL", "UpdateVendorGroup");
               }
           }
           catch (Exception ex)
           {
               objResponse = new UpdateVendorGroupResponse();
               objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "VendorGroup Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public DeleteVendorGroupResponse DeleteVendorGroup(DeleteVendorGroupRequest objRequest)
       {
           DeleteVendorGroupResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseVendorGroupMasterDAL = objFactory.GetDALRepository().GetVendorGroupMasterDAL();
               objResponse = (DeleteVendorGroupResponse)objBaseVendorGroupMasterDAL.DeleteRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   //objRequest.DocumentIDs = Convert.ToString(objRequest.VendorGroupRecord.ID);
                   objRequest.DocumentType = Enums.DocumentType.VENDORGROUP;
                   objRequest.ProcessMode = Enums.ProcessMode.Delete;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.VendorGroupMasterBLL", "DeleteVendorGroup");
               }
           }
           catch (Exception ex)
           {
               objResponse = new DeleteVendorGroupResponse();
               objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "VendorGroup Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectAllVendorGroupResponse SelectAllRecordVendorGroup(SelectAllVendorGroupRequest objRequest)
       {
           SelectAllVendorGroupResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseVendorGroupMasterDAL = objFactory.GetDALRepository().GetVendorGroupMasterDAL();
               objResponse = (SelectAllVendorGroupResponse)objBaseVendorGroupMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllVendorGroupResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "VendorGroup Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectByVendorGroupIDResponse SelectVendorGroupRecord(SelectByVendorGroupIDRequest objRequest)
       {
           SelectByVendorGroupIDResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }   
               var objBaseVendorGroupMasterDAL = objFactory.GetDALRepository().GetVendorGroupMasterDAL();
               objResponse = (SelectByVendorGroupIDResponse)objBaseVendorGroupMasterDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByVendorGroupIDResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "VendorGroup Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectVendorGroupLookUpResponse VendorGroupLookUp(SelectVendorGroupLookUpRequest objRequest)
       {
           SelectVendorGroupLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseVendorGroupMasterDAL = objFactory.GetDALRepository().GetVendorGroupMasterDAL();
               objResponse = (SelectVendorGroupLookUpResponse)objBaseVendorGroupMasterDAL.SelectVendorGroupLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectVendorGroupLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "VendorGroup Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

      
    }
}
