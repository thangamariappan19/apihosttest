using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.RetailSettingsResponse;
using EasyBizResponse.Masters.TaxMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class TaxBLL
    {
        public SaveTaxResponse SaveTax(SaveTaxRequest objRequest)
        {
            SaveTaxResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Cha
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    //var objTax = new TaxMaster();
                    //objTax = (TaxMaster)objRequest.RequestDynamicData;
                    //objRequest.Taxlist = objTax.Taxlist;

                    var objTax = new List<TaxMaster>();
                    objTax.AddRange(objRequest.RequestDynamicData);
                    objRequest.Taxlist = objTax;
                }
                objResponse = (SaveTaxResponse)objBaseTaxDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.TAXMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TaxBLL", "SaveTax");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveTaxResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectAllTaxResponse API_SelectALL(SelectAllTaxRequest requestData)

        {
            SelectAllTaxResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (SelectAllTaxResponse)objBaseTaxDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllTaxResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public DeleteTaxResponse DeleteTax(DeleteTaxRequest objRequest)
        {
            DeleteTaxResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (DeleteTaxResponse)objBaseTaxDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objRequest.TaxRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.TAXMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TaxBLL", "DeleteTax");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteTaxResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllTaxResponse SelectAllTaxRecords(SelectAllTaxRequest objRequest)
        {
            SelectAllTaxResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (SelectAllTaxResponse)objBaseTaxDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllTaxResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateTaxResponse UpdateTax(UpdateTaxRequest objRequest)
        {
            UpdateTaxResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objTax = new TaxMaster();
                    objTax = (TaxMaster)objRequest.RequestDynamicData;
                    objRequest.TaxRecord = objTax;
                }
                objResponse = (UpdateTaxResponse)objBaseTaxDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.TaxRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.TAXMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TaxBLL", "UpdateTax");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateTaxResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByTaxIDResponse SelectTaxRecord(SelectByTaxIDRequest objRequest)
        {
            SelectByTaxIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                if (objRequest.ID == 0)
                {
                    if (!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        int Doc_Id;
                        int.TryParse(objRequest.DocumentIDs, out Doc_Id);
                        objRequest.ID = Doc_Id;
                    }
                }
                objResponse = (SelectByTaxIDResponse)objBaseTaxDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByTaxIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectTaxLookUpResponse SubSeasomLookUp(SelectTaxLookUpRequest objRequest)
        {
            SelectTaxLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (SelectTaxLookUpResponse)objBaseTaxDAL.SelectTaxLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectTaxLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectTaxLookUpResponse SelectTaxLookUp(SelectTaxLookUpRequest objRequest)
        {
            SelectTaxLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxMasterDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (SelectTaxLookUpResponse)objBaseTaxMasterDAL.SelectTaxLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectTaxLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectTaxDetailsByCountryIDResponse SelectTaxDetailsByCountryID(SelectTaxDetailsByCountryIDRequest objRequest)
        {
            SelectTaxDetailsByCountryIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxMasterDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (SelectTaxDetailsByCountryIDResponse)objBaseTaxMasterDAL.SelectTaxDetailsByCountryID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectTaxDetailsByCountryIDResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllTaxResponse API_SelectTaxLookUp(SelectAllTaxRequest objRequest)
        {
            SelectAllTaxResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTaxDAL = objFactory.GetDALRepository().GetTaxMasterDAL();
                objResponse = (SelectAllTaxResponse)objBaseTaxDAL.API_SelectTaxLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllTaxResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tax Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
