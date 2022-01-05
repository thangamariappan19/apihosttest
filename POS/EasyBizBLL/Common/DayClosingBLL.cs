using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.Common;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Common;
using EasyBizResponse.Transactions.POS.Invoice;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
    public class DayClosingBLL
    {
        public SaveDayClosingResponse SaveDayClosing(SaveDayClosingRequest objRequest)
        {
            SaveDayClosingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.DayClosingRecord = (DayClosing)objRequest.RequestDynamicData;
                }  

                var objBaseDayClosingMasterDAL = objFactory.GetDALRepository().GetBaseDayClosingDAL();
                objResponse = (SaveDayClosingResponse)objBaseDayClosingMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DayClosingRecord.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.DAYCLOSING;
                //    objRequest.ProcessMode = Enums.ProcessMode.New;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Common.DayClosingBLL", "SaveDayClosing");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveDayClosingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectShiftLogResponse SelectJoinShiftAmount(SelectShiftLogRequest objRequest)
        {
            SelectShiftLogResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseDayClosingMasterDAL = objFactory.GetDALRepository().GetShiftMasterDAL();
                objResponse = (SelectShiftLogResponse)objBaseDayClosingMasterDAL.SelectJoinShiftAmount(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectShiftLogResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        
        public SaveDayClosingResponse UpdateDayClosing(SaveDayClosingRequest objRequest)
        {
            SaveDayClosingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseDayClosingMasterDAL = objFactory.GetDALRepository().GetBaseDayClosingDAL();
                
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.DayClosingRecord = (DayClosing)objRequest.RequestDynamicData;
                }  

                objResponse = (SaveDayClosingResponse)objBaseDayClosingMasterDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DayClosingRecord.ID = Convert.ToInt32(objResponse.IDs);
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.DAYCLOSING;
                //    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                //    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Common.DayClosingBLL", "UpdateDayClosing");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveDayClosingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "DayClosing Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
