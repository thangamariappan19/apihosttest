using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizResponse.Masters.TailoringMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class TailoringMasterBLL
    {
       public SelectAllTailoringResponse SelectAllTailoringUnit(SelectAllTailoringRequest objRequest)
       {
           SelectAllTailoringResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseTailoringMasterDAL = objFactory.GetDALRepository().GetTailoringMasterDAL();
               objResponse = (SelectAllTailoringResponse)objBaseTailoringMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllTailoringResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Unit Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SaveTailoringResponse SaveTailoringmaster(SaveTailoringRequest objRequest)
       {
           SaveTailoringResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               if (objRequest.RequestDynamicData != null)
               {
                   objRequest.TailoringMasterRecord = (TailoringMasterTypes)objRequest.RequestDynamicData;
               }
               var objBaseTailoringMasterDAL = objFactory.GetDALRepository().GetTailoringMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objTailoring = new TailoringMasterTypes();
                   objTailoring = (TailoringMasterTypes)objRequest.RequestDynamicData;
                   objRequest.TailoringMasterRecord = objTailoring;

               }
               objResponse = (SaveTailoringResponse)objBaseTailoringMasterDAL.InsertRecord(objRequest);

               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.TailoringMasterRecord.ID = Convert.ToInt32(objResponse.IDs);
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentNos = objRequest.TailoringMasterRecord.tailoringunitcode;
                   objRequest.DocumentType = Enums.DocumentType.TAILORINGMASTER;
                   objRequest.ProcessMode = Enums.ProcessMode.New;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.TailoringMasterBLL", "SaveTailoringmaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveTailoringResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Unit Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectByTailoringIDResponse SelectTailoringUnitRecord(SelectByTailoringIDRequest objRequest)
       {
           SelectByTailoringIDResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }
               var objBaseTaioringMasterDAL = objFactory.GetDALRepository().GetTailoringMasterDAL();
               objResponse = (SelectByTailoringIDResponse)objBaseTaioringMasterDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByTailoringIDResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Unit Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

	   public SelectAllTailoringMasterByStoreResponse SelectTailoringMasterLookup(SelectAllTailoringMasterByStoreRequest objRequest)
	   {
		   SelectAllTailoringMasterByStoreResponse objResponse = null;
		   var objFactory = new DALFactory();
		   try
		   {
			   var objBaseTailoringMasterDAL = objFactory.GetDALRepository().GetTailoringMasterDAL();
			   objResponse = (SelectAllTailoringMasterByStoreResponse)objBaseTailoringMasterDAL.SelectTailorMasterLookUp(objRequest);
		   }
		   catch (Exception ex)
		   {
			   objResponse = new SelectAllTailoringMasterByStoreResponse();
			   objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Unit Master");
			   objResponse.ExceptionMessage = ex.Message;
			   objResponse.StackTrace = ex.StackTrace;
		   }
		   return objResponse;
	   }

        public SelectAllTailoringResponse API_SelectALL(SelectAllTailoringRequest objRequest)
        {
            SelectAllTailoringResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseTailoringMasterDAL = objFactory.GetDALRepository().GetTailoringMasterDAL();
                objResponse = (SelectAllTailoringResponse)objBaseTailoringMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllTailoringResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Tailoring Unit Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
