using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizFactory;
using EasyBizRequest.Transactions.Promotions.PromotionMappingRequest;
using EasyBizResponse.Transactions.Promotions.PromotionMappingResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.Promotions
{
    public class PromotionMappingBLL
    {
        public SavePromotionMappingResponse SavePromotionMapping(SavePromotionMappingRequest objRequest)
        {
            SavePromotionMappingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBasePromotionMapDAL = objFactory.GetDALRepository().GetPromotionMappingDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    // Changed by Senthamil @ 07.09.2018
                    var objPromotionMapping = new List< PromotionMappingTypes>();
                    objPromotionMapping.AddRange(objRequest.RequestDynamicData);
                    objRequest.PromotionMappingList = objPromotionMapping;

                    //var objPromotionMapping = new PromotionMappingTypes();
                    //objPromotionMapping = (PromotionMappingTypes)objRequest.RequestDynamicData;
                    //objRequest.PromotionMappingList = objPromotionMapping.PromotionMappingList;
                }
                objResponse = (SavePromotionMappingResponse)objBasePromotionMapDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.PROMOTIONMAPPING;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Promotions.PromotionMappingBLL", "SavePromotionMapping");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SavePromotionMappingResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotion Mapping");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllPromotionMappingResponse SelectAllPromotionMapping(SelectAllPromotionMappingRequest objRequest)
        {
            SelectAllPromotionMappingResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBasePromotionMapDAL = objFactory.GetDALRepository().GetPromotionMappingDAL();
                // Changed by Senthamil @ 07.09.2018
                if(objRequest.WNPromotionID == null || objRequest.WNPromotionID == 0)
                {
                    if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        objRequest.WNPromotionID = int.Parse(objRequest.DocumentIDs);
                    }
                }
                objResponse = (SelectAllPromotionMappingResponse)objBasePromotionMapDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllPromotionMappingResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Promotion Mapping");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
