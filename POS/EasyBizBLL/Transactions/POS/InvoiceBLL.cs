using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.Invoice;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class InvoiceBLL
    {
        public SelectAllInvoiceResponse SelectAllInvoice(SelectAllInvoiceRequest objRequest)
        {
            SelectAllInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectAllInvoiceResponse)BaseInvoice.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        #region SelectBillCompletedSalesInvoice

        public SelectAllInvoiceResponse SelectBillCompletedSalesInvoice(SelectAllInvoiceRequest objRequest)
        {
            SelectAllInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectAllInvoiceResponse)BaseInvoice.SelectBillCompletedSalesInvoice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        #endregion 


        public SelectAllInvoiceResponse SelectHoldSalesInvoice(SelectAllInvoiceRequest objRequest)
        {
            SelectAllInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectAllInvoiceResponse)BaseInvoice.SelectHoldSalesInvoice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllInvoiceResponse SelectedPOSSearchInvoice(SelectAllInvoiceRequest objRequest)
        {
            SelectAllInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectAllInvoiceResponse)BaseInvoice.SelectedPOSSearchInvoice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectInvoiceDetailsListResponse GetSelectedRecallInvoice(SelectInvoiceDetailsListRequest objRequest)
        {
            SelectInvoiceDetailsListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectInvoiceDetailsListResponse)BaseInvoice.GetSelectedRecallInvoice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectInvoiceDetailsListResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllInvoiceResponse SelectPOSSearchAllInvoice(SelectAllInvoiceRequest objRequest)
        {
            SelectAllInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectAllInvoiceResponse)BaseInvoice.SelectPOSSearchAllInvoice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveInvoiceResponse SaveInvoice(SaveInvoiceRequest objRequest)
        {
            SaveInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInvoiceDAL = objFactory.GetDALRepository().GetInvoiceDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var _InvoiceHeader = new InvoiceHeader();
                    _InvoiceHeader = (InvoiceHeader)objRequest.RequestDynamicData;
                    objRequest.InvoiceHeaderData = _InvoiceHeader;
                    objRequest.InvoiceDetailList = _InvoiceHeader.InvoiceDetailList;
                    objRequest.PaymentList = _InvoiceHeader.PaymentList;
                    objRequest.TransactionLogList = _InvoiceHeader.TransactionLogList;
                }
                 objResponse = (SaveInvoiceResponse)objBaseInvoiceDAL.InsertRecord(objRequest);                

                if (objRequest.InvoiceHeaderData.SalesStatus == "Completed")
                {
                    if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false && objRequest.RequestFrom != Enums.RequestFrom.SyncService)
                    {
                        objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                        objRequest.DocumentIDs = objResponse.IDs;
                        objRequest.DocumentNos = objRequest.InvoiceHeaderData.InvoiceNo;
                        objRequest.DocumentType = Enums.DocumentType.SALES;
                        objRequest.ProcessMode = Enums.ProcessMode.New;

                        //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceBLL", "SaveInvoice");
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
                throw ex;
            }
            return objResponse;
        }

        #region SaveSalesInvoice

        public SaveInvoiceResponse SaveSalesInvoice(SaveInvoiceRequest objRequest)
        {
            SaveInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseInvoiceDAL = objFactory.GetDALRepository().GetInvoiceDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var _InvoiceHeader = new InvoiceHeader();
                    _InvoiceHeader = (InvoiceHeader)objRequest.RequestDynamicData;
                    objRequest.InvoiceHeaderData = _InvoiceHeader;
                    objRequest.InvoiceDetailList = _InvoiceHeader.InvoiceDetailList;
                    objRequest.PaymentList = _InvoiceHeader.PaymentList;
                    objRequest.TransactionLogList = _InvoiceHeader.TransactionLogList;
                }
                objResponse = (SaveInvoiceResponse)objBaseInvoiceDAL.SaveSalesRecord(objRequest);

                if (objRequest.InvoiceHeaderData.SalesStatus == "Completed")
                {
                    if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false && objRequest.RequestFrom != Enums.RequestFrom.SyncService)
                    {
                        objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                        objRequest.DocumentIDs = objResponse.IDs;
                        objRequest.DocumentNos = objRequest.InvoiceHeaderData.InvoiceNo;
                        objRequest.DocumentType = Enums.DocumentType.SALES;
                        objRequest.ProcessMode = Enums.ProcessMode.New;

                        //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceBLL", "SaveInvoice");
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SaveInvoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
                throw ex;
            }
            return objResponse;
        }

        #endregion

        #region SavePaymentcardcashInvoiceDtls

        public SaveInvoiceResponse SavePaymentcardcashInvoiceDtls(SaveInvoiceRequest objRequest)
        {
            SaveInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SaveInvoiceResponse)BaseInvoice.SaveCashandCardRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SaveInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        #endregion 





        public GetSearchInvoiceHeaderDetailsResponse GetSearchInvoiceHeaderDetails(SelectInvoiceDetailsListRequest objRequest)
        {
            GetSearchInvoiceHeaderDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (GetSearchInvoiceHeaderDetailsResponse)BaseInvoice.GetSearchInvoiceHeaderDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetSearchInvoiceHeaderDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetSearchInvoiceHeaderDetailsResponse GetExchangeItemDetails(SelectInvoiceDetailsListRequest objRequest)
        {
            GetSearchInvoiceHeaderDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (GetSearchInvoiceHeaderDetailsResponse)BaseInvoice.GetExchangeItemDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetSearchInvoiceHeaderDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveInvoiceResponse SavePaymentProcessor(SaveInvoiceRequest objRequest)
        {
            SaveInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.PaymentProcessorList = (List<PaymentProcessor>)objRequest.RequestDynamicData;
                }
                var objPaymentProcessorDAL = objFactory.GetDALRepository().GetInvoiceDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objPaymentProcessor = new List<PaymentProcessor>();
                    objPaymentProcessor = (List<PaymentProcessor>)objRequest.RequestDynamicData;
                    objRequest.PaymentProcessorList = objPaymentProcessor;

                }
                objResponse = (SaveInvoiceResponse)objPaymentProcessorDAL.SavePaymentProcesor(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = Enums.RequestFrom.StoreServer;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceBLL", "SavePaymentProcessor");
                }
            }
            catch (Exception ex)
            {
                //objResponse = new SaveInvoiceResponse();
                ////objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Processor");
                //objResponse.ExceptionMessage = ex.Message;
                //objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        //public SelectInvoiceLookupResponse SelectInvoiceHeaderRecord(SelectInvoiceLookupRequest objRequest)
        //{
        //    SelectInvoiceLookupResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {
        //        var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
        //        objResponse = (SelectInvoiceLookupResponse)BaseInvoice.SelectInvoiceLookupListID(objRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SelectInvoiceLookupResponse();
        //        objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }
        //    return objResponse;
        //}

        public SelectInvoiceReceiptByInvoiceNumResponse GetInvoiceReceipt(SelectInvoiceReceiptByInvoiceNumRequest objRequest)
        {
            SelectInvoiceReceiptByInvoiceNumResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectInvoiceReceiptByInvoiceNumResponse)BaseInvoice.GetInvoiceReceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectInvoiceReceiptByInvoiceNumResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectInvoiceReceiptByInvoiceNumResponse GetInvoiceReceipt1(SelectInvoiceReceiptByInvoiceNumRequest objRequest)
        {
            SelectInvoiceReceiptByInvoiceNumResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectInvoiceReceiptByInvoiceNumResponse)BaseInvoice.GetInvoiceReceipt1(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectInvoiceReceiptByInvoiceNumResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectInvoiceReceiptApprovalNumResponse GetInvoiceReceipt2(SelectInvoiceApprovalNumRequest objRequest)
        {
            SelectInvoiceReceiptApprovalNumResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectInvoiceReceiptApprovalNumResponse)BaseInvoice.GetInvoiceReceipt2(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectInvoiceReceiptApprovalNumResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectHoldReceiptByInvoiceNumResponse GetHoldReceipt(SelectHoldReceiptByInvoiceNumRequest objRequest)
        {
            SelectHoldReceiptByInvoiceNumResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectHoldReceiptByInvoiceNumResponse)BaseInvoice.GetHoldReceipt(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectHoldReceiptByInvoiceNumResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SelectInvoiceDetailsListResponse SelectInvoiceDetailsList(SelectInvoiceDetailsListRequest objRequest)
        {
            SelectInvoiceDetailsListResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectInvoiceDetailsListResponse)BaseInvoice.SelectInvoiceDetailsList(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectInvoiceDetailsListResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDInvoiceResponse SelectRecord(SelectByIDInvoiceRequest objRequest)
        {
            SelectByIDInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SelectByIDInvoiceResponse)BaseInvoice.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        //public SelectInvoiceDetailsByIDResponse SelectInvoiceDetailsByID(SelectInvoiceDetailsByIDRequest ReqObj)
        //{
        //    SelectInvoiceDetailsByIDResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {
        //        var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
        //        objResponse = (SelectInvoiceDetailsByIDResponse)BaseInvoice.SelectInvoiceDetailsByID(ReqObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SelectInvoiceDetailsByIDResponse();
        //        objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }
        //    return objResponse;
        //}
        public UpdateInvoiceStatusResponse UpdateInvoiceStatus(UpdateInvoiceStatusRequest objRequest)
        {
            UpdateInvoiceStatusResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.RequestDynamicData != null)
                {
                    int Status = (int)Enums.InvoiceStatus.Resale;
                    string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                    var _InvoiceHeader = new InvoiceHeader();
                    _InvoiceHeader = (InvoiceHeader)objRequest.RequestDynamicData;
                    objRequest.InvoiceID = _InvoiceHeader.ID;
                    objRequest.Status = TypeName;
                    objRequest.StoreID = _InvoiceHeader.StoreID;
                }
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (UpdateInvoiceStatusResponse)BaseInvoice.UpdateInvoiceStatus(objRequest);

                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false && objRequest.RequestFrom != Enums.RequestFrom.SyncService)
                //{
                //    objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.InvoiceID);
                //    objRequest.DocumentNos = objRequest.DocumentNos;
                //    objRequest.DocumentType = Enums.DocumentType.SALES;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceBLL", "UpdateInvoiceStatus");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new UpdateInvoiceStatusResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Hold Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteHoldSaleRecordsResponse DeleteHoldSaleRecords(DeleteHoldSaleRecordsRequest objRequest)
        {
            DeleteHoldSaleRecordsResponse objResponse = null;

            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;

                if (objRequest.RequestDynamicData != null)
                {
                    int Status = (int)Enums.InvoiceStatus.Resale;
                    string TypeName = Enum.GetName(typeof(Enums.InvoiceStatus), Status);
                    var _InvoiceHeader = new InvoiceHeader();
                    _InvoiceHeader = (InvoiceHeader)objRequest.RequestDynamicData;
                    objRequest.BusinessDate = _InvoiceHeader.BusinessDate;
                    objRequest.Status = TypeName;
                    objRequest.StoreID = _InvoiceHeader.StoreID;
                }

                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (DeleteHoldSaleRecordsResponse)BaseInvoice.DeleteHoldSaleRecords(objRequest);

                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false && objRequest.RequestFrom != Enums.RequestFrom.SyncService)
                //{
                //    objRequest.RequestFrom = Enums.RequestFrom.StoreServer;
                //    objRequest.DocumentIDs = string.Empty;
                //    objRequest.DocumentNos = string.Empty;
                //    objRequest.DocumentType = Enums.DocumentType.SALES;
                //    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.InvoiceBLL", "DeleteHoldSaleRecords");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new DeleteHoldSaleRecordsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Hold Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public SearchCommonInvoiceResponse GetPOSSearchInvoice(SearchCommonInvoiceRequest objRequest)
        {
            SearchCommonInvoiceResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var BaseInvoice = objFactory.GetDALRepository().GetInvoiceDAL();
                objResponse = (SearchCommonInvoiceResponse)BaseInvoice.GetSearchInvoice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SearchCommonInvoiceResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

    }
}
