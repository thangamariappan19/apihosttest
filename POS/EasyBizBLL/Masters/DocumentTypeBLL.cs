using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizResponse.Masters.DocumentTypeResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class DocumentTypeBLL
    {
       public SaveDocumentTypeResponse SaveDocumentType(SaveDocumentTypeRequest objRequest)
        {
            SaveDocumentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //if (objRequest.RequestDynamicData != null)
                //{
                //    objRequest.DocumentTypeRecord = (DocumentTypes)objRequest.RequestDynamicData;
                //}
                BaseDocumentTypeDAL objBaseDocumentTypeDAL = objFactory.GetDALRepository().GetDocumentTypeDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjDocumentType = new DocumentTypes();
                    ObjDocumentType = (DocumentTypes)objRequest.RequestDynamicData;
                    objRequest.DocumentTypeRecord = ObjDocumentType;
                }
                objResponse = (SaveDocumentTypeResponse)objBaseDocumentTypeDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentTypeRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.DOCUMENTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentTypeBLL", "SaveDocumentType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveDocumentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateDocumentTypeResponse UpdateDocumentType(UpdateDocumentTypeRequest objRequest)
        {
            UpdateDocumentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentTypeDAL objBaseDocumentTypeDAL = objFactory.GetDALRepository().GetDocumentTypeDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var ObjDocumentType = new DocumentTypes();
                    ObjDocumentType = (DocumentTypes)objRequest.RequestDynamicData;
                    objRequest.DocumentTypeRecord = ObjDocumentType;
                }
                objResponse = (UpdateDocumentTypeResponse)objBaseDocumentTypeDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentTypeRecord.ID);
                    //objRequest.DocumentType = Enums.DocumentType.DOCUMENTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentTypeBLL", "UpdateDocumentType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateDocumentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteDocumentTypeResponse DeleteDocumentType(DeleteDocumentTypeRequest objRequest)
        {
            DeleteDocumentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentTypeDAL objBaseDocumentTypeDAL = objFactory.GetDALRepository().GetDocumentTypeDAL();
                objResponse = (DeleteDocumentTypeResponse)objBaseDocumentTypeDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objRequest.DocumentTypeRecord.ID);
                    //objRequest.DocumentType = Enums.DocumentType.DOCUMENTTYPE;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.DocumentTypeBLL", "DeleteDocumentType");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteDocumentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllDocumentTypeResponse SelectAllDocumentType(SelectAllDocumentTypeRequest objRequest)
        {
            SelectAllDocumentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentTypeDAL objBaseDocumentTypeDAL = objFactory.GetDALRepository().GetDocumentTypeDAL();
                objResponse = (SelectAllDocumentTypeResponse)objBaseDocumentTypeDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllDocumentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDDocumentTypeResponse SelectDocumentType(SelectByIDDocumentTypeRequest objRequest)
        {
            SelectByIDDocumentTypeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }    
                BaseDocumentTypeDAL objBaseDocumentTypeDAL = objFactory.GetDALRepository().GetDocumentTypeDAL();
                objResponse = (SelectByIDDocumentTypeResponse)objBaseDocumentTypeDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectByIDDocumentTypeResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectDocumentLookUpResponse SelectDocumentLookUp(SelectDocumentLookUpRequest objRequest)
        {
            SelectDocumentLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseDocumentTypeDAL objBaseDocumentTypeDAL = objFactory.GetDALRepository().GetDocumentTypeDAL();
                objResponse = (SelectDocumentLookUpResponse)objBaseDocumentTypeDAL.SelectDocumentLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectDocumentLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
