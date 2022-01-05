using EasyBizAbsDAL.Transactions.DiscountMaster;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizFactory;
using EasyBizRequest.Transactions.DiscountMasterRequest;
using EasyBizResponse.Transactions.Promotions.DiscountMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Promotions
{
   public class DiscountMasterBLL 
    {

       public SaveDiscountMasterResponse SaveDiscountMaster(SaveDiscountMasterRequest objRequest)
       {
           SaveDiscountMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                BaseDiscountMasterDAL objBaseDiscountMasterDAL = objFactory.GetDALRepository().GetDiscountMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjDiscountMaster = new DiscountMasterTypes();
                   ObjDiscountMaster = (DiscountMasterTypes)objRequest.RequestDynamicData;
                   objRequest.CustomerGroupID = ObjDiscountMaster.CustomerGroupID;
                   objRequest.CountryIDs = ObjDiscountMaster.CountryIDs;
                   objRequest.StoreIDs = ObjDiscountMaster.StoreIDs;
                   objRequest.DiscountType = ObjDiscountMaster.DiscountType;                 
                   objRequest.DiscountMasterRecord = ObjDiscountMaster;
                   objRequest.EmployeeDiscountDetailList = ObjDiscountMaster.EmployeeDiscountDetails;
                   objRequest.FamilyDiscountDetailList = ObjDiscountMaster.FamilyDiscountDetails;
               }
               objResponse = (SaveDiscountMasterResponse)objBaseDiscountMasterDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.DISCOUNTMASTER;
                   objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                 //  objRequest.BaseIntegrateStoreID = objRequest.DiscountMasterRecord.StoreID;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Promotions.DiscountMasterBLL", "SaveDiscountMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveDiscountMasterResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectAllDiscountMasterResponse SelectAllMappingRecords(SelectAllDiscountMasterRequest objRequest)
       {
           SelectAllDiscountMasterResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseDiscountMasterDAL = objFactory.GetDALRepository().GetDiscountMasterDAL();
               objResponse = (SelectAllDiscountMasterResponse)objBaseDiscountMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllDiscountMasterResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "DiscountMaster");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }

           return objResponse;
       }
    }
}
