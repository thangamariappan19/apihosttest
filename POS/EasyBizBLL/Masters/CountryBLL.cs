using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizResponse.Masters.CountryResponse;
using ResourceStrings;
using System;

namespace EasyBizBLL.Masters
{
    public class CountryBLL
    {
        public SaveCountryResponse SaveCountryMaster(SaveCountryRequest objRequest)
        {
            SaveCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCountryMaster = new CountryMaster();
                    objCountryMaster = (CountryMaster)objRequest.RequestDynamicData;
                    objRequest.CountryMasterData = objCountryMaster;
                }
                objResponse = (SaveCountryResponse)objBaseCountryDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.CountryMasterData.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUNTRY;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CountryBLL", "SaveCountryMaster");
                }

            }
            catch (Exception ex)
            {
                objResponse = new SaveCountryResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateCountryResponse UpdateCountryMaster(UpdateCountryRequest objRequest)
        {
            UpdateCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objCountryMaster = new CountryMaster();
                    objCountryMaster = (CountryMaster)objRequest.RequestDynamicData;
                    objRequest.CountryMasterData = objCountryMaster;
                }
                objResponse = (UpdateCountryResponse)objBaseCountryDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.CountryMasterData.ID);
                    objRequest.DocumentType = Enums.DocumentType.COUNTRY;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CountryBLL", "UpdateCountryMaster");
                }

            }
            catch (Exception ex)
            {
                objResponse = new UpdateCountryResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteCountryResponse DeleteCountryMaster(DeleteCountryRequest objRequest)
        {
            DeleteCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (DeleteCountryResponse)objBaseCountryDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COUNTRY;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.CountryBLL", "DeleteCountryMaster");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteCountryResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllCountryResponse SelectAllCountryMaster(SelectAllCountryRequest objRequest)
        {
            SelectAllCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            var _ShiftBLL = new ShiftBLL();
            try
            {
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (SelectAllCountryResponse)objBaseCountryDAL.SelectAll(objRequest);
                /*if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var SubBrandList = new List<SubBrandMaster>();
                    foreach (CountryMaster objBrand in objResponse.CountryMasterList)
                    {
                        var objSelectShiftListForCategoryRequest = new SelectShiftListForCategoryRequest();
                        objSelectShiftListForCategoryRequest.CountryID = objBrand.ID;
                        objSelectShiftListForCategoryRequest.ShowInActiveRecords = true;
                        var objSelectShiftListForCategoryResponse = new SelectShiftListForCategoryResponse();
                        objSelectShiftListForCategoryResponse = _ShiftBLL.ShiftByCountry(objSelectShiftListForCategoryRequest);
                        if (objSelectShiftListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objBrand.Shiftlist = objSelectShiftListForCategoryResponse.ShiftList;
                        }
                        //else
                        //{
                        //    objBrand.Shiftlist = new List<ShiftMaster>();
                        //}                      
                    }
                }*/
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllCountryResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllCountryResponse API_SelectAllCountryMaster(SelectAllCountryRequest objRequest)
        {
            SelectAllCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            var _ShiftBLL = new ShiftBLL();
            try
            {
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (SelectAllCountryResponse)objBaseCountryDAL.API_SelectAll(objRequest);
                //if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                // {                   
                //     var SubBrandList = new List<SubBrandMaster>();
                //     foreach (CountryMaster objBrand in objResponse.CountryMasterList)
                //     {
                //         var objSelectShiftListForCategoryRequest = new SelectShiftListForCategoryRequest();
                //         objSelectShiftListForCategoryRequest.CountryID = objBrand.ID;
                //         objSelectShiftListForCategoryRequest.ShowInActiveRecords = true;
                //         var objSelectShiftListForCategoryResponse = new SelectShiftListForCategoryResponse();
                //         objSelectShiftListForCategoryResponse = _ShiftBLL.ShiftByCountry(objSelectShiftListForCategoryRequest);
                //         if (objSelectShiftListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                //         {
                //             objBrand.Shiftlist = objSelectShiftListForCategoryResponse.ShiftList;
                //         }
                //         //else
                //         //{
                //         //    objBrand.Shiftlist = new List<ShiftMaster>();
                //         //}                      
                //     }
                // }
            }
            catch (Exception ex)
            {

                objResponse = new SelectAllCountryResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByIDCountryResponse SelectCountryMaster(SelectByIDCountryRequest objRequest)
        {
            SelectByIDCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (SelectByIDCountryResponse)objBaseCountryDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectByIDCountryResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectCountryLookUpResponse SelectCountryLookUp(SelectCountryLookUpRequest objRequest)
        {
            SelectCountryLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (SelectCountryLookUpResponse)objBaseCountryDAL.SelectCountryLookUp(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new SelectCountryLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetCurrencyCodeForCountryResponse GetCurrencyCodeForCountry(GetCurrencyCodeForCountryRequest objRequest)
        {
            GetCurrencyCodeForCountryResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (GetCurrencyCodeForCountryResponse)objBaseCountryDAL.GetCurrencyCodeForCountry(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new GetCurrencyCodeForCountryResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetCurrencyByStoreResponse GetCurencyByStore(GetCurrencyByStoreRequest objRequest)
        {
            GetCurrencyByStoreResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                BaseCountryDAL objBaseCountryDAL = objFactory.GetDALRepository().GetCountryDAL();
                objResponse = (GetCurrencyByStoreResponse)objBaseCountryDAL.GetCurencyByStore(objRequest);
            }
            catch (Exception ex)
            {

                objResponse = new GetCurrencyByStoreResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Country Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
