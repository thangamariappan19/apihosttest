using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class BarcodeSettingsBLL
    {
        public SaveBarcodeSettingsResponse SaveBarcodeSettings(SaveBarcodeSettingsRequest objRequest)
        {
            SaveBarcodeSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 06.09.2018
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();

                if (objRequest.RequestDynamicData != null)
                {
                    var objBarcodeSettings = new List<BarcodeSettings>();
                    objBarcodeSettings.AddRange(objRequest.RequestDynamicData);
                    objRequest.BarcodeSettingsList = objBarcodeSettings;
                }

                objResponse = (SaveBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.InsertRecord(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.BARCODESETUP;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BarcodeSettingsBLL", "SaveBarcodeSettings");
                }   


                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                //var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                //objResponse = (SaveBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.InsertRecord(objRequest);
                
                ////if(objRequest.RequestDynamicData != null)
                ////{
                ////    var objBarcodeSettings = new BarcodeSettings();
                ////    objBarcodeSettings = (BarcodeSettings)objRequest.RequestDynamicData;
                ////    objRequest.BarcodeSettingsList = objBarcodeSettings.BarCodeList;
                ////}
                
                ////if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                ////{
                ////    objRequest.RequestFrom = objRequest.RequestFrom;
                ////    //objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                ////    objRequest.DocumentType = Enums.DocumentType.BARCODESETUP;
                ////    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                ////    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BarcodeSettingsBLL", "SaveBarcodeSettings");
                ////}             
            
            }
            catch (Exception ex)
            {
                objResponse = new SaveBarcodeSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllBarcodeSettingsResponse SelectAllBarcodeSettings(SelectAllBarcodeSettingsRequest objRequest)
        {
            SelectAllBarcodeSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                objResponse = (SelectAllBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllBarcodeSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllBarcodeSettingsResponse API_SelectAllBarcodeSettings(SelectAllBarcodeSettingsRequest objRequest)
        {
            SelectAllBarcodeSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                objResponse = (SelectAllBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllBarcodeSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDBarcodeSettingsResponse SelectBarcodeSettingsRecord(SelectByIDBarcodeSettingsRequest objRequest)
        {
            SelectByIDBarcodeSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                } 
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                objResponse = (SelectByIDBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDBarcodeSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateBarcodeSettingsResponse UpdateBarcodeSettings(UpdateBarcodeSettingsRequest objRequest)
        {
            UpdateBarcodeSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objBarcodeSettings = new BarcodeSettings();
                    objBarcodeSettings = (BarcodeSettings)objRequest.RequestDynamicData;
                    objRequest.BarcodeSettingsData = objBarcodeSettings;
                }
                //objResponse = (UpdateBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.BarcodeSettingsData.ID);
                //    objRequest.DocumentType = Enums.DocumentType.BARCODESETUP;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BarcodeSettingsBLL", "UpdateBarcodeSettings");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new UpdateBarcodeSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteBarcodeSettingsResponse DeleteBarcodeSettings(DeleteBarcodeSettingsRequest objRequest)
        {
            DeleteBarcodeSettingsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                objResponse = (DeleteBarcodeSettingsResponse)objBaseBarcodeSettingsDAL.DeleteRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //   // objRequest.DocumentIDs = Convert.ToString(objRequest.BarcodeSettingsData.ID);
                //    objRequest.DocumentType = Enums.DocumentType.BARCODESETUP;
                //    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BarcodeSettingsBLL", "DeleteBarcodeSettings");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new DeleteBarcodeSettingsResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectBarcodeSettingsLookUpResponse SelectBarcodeSettingsLookUp(SelectBarcodeSettingsLookUpRequest objRequest)
        {
            SelectBarcodeSettingsLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBarcodeSettingsDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                objResponse = (SelectBarcodeSettingsLookUpResponse)objBaseBarcodeSettingsDAL.SelectBarcodeSettingsLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBarcodeSettingsLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Barcode Settings");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectBarcodeGenerateBySKUResponse BarcodeGenerateBySKU(SelectBarcodeGenerateBySKURequest objRequest)
        {
            string SkuBarcodeGererate = string.Empty;
            string SumPrefixSuffix = string.Empty;
            string SumPrefixSuffixLength = string.Empty;
            string DynamicNo = string.Empty;
            int length = 0;
            string RunningNolength = string.Empty;
            var RequestData = (SelectBarcodeGenerateBySKURequest)objRequest;
            SelectBarcodeGenerateBySKUResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBarcodeGenerateBySKUDAL = objFactory.GetDALRepository().GetBarcodeSettingsDAL();
                objResponse = (SelectBarcodeGenerateBySKUResponse)objBarcodeGenerateBySKUDAL.SelectBarcodeGenerateBySKU(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                {
                    //SkuBarcodeGererate = objResponse.BarcodeGenerateBySKURecord.Prefix + objResponse.BarcodeGenerateBySKURecord.Suffix + objResponse.BarcodeGenerateBySKURecord.StartNumber;
                    SumPrefixSuffix = objResponse.BarcodeGenerateBySKURecord.Prefix + objResponse.BarcodeGenerateBySKURecord.Suffix;
                    SumPrefixSuffixLength = objResponse.BarcodeGenerateBySKURecord.Prefix + objResponse.BarcodeGenerateBySKURecord.Suffix + objResponse.BarcodeGenerateBySKURecord.RunningNo;
                    RunningNolength = Convert.ToString(objResponse.BarcodeGenerateBySKURecord.RunningNo);
                    length = (13 - (RunningNolength.Length));
                    if (SumPrefixSuffix.Length <= 13)
                    {
                        DynamicNo = SumPrefixSuffix.PadRight(length, '0');
                    }
                    SkuBarcodeGererate = DynamicNo + objResponse.BarcodeGenerateBySKURecord.RunningNo;
                }
            }
            catch (Exception ex)
            {
                objResponse = new SelectBarcodeGenerateBySKUResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "BarcodeGenerate");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
