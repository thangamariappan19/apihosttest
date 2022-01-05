using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizResponse.Transactions.PriceChange;
using EasyBizDBTypes.Transactions.PriceChange;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;

namespace EasyBizBLL.Transactions.PriceChange
{
	public class PriceChangeBLL
	{
		public ValidatePriceChangeResponse ValidatePriceChangeDetails(ValidatePriceChangeRequest ObjRequest)
		{
			ValidatePriceChangeResponse ObjResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				ObjResponse = (ValidatePriceChangeResponse)objPriceChangeDAL.ValidatePriceChangeDetails(ObjRequest);
			}
			catch (Exception ex)
			{
				ObjResponse = new ValidatePriceChangeResponse();
				ObjResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				ObjResponse.ExceptionMessage = ex.Message;
				ObjResponse.StackTrace = ex.StackTrace;
			}
			return ObjResponse;
		}

		public SelectAllPriceChangeResponse API_SelectALL(SelectAllPriceChangeRequest requestData)
		{
			SelectAllPriceChangeResponse ObjResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				ObjResponse = (SelectAllPriceChangeResponse)objPriceChangeDAL.API_SelectALL(requestData);
			}
			catch (Exception ex)
			{
				ObjResponse = new SelectAllPriceChangeResponse();
				ObjResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				ObjResponse.ExceptionMessage = ex.Message;
				ObjResponse.StackTrace = ex.StackTrace;
			}
			return ObjResponse;
		}
		public SelectAllPriceChangeResponse GetAllPriceChange(SelectAllPriceChangeRequest ObjRequest)
		{
			SelectAllPriceChangeResponse ObjResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				ObjResponse = (SelectAllPriceChangeResponse)objPriceChangeDAL.SelectAll(ObjRequest);
			}
			catch (Exception ex)
			{
				ObjResponse = new SelectAllPriceChangeResponse();
				ObjResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				ObjResponse.ExceptionMessage = ex.Message;
				ObjResponse.StackTrace = ex.StackTrace;
			}
			return ObjResponse;
		}
		public SavePriceChangeResponse SavePriceChange(SavePriceChangeRequest objRequest)
		{
			SavePriceChangeResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
				var ObjBasePriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				if (objRequest.RequestDynamicData != null)
				{
					objRequest.PriceChangeRecord = (EasyBizDBTypes.Transactions.PriceChange.PriceChange)objRequest.RequestDynamicData;
					//objRequest.PriceChangeRecord = objRequest.RequestDynamicData.PriceChange;
					objRequest.PriceChangeDetailsList = objRequest.RequestDynamicData.PriceChangeDetailsList;
					objRequest.PriceChangeCountriesList = objRequest.RequestDynamicData.PriceChangeCountriesList;
				}
				objResponse = (SavePriceChangeResponse)ObjBasePriceChangeDAL.InsertRecord(objRequest);
				/*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
				{
					objRequest.RequestFrom = objRequest.RequestFrom;
					objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
					objRequest.DocumentType = Enums.DocumentType.PRICECHANGE;
					objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

					BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.PriceChange.PriceChangeBLL", "SavePriceChange");
				}*/
			}
			catch (Exception ex)
			{
				objResponse = new SavePriceChangeResponse();
				objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Change");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public SelectByIDPriceChangeResponse GetPriceChangeRecord(SelectByIDPriceChangeRequest objRequest)
		{
			SelectByIDPriceChangeResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				objResponse = (SelectByIDPriceChangeResponse)objPriceChangeDAL.SelectByIDs(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectByIDPriceChangeResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public SelectPriceChangeDetailsResponse GetPriceChangeDetails(SelectPriceChangeDetailsRequest objRequest)
		{
			SelectPriceChangeDetailsResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				objResponse = (SelectPriceChangeDetailsResponse)objPriceChangeDAL.GetPriceChangeDetails(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectPriceChangeDetailsResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}
		public SelectPriceChangeCountriesResponse GetPriceChangeCountries(SelectPriceChangeCountriesRequest objRequest)
		{
			SelectPriceChangeCountriesResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				objResponse = (SelectPriceChangeCountriesResponse)objPriceChangeDAL.GetPriceChangeCountries(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectPriceChangeCountriesResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}

		public SelectPriceChangeRecordResponse SelectPriceChangeRecord(SelectPriceChangeRecordRequest objRequest)
		{
			SelectPriceChangeRecordResponse objResponse = null;
			var objFactory = new DALFactory();
			try
			{
				var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
				if(objRequest.ID == 0)
				{
					if(!string.IsNullOrWhiteSpace(objRequest.DocumentIDs))
					{
						int doc_id;
						int.TryParse(objRequest.DocumentIDs, out doc_id);
						objRequest.ID = doc_id;
					}
				}
				objResponse = (SelectPriceChangeRecordResponse)objPriceChangeDAL.SelectRecord(objRequest);
			}
			catch (Exception ex)
			{
				objResponse = new SelectPriceChangeRecordResponse();
				objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
				objResponse.ExceptionMessage = ex.Message;
				objResponse.StackTrace = ex.StackTrace;
			}
			return objResponse;
		}

        public SelectPriceChangeStatusResponse SelectPriceChangeStatus(SelectPriceChangeStatusRequest objRequest)
        {
            SelectPriceChangeStatusResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
                objResponse = (SelectPriceChangeStatusResponse)objPriceChangeDAL.SelectPriceChangeStatus(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPriceChangeStatusResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public PriceUpdateResponse UpdateStylePrice(PriceUpdateRequest objRequest)
        {
            PriceUpdateResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
                objResponse = (PriceUpdateResponse)objPriceChangeDAL.UpdateStylePrice(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new PriceUpdateResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectPriceChangeLogResponse SelectPriceChangeLog(SelectPriceChangeLogRequest ObjRequest)
        {
            SelectPriceChangeLogResponse ObjResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objPriceChangeDAL = objFactory.GetDALRepository().GetPriceChangeDAL();
                ObjResponse = (SelectPriceChangeLogResponse)objPriceChangeDAL.SelectPriceChangeLog(ObjRequest);
            }
            catch (Exception ex)
            {
                ObjResponse = new SelectPriceChangeLogResponse();
                ObjResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Change Log Report");
                ObjResponse.ExceptionMessage = ex.Message;
                ObjResponse.StackTrace = ex.StackTrace;
            }
            return ObjResponse;
        }

	}
}
