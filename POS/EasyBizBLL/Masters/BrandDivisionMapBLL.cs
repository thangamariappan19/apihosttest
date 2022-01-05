using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class BrandDivisionMapBLL
    {
         public SaveBrandDivisionMapResponse SaveBrandDivision(SaveBrandDivisionMapRequest objRequest)
        {
              SaveBrandDivisionMapResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 10.09.2018
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    //var objBrandDivision = new BrandDivisionTypes();
                    //objBrandDivision = (BrandDivisionTypes)objRequest.RequestDynamicData;
                    //objRequest.BrandDivisionList = objBrandDivision.BrandDivisionList;

                    var objBrandDivision = new List< BrandDivisionTypes>();
                    objBrandDivision.AddRange(objRequest.RequestDynamicData);
                    objRequest.BrandDivisionList = objBrandDivision;
                }
                objResponse = (SaveBrandDivisionMapResponse)objBaseBrandDivisionDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.BRANDDIVISIONMAP;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BrandDivisionMapBLL", "SaveBrandDivision");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveBrandDivisionMapResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "BrandDivision Map");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

      

        public DeleteBrandDivisionMapResponse DeleteBrandDivision(DeleteBrandDivisionRequest objRequest)
        {
              DeleteBrandDivisionMapResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();
                objResponse = (DeleteBrandDivisionMapResponse)objBaseBrandDivisionDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    //objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.BRANDDIVISIONMAP;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BrandDivisionMapBLL", "DeleteBrandDivision");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteBrandDivisionMapResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "BrandDivision Map");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

         public SelectAllBrandDivisionMapResponse SelectAllBrandDivisionRecords(SelectAllBrandDivisionRequest objRequest)
         {
             SelectAllBrandDivisionMapResponse objResponse = null;
             var objFactory = new DALFactory();
             try
             {
                 var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();

                 // Changed by Senthamil @ 10.09.2018

                 if(objRequest.BrandID == null || objRequest.BrandID == 0)
                 {
                     if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                     {
                         int BrandID;
                         int.TryParse(objRequest.DocumentIDs,out BrandID);
                         objRequest.BrandID = BrandID;
                     }
                 }
                 objResponse = (SelectAllBrandDivisionMapResponse)objBaseBrandDivisionDAL.SelectAll(objRequest);
             }
             catch (Exception ex)
             {
                 objResponse = new SelectAllBrandDivisionMapResponse();
                 objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "BrandDivision Map");
                 objResponse.ExceptionMessage = ex.Message;
                 objResponse.StackTrace = ex.StackTrace;
             }

             return objResponse;
         }

         public UpdateBrandDivisionMapResponse UpdateBrandDivision(UpdateBrandDivisionMapRequest objRequest)
        {
            UpdateBrandDivisionMapResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();
                if(objRequest.RequestDynamicData != null)
                {
                    var objBrandDivision = new BrandDivisionTypes();
                    objBrandDivision = (BrandDivisionTypes)objRequest.RequestDynamicData;
                    objRequest.BrandDivisionData = objBrandDivision;
                }
                objResponse = (UpdateBrandDivisionMapResponse)objBaseBrandDivisionDAL.UpdateRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;                  
                    objRequest.DocumentType = Enums.DocumentType.BRANDDIVISIONMAP;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.BrandDivisionMapBLL", "UpdateBrandDivision");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateBrandDivisionMapResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "BrandDivision Map");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

         public SelectByBrandDivisionIDResponse SelectBrandDivisionRecord(SelectByBrandDivisionIDRequest objRequest)
         {
             SelectByBrandDivisionIDResponse objResponse = null;
             var objFactory = new DALFactory();
             try
             {
                 var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();
                 objResponse = (SelectByBrandDivisionIDResponse)objBaseBrandDivisionDAL.SelectRecord(objRequest);
             }
             catch (Exception ex)
             {
                 objResponse = new SelectByBrandDivisionIDResponse();
                 objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "BrandDivision Map");
                 objResponse.ExceptionMessage = ex.Message;
                 objResponse.StackTrace = ex.StackTrace;
             }

             return objResponse;
         }

         public SelectBrandDivisionMapLookUpResponse BrandDivisionLookUp(SelectBrandDivisionLookUpRequest objRequest)
         {
             SelectBrandDivisionMapLookUpResponse objResponse = null;
             var objFactory = new DALFactory();
             try
             {
                 var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();
                 objResponse = (SelectBrandDivisionMapLookUpResponse)objBaseBrandDivisionDAL.SelectBrandDivisionMapLookUp(objRequest);
             }
             catch (Exception ex)
             {
                 objResponse = new SelectBrandDivisionMapLookUpResponse();
                 objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "BrandDivision Map");
                 objResponse.ExceptionMessage = ex.Message;
                 objResponse.StackTrace = ex.StackTrace;
             }
             return objResponse;
         }

         public SelectBrandDivListforCategoryResponse BrandDivisionByBrand(SelectBrandDivListforCategoryRequest objRequest)
         {
             SelectBrandDivListforCategoryResponse objResponse = null;
             var objFactory = new DALFactory();
             try
             {
                 var objBaseBrandDivisionDAL = objFactory.GetDALRepository().GetBrandDivisionMapDAL();
                 objResponse = (SelectBrandDivListforCategoryResponse)objBaseBrandDivisionDAL.SelectBrandDivisionListByBrand(objRequest);
             }
             catch (Exception ex)
             {
                 objResponse = new SelectBrandDivListforCategoryResponse();
                 objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand Division");
                 objResponse.ExceptionMessage = ex.Message;
                 objResponse.StackTrace = ex.StackTrace;
             }
             return objResponse;

         }
    }
}
