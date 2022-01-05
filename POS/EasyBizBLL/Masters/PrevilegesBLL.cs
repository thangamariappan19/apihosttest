using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizResponse.Masters.PrevilegesResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
   public class PrevilegesBLL
    {
       public SelectRoleLookupResponse RoleLookUp(SelectRoleLookupRequest objRequest)
       {
           SelectRoleLookupResponse objResponse = null;
           DALFactory objFactory = new DALFactory();
           try
           {
               var objBaseRoleDAL = objFactory.GetDALRepository().GetPrevilegesDAL();               
               objResponse = (SelectRoleLookupResponse)objBaseRoleDAL.SelectRoleLookUp(objRequest);              
           }
           catch (Exception ex)
           {
               objResponse = new SelectRoleLookupResponse();
               objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Role");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public GetScreenNamesResponse POSScreenNames(GetScreenNamesRequest objRequest)
       {
           GetScreenNamesResponse objResponse = null;
           DALFactory objFactory = new DALFactory();
           try
           {
               BasePrevilegesDAL objBaseMASUserPrivilagesDAL = objFactory.GetDALRepository().GetPrevilegesDAL();
               objResponse = (GetScreenNamesResponse)objBaseMASUserPrivilagesDAL.SelectPOSScreenNameLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new GetScreenNamesResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "MASScreenNames");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }      

        public SavePrevilegesResponse SaveMASUserprivilagesResponse(SavePrevilegesRequestt RequestData)
       {

           SavePrevilegesResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               RequestData.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
               var objBaseColorDAL = objFactory.GetDALRepository().GetPrevilegesDAL();
               if (RequestData.RequestDynamicData != null)
               {
                   var objMASUserIDPrivilages = new UserPrivilagesTypes();
                   objMASUserIDPrivilages = (UserPrivilagesTypes)RequestData.RequestDynamicData;
                   RequestData.UserPrivilagesData = objMASUserIDPrivilages;
               }
               objResponse = (SavePrevilegesResponse)objBaseColorDAL.InsertRecord(RequestData);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && RequestData.DataSync == false)
               {
                   RequestData.RequestFrom = Enums.RequestFrom.MainServer;
                   RequestData.DocumentIDs = Convert.ToString(objResponse.IDs);
                   RequestData.DocumentType = Enums.DocumentType.USERPREVILEGE;
                   RequestData.ProcessMode = Enums.ProcessMode.New;

                   //BackgroundServices _BackgroundServices = new BackgroundServices(RequestData, objResponse, "EasyBizBLL.Masters.PrevilegesBLL", "SaveMASUserprivilagesResponse");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SavePrevilegesResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Previlege Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;          
       }

       public SelectByUserIDPrivilagesResponse SelectUserIDPrivilagesResponse(SelectByUserIDPrivilagesRequest RequestData)
       {
           SelectByUserIDPrivilagesResponse objResponse = null;
           DALFactory objFactory = new DALFactory();
           try
           {
               BasePrevilegesDAL objBaseMASUserIDPrivilagesDAL = objFactory.GetDALRepository().GetPrevilegesDAL();
               if(RequestData.ID == null || RequestData.ID == 0)
               {
                   if(!string.IsNullOrEmpty(RequestData.DocumentIDs))
                   {
                       int Doc_Id;
                       int.TryParse(RequestData.DocumentIDs, out Doc_Id);
                       RequestData.ID = Doc_Id;
                   }
               }
               objResponse = (SelectByUserIDPrivilagesResponse)objBaseMASUserIDPrivilagesDAL.SelectRecord(RequestData);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByUserIDPrivilagesResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "MASUserIdPrivilages");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectPrevilegesLookUpResponse SelectPrevilegesLookUp(SelectPrevilegesLookUpRequest objRequest)
       {
           SelectPrevilegesLookUpResponse objResponse = null;
           DALFactory objFactory = new DALFactory();
           try
           {
               BasePrevilegesDAL objBaseMASUserPrivilagesDAL = objFactory.GetDALRepository().GetPrevilegesDAL();
               objResponse = (SelectPrevilegesLookUpResponse)objBaseMASUserPrivilagesDAL.SelectUserPrivilagesLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectPrevilegesLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "MASScreenNames");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }


    }
}
