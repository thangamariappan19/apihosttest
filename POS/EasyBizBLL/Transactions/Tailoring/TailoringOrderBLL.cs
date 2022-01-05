using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Tailoring;
using EasyBizFactory;
using EasyBizRequest.Transactions.Tailoring;
using EasyBizResponse.Transactions.Tailoring;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Tailoring
{
	public class TailoringOrderBLL
    {
        #region "Tailoring Order"
        public SaveTailoringOrderResponse SaveTailoringOrder(SaveTailoringOrderRequest objRequest)
		{
			SaveTailoringOrderResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
				var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
				if (objRequest.RequestDynamicData != null)
				{
					var objTailoringOrder = new TailoringOrder();
					objTailoringOrder = (TailoringOrder)objRequest.RequestDynamicData;
					objRequest.TailoringOrderHeaderRecord = objTailoringOrder;
					objRequest.TailoringOrderDetailsList = objTailoringOrder.TailoringOrderDetailsList;
				}
				objResponse = (SaveTailoringOrderResponse)objBaseTailoringOrderDAL.InsertRecord(objRequest);
				if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
				{
					objRequest.RequestFrom = objRequest.RequestFrom;
					objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
					objRequest.DocumentType = Enums.DocumentType.TAILORINGORDER;
					objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

					BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Tailoring.TailoringOrderBLL", "SaveTailoringOrder");
				}
			}
			catch (Exception ex)
			{
				objResponse = new SaveTailoringOrderResponse();
				objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Order");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public UpdateTailoringOrderResponse UpdateTailoringOrder(UpdateTailoringOrderRequest objRequest)
		{
			UpdateTailoringOrderResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
				var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
				objResponse = (UpdateTailoringOrderResponse)objBaseTailoringOrderDAL.UpdateRecord(objRequest);
				if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
				{
					objRequest.RequestFrom = objRequest.RequestFrom;
					objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
					objRequest.DocumentType = Enums.DocumentType.TAILORINGORDER;
					objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

					BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Tailoring.TailoringOrderBLL", "UpdateTailoringOrder");
				}
			}
			catch (Exception ex)
			{
				objResponse = new UpdateTailoringOrderResponse();
				objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Tailoring Order");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public DeleteTailoringOrderResponse DeleteTailoringOrder(DeleteTailoringOrderRequest objRequest)
		{
			DeleteTailoringOrderResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
				var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
				objResponse = (DeleteTailoringOrderResponse)objBaseTailoringOrderDAL.DeleteRecord(objRequest);
				if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
				{
					objRequest.RequestFrom = objRequest.RequestFrom;
					objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
					objRequest.DocumentType = Enums.DocumentType.TAILORINGORDER;
					objRequest.ProcessMode = Enums.ProcessMode.Delete;

					BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Tailoring.TailoringOrderBLL", "DeleteTailoringOrder");
				}
			}
			catch (Exception ex)
			{
				objResponse = new DeleteTailoringOrderResponse();
				objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Tailoring Order");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public SelectAllTailoringOrderResponse SelectAllTailoringOrder(SelectAllTailoringOrderRequest objRequest)
		{
			SelectAllTailoringOrderResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
				objResponse = (SelectAllTailoringOrderResponse)objBaseTailoringOrderDAL.SelectAll(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectAllTailoringOrderResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Order");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}		
		public SelectByIDTailoringOrderResponse SelectTailoringOrder(SelectByIDTailoringOrderRequest objRequest)
		{
			SelectByIDTailoringOrderResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
				objResponse = (SelectByIDTailoringOrderResponse)objBaseTailoringOrderDAL.SelectRecord(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectByIDTailoringOrderResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Order");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public SelectTailoringOrderDetailsResponse SelectTailoringOrderDetails(SelectTailoringOrderDetailsRequest objRequest)
		{
			SelectTailoringOrderDetailsResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
				objResponse = (SelectTailoringOrderDetailsResponse)objBaseTailoringOrderDAL.SelectTailoringOrderDetails(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectTailoringOrderDetailsResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Order");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
        #endregion
        #region "Despatch To Tailoring Unit"
        public SelectAllOPENTailoringOrderResponse SelectAllOPENTailoringOrder(SelectAllOPENTailoringOrderRequest objRequest)
        {
            SelectAllOPENTailoringOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
                objResponse = (SelectAllOPENTailoringOrderResponse)objBaseTailoringOrderDAL.SelectAllOPENTailoringOrder(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllOPENTailoringOrderResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Order");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveDespatchToTailorResponse SaveDespatchToTailor(SaveDespatchToTailorRequest objRequest)
        {
            SaveDespatchToTailorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objTailoringOrderList = new List<TailoringOrder>();
                    objTailoringOrderList = (List<TailoringOrder>)objRequest.RequestDynamicData;
                    objRequest.TailoringOrderList = objTailoringOrderList;
                }
                objResponse = (SaveDespatchToTailorResponse)objBaseTailoringOrderDAL.SaveDespatchToTailor(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.TAILORINGORDER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Tailoring.TailoringOrderBLL", "SaveTailoringOrder");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveDespatchToTailorResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Despatch To Tailoring Unit");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        #endregion
        #region "Receive From Tailor"
        public SelectTailoringOrderForReceiveFromTailorResponse SelectTailoringOrderForReceiveFromTailor(SelectTailoringOrderForReceiveFromTailorRequest objRequest)
        {
            SelectTailoringOrderForReceiveFromTailorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
                objResponse = (SelectTailoringOrderForReceiveFromTailorResponse)objBaseTailoringOrderDAL.SelectTailoringOrderForReceiveFromTailor(objRequest);


                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubBrandList = new List<TailoringOrderDetails>();
                    foreach (TailoringOrder objBrand in objResponse.TailoringOrderList)
                    {
                        var objSelectSubBrandListForCategoryRequest = new SelectTailoringOrderDetailsRequest();
                        objSelectSubBrandListForCategoryRequest.ID = objBrand.ID;
                        objSelectSubBrandListForCategoryRequest.ShowInActiveRecords = true;
                        var objSelectSubBrandListForCategoryResponse = new SelectTailoringOrderDetailsResponse();
                        objSelectSubBrandListForCategoryResponse = objBaseTailoringOrderDAL.SelectTailoringOrderDetails(objSelectSubBrandListForCategoryRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objBrand.TailoringOrderDetailsList = objSelectSubBrandListForCategoryResponse.TailoringOrderDetailsList;
                        }
                        else
                        {
                            objBrand.TailoringOrderDetailsList = new List<TailoringOrderDetails>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse = new SelectTailoringOrderForReceiveFromTailorResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Receive From Tailor");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveReceiveFromTailoringOrderResponse SaveReceiveFromTailoring(SaveReceiveFromTailoringOrderRequest objRequest)
        {
            SaveReceiveFromTailoringOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objTailoringOrderList = new List<TailoringOrder>();
                    objTailoringOrderList = (List<TailoringOrder>)objRequest.RequestDynamicData;
                    objRequest.TailoringOrderList = objTailoringOrderList;
                }
                objResponse = (SaveReceiveFromTailoringOrderResponse)objBaseTailoringOrderDAL.SaveReceiveFromTailoring(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.TAILORINGORDER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Tailoring.TailoringOrderBLL", "SaveReceiveFromTailoring");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveReceiveFromTailoringOrderResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Receive From Tailor");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        #endregion
        #region "Delover To Customer"
        public SelectTailoringOrderDeliverToCustomerResponse SelectTailoringOrderForReceiveFromTailor(SelectTailoringOrderForDeliverToCustomerRequest objRequest)
        {
            SelectTailoringOrderDeliverToCustomerResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
                objResponse = (SelectTailoringOrderDeliverToCustomerResponse)objBaseTailoringOrderDAL.SelectTailoringOrderDeliverToCustomerDetails(objRequest);


                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubBrandList = new List<TailoringOrderDetails>();
                    foreach (TailoringOrder objBrand in objResponse.TailoringOrderList)
                    {
                        var objSelectSubBrandListForCategoryRequest = new SelectTailoringOrderDetailsRequest();
                        objSelectSubBrandListForCategoryRequest.ID = objBrand.ID;
                        objSelectSubBrandListForCategoryRequest.ShowInActiveRecords = true;
                        objSelectSubBrandListForCategoryRequest.CheckLoggedIn = "Deliver";
                        objSelectSubBrandListForCategoryRequest.FromDeliverCode = "DeliverToCustomer";
                        var objSelectSubBrandListForCategoryResponse = new SelectTailoringOrderDetailsResponse();
                        objSelectSubBrandListForCategoryResponse = objBaseTailoringOrderDAL.SelectTailoringOrderDetails(objSelectSubBrandListForCategoryRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objBrand.TailoringOrderDetailsList = objSelectSubBrandListForCategoryResponse.TailoringOrderDetailsList;
                        }
                        else
                        {
                            objBrand.TailoringOrderDetailsList = new List<TailoringOrderDetails>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse = new SelectTailoringOrderDeliverToCustomerResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Deliver TGo Customer");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SaveDeliverToCustomerResponse SaveDeliverToCustomer(SaveDeliverToCustomerRequest objRequest)
        {
            SaveDeliverToCustomerResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseTailoringOrderDAL = objFactory.GetDALRepository().GetTailoringOrderDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objTailoringOrderList = new List<TailoringOrder>();
                    objTailoringOrderList = (List<TailoringOrder>)objRequest.RequestDynamicData;
                    objRequest.TailoringOrderList = objTailoringOrderList;
                }
                objResponse = (SaveDeliverToCustomerResponse)objBaseTailoringOrderDAL.SaveDeliverToCustomer(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.TAILORINGORDER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Tailoring.TailoringOrderBLL", "SaveDeliverToCustomer");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveDeliverToCustomerResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Deliver TGo Customer");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        #endregion
    }
}
