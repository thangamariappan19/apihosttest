using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizResponse.Masters.ColorMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ColorBLL
    {
        public SaveColorResponse SaveColor(SaveColorRequest objRequest)
        {
            SaveColorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.ColorRecord = (ColorMaster)objRequest.RequestDynamicData;
                }
                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objColorMasterRecord = new ColorMaster();
                    objColorMasterRecord = (ColorMaster)objRequest.RequestDynamicData;
                    objRequest.ColorRecord = objColorMasterRecord;
                }
                objResponse = (SaveColorResponse)objBaseColorDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.ColorRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COLORMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ColorBLL", "SaveColor");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveColorResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllColorResponse API_SelectALL(SelectAllColorRequest requestData)
        {
            var _SubBrandBLL = new SubBrandBLL();
            SelectAllColorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                objResponse = (SelectAllColorResponse)objBaseColorDAL.API_SelectALL(requestData);
            }

            catch (Exception ex)
            {
                objResponse = new SelectAllColorResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

        public DeleteColorResponse DeleteColor(DeleteColorRequest objRequest)
        {
            DeleteColorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                objResponse = (DeleteColorResponse)objBaseColorDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                  //  objRequest.ColorRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.COLORMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ColorBLL", "DeleteColor");
                }
               
            }
            catch (Exception ex)
            {
                objResponse = new DeleteColorResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllColorResponse SelectAllColorRecords(SelectAllColorRequest objRequest)
        {
            var _SubBrandBLL = new SubBrandBLL();
            SelectAllColorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                objResponse = (SelectAllColorResponse)objBaseColorDAL.SelectAll(objRequest);              
           }

            catch (Exception ex)
            {
                objResponse = new SelectAllColorResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public UpdateColorResponse UpdateColor(UpdateColorRequest objRequest)
        {
            UpdateColorResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objColorMasterRecord = new ColorMaster();
                    objColorMasterRecord = (ColorMaster)objRequest.RequestDynamicData;
                    objRequest.ColorRecord = objColorMasterRecord;
                }
                objResponse = (UpdateColorResponse)objBaseColorDAL.UpdateRecord(objRequest);
             if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objRequest.ColorRecord.ID);
                    objRequest.DocumentType = Enums.DocumentType.COLORMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.Edit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ColorBLL", "UpdateColor");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateColorResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByColorIDResponse SelectColorRecord(SelectByColorIDRequest objRequest)
        {
            SelectByColorIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                if (objRequest.ID == 0)
                {
                    objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
                }  
                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                objResponse = (SelectByColorIDResponse)objBaseColorDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByColorIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectColorLookUpResponse SelectColorLookup(SelectColorLookUpRequest objRequest)
        {
            SelectColorLookUpResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseColorDAL = objFactory.GetDALRepository().GetColorMasterDAL();
                objResponse = (SelectColorLookUpResponse)objBaseColorDAL.SelectColorLookUp(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectColorLookUpResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Color Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
