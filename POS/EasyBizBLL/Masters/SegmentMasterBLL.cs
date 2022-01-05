using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizResponse.Masters.SegmentationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizAbsDAL;
using EasyBizAbsDAL.Common;
using MsSqlDAL;
using EasyBizFactory;
using ResourceStrings;
using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizBLL.Common;
using EasyBizDBTypes.Masters;
namespace EasyBizBLL.Masters
{
   public class SegmentMasterBLL
    {
       public SaveSegmentResponse SaveSegementMaster(SaveSegmentRequest objRequest)
       {
           SaveSegmentResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var BaseCollection = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objSegmentMasterTypes = new SegmentMaster();
                   objSegmentMasterTypes = (SegmentMaster)objRequest.RequestDynamicData;
                   objRequest.SegmentationRecord = objSegmentMasterTypes;
               }
               objResponse = (SaveSegmentResponse)BaseCollection.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.SEGAMENTATIONTYPES;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SegmentMasterBLL", "SaveSegementMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveSegmentResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segment Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

        public SelectAllSegmentResponse API_SelectALL(SelectAllSegmentRequest requestData)
        {
            SelectAllSegmentResponse objResponse = null;
            var _SubCollectionBLL = new SubCollectionBLL();

            var objFactory = new DALFactory();
            try
            {

                BaseSegmentationMasterDAL objBaseSegmentationMasterDAL = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();
                objResponse = (SelectAllSegmentResponse)objBaseSegmentationMasterDAL.API_SelectALL(requestData);

                //var BaseCollection = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();
                //objResponse = (SelectAllSegmentResponse)BaseCollection.SelectAll(objRequest);



            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSegmentResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segment Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateSegmentResponse UpdateSegmentMaster(UpdateSegmentRequest objRequest)
       {
           UpdateSegmentResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var BaseCollection = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objSegmentMasterTypes = new SegmentMaster();
                   objSegmentMasterTypes = (SegmentMaster)objRequest.RequestDynamicData;
                   objRequest.SegmentMasterData = objSegmentMasterTypes;
               }
               objResponse = (UpdateSegmentResponse)BaseCollection.UpdateRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objRequest.SegmentMasterData.ID);
                   objRequest.DocumentType = Enums.DocumentType.SEGAMENTATIONTYPES;
                   objRequest.ProcessMode = Enums.ProcessMode.Edit;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SegmentMasterBLL", "UpdateSegmentMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new UpdateSegmentResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segment Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
      
       public SelectAllSegmentResponse SelectAllSegmentMaster(SelectAllSegmentRequest objRequest)
       {
           SelectAllSegmentResponse objResponse = null;
           var _SubCollectionBLL = new SubCollectionBLL();

           var objFactory = new DALFactory();
           try
           {

               BaseSegmentationMasterDAL objBaseSegmentationMasterDAL = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();
               objResponse = (SelectAllSegmentResponse)objBaseSegmentationMasterDAL.SelectAll(objRequest);

               //var BaseCollection = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();
               //objResponse = (SelectAllSegmentResponse)BaseCollection.SelectAll(objRequest);
             


           }
           catch (Exception ex)
           {
               objResponse = new SelectAllSegmentResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segment Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectBySegmentIDResponse SelectByIDSegmentMaster(SelectBySegmentIDRequest objRequest)
       {
           SelectBySegmentIDResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseCollection = objFactory.GetDALRepository().GetBaseSegmentationMasterDAL();

               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }  

               objResponse = (SelectBySegmentIDResponse)BaseCollection.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectBySegmentIDResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segment Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public DeleteSengmentResponse DeleteSegmentMaster(DeleteSegmentRequest objRequest)
       {
           DeleteSengmentResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseSegmentationMasterDAL = objFactory.GetDALRepository().GetAgentMasterDAL();
               objResponse = (DeleteSengmentResponse)objBaseSegmentationMasterDAL.DeleteRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   //objRequest.DocumentIDs = Convert.ToString(objRequest.SegmentMasterData.ID);
                   objRequest.DocumentType = Enums.DocumentType.SEGAMENTATIONTYPES;
                   objRequest.ProcessMode = Enums.ProcessMode.Delete;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SegmentMasterBLL", "DeleteSegmentMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new DeleteSengmentResponse();
               objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Agent Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
