using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizFactory;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Pricing
{
    public class PricePointBLL
    {
        public SelectAllPricePointResponse GetPricePointList(SelectAllPricePointRequest objRequest)
        {
            SelectAllPricePointResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePricePointDAL = objFactory.GetDALRepository().GetPricePointDAL();
                objResponse = (SelectAllPricePointResponse)objBasePricePointDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPricePointResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Point List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPricePointResponse API_GetPricePointList(SelectAllPricePointRequest objRequest)
        {
            SelectAllPricePointResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePricePointDAL = objFactory.GetDALRepository().GetPricePointDAL();
                objResponse = (SelectAllPricePointResponse)objBasePricePointDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPricePointResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Point List");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectPricePointByIDResponse GetPricePointRecord(SelectPricePointByIDRequest objRequest)
        {
            SelectPricePointByIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePricePointDAL = objFactory.GetDALRepository().GetPricePointDAL();
				// Changed by Senthamil @ 11.09.2018
				if (string.IsNullOrEmpty(objRequest.PricePointCode))
				{
					if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
					{
						objRequest.PricePointCode = objRequest.DocumentIDs;
					}
				}
                objResponse = (SelectPricePointByIDResponse)objBasePricePointDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectPricePointByIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Point Record");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SavePricePointResponse SavePricePointList(SavePricePointRequest objRequest)
        {
            SavePricePointResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePricePointDAL = objFactory.GetDALRepository().GetPricePointDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var PricePointList = new List<PricePoint>();
                    PricePointList = (List<PricePoint>)objRequest.RequestDynamicData;
                    objRequest.PricePointList = PricePointList;
					objRequest.PricePointCode = PricePointList.FirstOrDefault().PricePointCode;
					objRequest.PricePointName = PricePointList.FirstOrDefault().PricePointName;
                }
                objResponse = (SavePricePointResponse)objBasePricePointDAL.InsertRecord(objRequest);
                /*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PRICEPOINT;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Pricing.PricePointBLL", "SavePricePointList");
                }*/
            }
            catch (Exception ex)
            {
                objResponse = new SavePricePointResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Price Point");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeletePricePointResponse DeletePricePoint(DeletePricePointRequest objRequest)
        {
            DeletePricePointResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePricePointDAL = objFactory.GetDALRepository().GetPricePointDAL();
                objResponse = (DeletePricePointResponse)objBasePricePointDAL.DeleteRecord(objRequest);
                /*if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PRICEPOINT;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Pricing.PricePointBLL", "DeletePricePoint");
                }*/
            }
            catch (Exception ex)
            {
                objResponse = new DeletePricePointResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "PricePoint");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
