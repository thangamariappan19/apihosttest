using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using EasyBizResponse.Masters.SubCollectionResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class SubCollectionBLL
    {
       public SaveSubCollectionResponse SaveSubCollection(SaveSubCollectionRequest objRequest)
        {
            SaveSubCollectionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    // Changed By Senthamil @ 11.09.2018
                    //var objSubCollectionMaster = new SubCollectionMaster();
                    //objSubCollectionMaster = (SubCollectionMaster)objRequest.RequestDynamicData;
                    //objRequest.SubCollectionMasterlist = objSubCollectionMaster.SubCollectionMasterlist;

                    var objSubCollectionMaster = new List<SubCollectionMaster>();
                    objSubCollectionMaster.AddRange(objRequest.RequestDynamicData);
                    objRequest.SubCollectionMasterlist = objSubCollectionMaster;
                }
                objResponse = (SaveSubCollectionResponse)objBaseSubCollectionDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.SUBCOLLECTIONMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubCollectionBLL", "SaveSubCollection");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveSubCollectionResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sub Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSubCollectionResponse API_SelectALL(SelectAllSubCollectionRequest requestData)
        {
            SelectAllSubCollectionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
                objResponse = (SelectAllSubCollectionResponse)objBaseSubCollectionDAL.API_SelectALL(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSubCollectionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public DeleteSubCollectionResponse DeleteSubCollection(DeleteSubCollectionRequest objRequest)
        {
            DeleteSubCollectionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
                objResponse = (DeleteSubCollectionResponse)objBaseSubCollectionDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SUBCOLLECTIONMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SubCollectionBLL", "DeleteSubCollection");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteSubCollectionResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Sub Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

       public SelectAllSubCollectionResponse SelectAllSubCollectionRecords(SelectAllSubCollectionRequest objRequest)
        {
            SelectAllSubCollectionResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
                objResponse = (SelectAllSubCollectionResponse)objBaseSubCollectionDAL.SelectAll(objRequest);

            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSubCollectionResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Collection Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
       public UpdateSubCollectionResponse DeleteSubBrand(UpdateSubCollectionRequest objRequest)
       {
           UpdateSubCollectionResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
               objResponse = (UpdateSubCollectionResponse)objBaseSubCollectionDAL.UpdateRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new UpdateSubCollectionResponse();
               objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Sub Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectByIDSubCollectionResponse SelectAllSubBrandRecords(SelectByIDSubCollectionRequest objRequest)
       {
           SelectByIDSubCollectionResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {

               var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
               
               objResponse = (SelectByIDSubCollectionResponse)objBaseSubCollectionDAL.SelectRecord(objRequest);

           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDSubCollectionResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
               
           }

           return objResponse;
       }
       public SelectSubCollectionLookUpResponse SelectAllLookUp(SelectSubCollectionLookUpRequest objRequest)
       {
           SelectSubCollectionLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
               objResponse = (SelectSubCollectionLookUpResponse)objBaseSubCollectionDAL.SelectSubCollectionLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectSubCollectionLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
             
           }

           return objResponse;
       }
       public SelectSubCollectionListForCollectionResponse SelectSubCollectionByCollection(SelectSubCollectionListForCollectionRequest objRequest)
       {
           SelectSubCollectionListForCollectionResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {

               var objBaseSubCollectionDAL = objFactory.GetDALRepository().GetSubCollectionDAL();
               if(objRequest.CollectionID == 0)
               { 
                    if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        int Doc_Id;
                        int.TryParse(objRequest.DocumentIDs, out Doc_Id);
                        objRequest.CollectionID = Doc_Id;
                        objRequest.ShowInActiveRecords = true;
                    }
               }
               objResponse = (SelectSubCollectionListForCollectionResponse)objBaseSubCollectionDAL.SelectSubCollectionByCollection(objRequest);

           }
           catch (Exception ex)
           {
               objResponse = new SelectSubCollectionListForCollectionResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;

           }

           return objResponse;
       }
    }
}
