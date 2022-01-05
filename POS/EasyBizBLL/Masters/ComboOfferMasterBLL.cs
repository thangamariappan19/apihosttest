using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizFactory;
using EasyBizRequest.Masters.ComboOfferRequest;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Masters.ComboOfferResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ComboOfferMasterBLL
    {

        public SelectAllComboOfferResponse SelectAllComboOfferRecords(SelectAllComboOfferRequest objRequest)
        {
            //var _SubBrandBLL = new SubBrandBLL();
            SelectAllComboOfferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseColorDAL = objFactory.GetDALRepository().GetComboOfferMasterDAL();
                objResponse = (SelectAllComboOfferResponse)objBaseColorDAL.SelectAll(objRequest);
            }

            catch (Exception ex)
            {
                objResponse = new SelectAllComboOfferResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Combo Offer");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SaveComboOfferResponse SaveComboOffer(SaveComboOfferRequest objRequest)
        {
            SaveComboOfferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                //objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseComboOfferRequestDAL = objFactory.GetDALRepository().GetComboOfferMasterDAL();
                //if (objRequest.RequestDynamicData != null)
                //{
                //    var objComboOfferRequest = new ComboOfferMaster();
                //    objComboOfferRequest = (ComboOfferMaster)objRequest.RequestDynamicData;
                //    objRequest.ComboOfferHeaderRecord = objComboOfferRequest;
                //    objRequest.ComboOfferDetailsList = objComboOfferRequest.ComboOfferDetailsList;
                //}
                objResponse = (SaveComboOfferResponse)objBaseComboOfferRequestDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.COMBOOFFER;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.OpeningStockBLL", "SaveOpeningStock");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveComboOfferResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ComboOffer");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSKUMasterResponse SelectAllSKUMaster(SelectAllSKUMasterRequest objRequest)
        {

            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectAllComboOfferResponse SelectAllComboOffer(SelectAllComboOfferRequest objRequest)
        {
            SelectAllComboOfferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseComboOfferDAL = objFactory.GetDALRepository().GetComboOfferMasterDAL();
                objResponse = (SelectAllComboOfferResponse)objBaseComboOfferDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllComboOfferResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OpeningStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByComboOfferIDResponse SelectComboOfferRecord(SelectByComboOfferIDRequest objRequest)
        {
            SelectByComboOfferIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockRequestDAL = objFactory.GetDALRepository().GetComboOfferMasterDAL();
                objResponse = (SelectByComboOfferIDResponse)objBaseStockRequestDAL.SelectByIDs(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByComboOfferIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "OpeningStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


    }
}

