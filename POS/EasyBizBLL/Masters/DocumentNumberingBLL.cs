using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.DocumentTypeResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class DocumentNumberingBLL
    {
        public SaveDocumentNumberingMasterResponse SaveDocumentNumberingMaster(SaveDocumentNumberingMasterRequest objRequest)
        {
            SaveDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;

                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjDocumentNumberingMaster = new DocumentNumberingMaster();
                    ObjDocumentNumberingMaster = (DocumentNumberingMaster)objRequest.RequestDynamicData;
                    objRequest.CountryID = ObjDocumentNumberingMaster.CountryID;
                    objRequest.StateID = ObjDocumentNumberingMaster.StateID;
                    objRequest.StoreID = ObjDocumentNumberingMaster.StoreID;
                    objRequest.PosID = ObjDocumentNumberingMaster.PosID;
                    objRequest.DocumentTypeID = ObjDocumentNumberingMaster.DocumentTypeID;

                    ObjDocumentNumberingMaster.ID = 0;
                    objRequest.DocumentNumberingMasterRecord = new DocumentNumberingMaster();
                    objRequest.DocumentNumberingMasterRecord = ObjDocumentNumberingMaster;                    
                    objRequest.DocumentNumberingDetailsList = ObjDocumentNumberingMaster.DocumentNumberingDetails;                    
                }
                objResponse = (SaveDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.DOCUMENTNUMBERING;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                    objRequest.BaseIntegrateStoreID = objRequest.DocumentNumberingMasterRecord.StoreID;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentNumberingBLL", "SaveDocumentNumberingMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateDocumentNumberingMasterResponse UpdateDocumentNumberingMaster(UpdateDocumentNumberingMasterRequest objRequest)
        {
            UpdateDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;

                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjDocumentNumberingMaster = new DocumentNumberingMaster();
                    ObjDocumentNumberingMaster = (DocumentNumberingMaster)objRequest.RequestDynamicData;                   
                    objRequest.DocumentNumberingMasterRecord = ObjDocumentNumberingMaster;
                    objRequest.ID = ObjDocumentNumberingMaster.ID;

                }
                objResponse = (UpdateDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;                   
                    objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentNumberingMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.DOCUMENTNUMBERING;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;
                    objRequest.BaseIntegrateStoreID = objRequest.DocumentNumberingMasterRecord.StoreID;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentNumberingBLL", "UpdateDocumentNumberingMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectDocumentNumberingBillNoDetailsResponse GetDocumentNo(SelectDocumentNumberingBillNoDetailsRequest RequestObj)
        {
            var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)RequestObj;
            SelectDocumentNumberingBillNoDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objDocumentNumberingDetailsDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectDocumentNumberingBillNoDetailsResponse)objDocumentNumberingDetailsDAL.SelectDocumentNumberingDetailsAPI(RequestObj);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDocumentNumberingBillNoDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Numbering Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateRunningNumResponse UpdateDocumentRunningNumber(UpdateRunningNumRequest objRequest)
        {
            UpdateRunningNumResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (UpdateRunningNumResponse)objBaseDocumentNumberingMasterDAL.UpdateRunningNum(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.RunningNumRecord.ID);
                    //objRequest.DocumentType = Enums.DocumentType.INVENTORYCOUNTING;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;
                    objRequest.BaseIntegrateStoreID = objRequest.StoreID;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentNumberingBLL", "UpdateDocumentNumberingMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateRunningNumResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllDocumentNumberingMasterResponse API_SelectALL(SelectAllDocumentNumberingMasterRequest requestData)
        {
            SelectAllDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectAllDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteDocumentNumberingMasterResponse DeleteDocumentNumberingMaster(DeleteDocumentNumberingMasterRequest objRequest)
        {
            DeleteDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (DeleteDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentNumberingMasterRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.DOCUMENTNUMBERING;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentNumberingBLL", "DeleteDocumentNumberingMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllDocumentNumberingMasterResponse SelectAllDocumentNumberingMaster(SelectAllDocumentNumberingMasterRequest objRequest)
        {
            SelectAllDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectAllDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDDocumentNumberingMasterResponse SelectDocumentNumberingMaster(SelectByIDDocumentNumberingMasterRequest objRequest)
        {
            SelectByIDDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectByIDDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectByIDDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDDocumentNumberingMasterResponse SelectHeaderID(SelectByIDDocumentNumberingMasterRequest objRequest)
        {
            SelectByIDDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectByIDDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.SelectHeaderID(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectByIDDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectDocumentNumberingMasterLookUpResponse SelectDocumentLookUp(SelectDocumentNumberingMasterLookUpRequest objRequest)
        {
            SelectDocumentNumberingMasterLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectDocumentNumberingMasterLookUpResponse)objBaseDocumentNumberingMasterDAL.InsertRecord(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectDocumentNumberingMasterLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectDocumentNumberingDetailsResponse SelectDocumentNumberingDetails(SelectDocumentNumberingDetailsRequest objRequest)
        {
            SelectDocumentNumberingDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objDocumentNumberingDetailsDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectDocumentNumberingDetailsResponse)objDocumentNumberingDetailsDAL.SelecDocumentNumberingDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDocumentNumberingDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Numbering Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectDocumentNumberingDetailsResponse SelectAutoIncrementIDDetails(SelectDocumentNumberingDetailsRequest objRequest)
        {
            SelectDocumentNumberingDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objDocumentNumberingDetailsDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectDocumentNumberingDetailsResponse)objDocumentNumberingDetailsDAL.SelectAutoIncrementID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectDocumentNumberingDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Numbering Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public string DocumentNumberingBillNoGenerate(SelectDocumentNumberingBillNoDetailsRequest objRequest)
        {
            string DocNum = string.Empty;
            var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)objRequest;
            SelectDocumentNumberingBillNoDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objDocumentNumberingDetailsDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectDocumentNumberingBillNoDetailsResponse)objDocumentNumberingDetailsDAL.SelectDocumentNumberingBillNoDetails(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success && RequestData.DocumentTypeID == (int)Enums.DocumentType.STYLEMASTER)
                {
                    DocNum = Convert.ToString(objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo);
                }
                else if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success && RequestData.DocumentTypeID != (int)Enums.DocumentType.STYLEMASTER)
                {                    //conta
                    //DocNum = objResponse.DocumentNumberingBillNoDetailsRecord.Prefix + objResponse.DocumentNumberingBillNoDetailsRecord.Suffix + objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo;     

                    DocNum = DocNum.ToDocumentNo(objResponse.DocumentNumberingBillNoDetailsRecord.Prefix, objResponse.DocumentNumberingBillNoDetailsRecord.Suffix, objResponse.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, objResponse.DocumentNumberingBillNoDetailsRecord.StartNumber, objResponse.DocumentNumberingBillNoDetailsRecord.EndNumber, objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo);
                }
                
             
            }
            catch (Exception ex)
            {
                objResponse = new SelectDocumentNumberingBillNoDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Numbering Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return DocNum;
        }
        public SelectDocumentNumberingBillNoDetailsResponse GetDocumentNoDetail(SelectDocumentNumberingBillNoDetailsRequest RequestObj)
        {            
            var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)RequestObj;
            SelectDocumentNumberingBillNoDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objDocumentNumberingDetailsDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectDocumentNumberingBillNoDetailsResponse)objDocumentNumberingDetailsDAL.SelectDocumentNumberingBillNoDetails(RequestObj);               
            }
            catch (Exception ex)
            {
                objResponse = new SelectDocumentNumberingBillNoDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Numbering Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        //public string DocumentNumberingCustomerCodeGenerate(SelectDocumentNumberingBillNoDetailsRequest objRequest)
        //{
        //    string DocNum = string.Empty;
        //    string Runningnum = string.Empty;
        //    var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)objRequest;
        //    SelectDocumentNumberingBillNoDetailsResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {
        //        var objDocumentNumberingDetailsDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
        //        objResponse = (SelectDocumentNumberingBillNoDetailsResponse)objDocumentNumberingDetailsDAL.SelectDocumentNumberingCustomerDetails(objRequest);
        //        if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success && RequestData.DocumentTypeID == (int)Enums.DocumentType.CUSTOMERMASTER)
        //        {
        //            Runningnum = Convert.ToString(objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo);
        //            DocNum = Convert.ToString(objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo);
        //            DocNum = DocNum.ToDocumentNo(objResponse.DocumentNumberingBillNoDetailsRecord.Prefix, objResponse.DocumentNumberingBillNoDetailsRecord.Suffix, objResponse.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, objResponse.DocumentNumberingBillNoDetailsRecord.StartNumber, objResponse.DocumentNumberingBillNoDetailsRecord.EndNumber, objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo);
        //        }
        //        else if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success && RequestData.DocumentTypeID != (int)Enums.DocumentType.CUSTOMERMASTER)
        //        {                    //conta
        //            //DocNum = objResponse.DocumentNumberingBillNoDetailsRecord.Prefix + objResponse.DocumentNumberingBillNoDetailsRecord.Suffix + objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo;     

        //            DocNum = DocNum.ToDocumentNo(objResponse.DocumentNumberingBillNoDetailsRecord.Prefix, objResponse.DocumentNumberingBillNoDetailsRecord.Suffix, objResponse.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, objResponse.DocumentNumberingBillNoDetailsRecord.StartNumber, objResponse.DocumentNumberingBillNoDetailsRecord.EndNumber, objResponse.DocumentNumberingBillNoDetailsRecord.RunningNo);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SelectDocumentNumberingBillNoDetailsResponse();
        //        objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Numbering Master");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }
          
        //    return DocNum;
           
         
        //}

        public SelectByIDDocumentNumberingMasterResponse DateValidation(SaveDocumentNumberingMasterRequest objRequest)
        {
            SelectByIDDocumentNumberingMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentNumberingMasterDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetDocumentNumberingMasterDAL();
                objResponse = (SelectByIDDocumentNumberingMasterResponse)objBaseDocumentNumberingMasterDAL.DateValidation(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectByIDDocumentNumberingMasterResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
