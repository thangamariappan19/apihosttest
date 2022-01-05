using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.BinSubLevelRequest;
using EasyBizResponse.Masters.BinSubLevelResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class BinLevelDetailsBLL
    {
        public SaveBinSubLevelResponse SaveBinConfigDetails(SaveBinLevelRequest objRequest)
        {
            SaveBinSubLevelResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.BinLevelDetailsRecord = (BinLevelDetailsTypes)objRequest.RequestDynamicData;
                }
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinLevelDetailsDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objBinLevel = new BinLevelDetailsTypes();
                    objBinLevel = (BinLevelDetailsTypes)objRequest.RequestDynamicData;
                    objRequest.BinLevelDetailsRecord = objBinLevel;

                }
                objResponse = (SaveBinSubLevelResponse)objBaseAgentMasterDAL.InsertRecord(objRequest);

                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    /*objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.AgentRecord.ID = Convert.ToInt32(objResponse.IDs);
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentNos = objRequest.AgentRecord.AgentCode;
                    objRequest.DocumentType = Enums.DocumentType.AGENTMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.New;*/

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.AgentBLL", "SaveAgent");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveBinSubLevelResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Bin Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllBinSubLevelResponse SelectAllBinConfig(SelectAllBinLevelRequest objRequest)
        {
            SelectAllBinSubLevelResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinLevelDetailsDAL();
                objResponse = (SelectAllBinSubLevelResponse)objBaseAgentMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllBinSubLevelResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDBinLevelDetailsResponse SelectBinConfigByID(SelectByIDBinLevelRequest objRequest)
        {
            SelectByIDBinLevelDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinLevelDetailsDAL();
                objResponse = (SelectByIDBinLevelDetailsResponse)objBaseAgentMasterDAL.SelectByIDs(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDBinLevelDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByIDBinLevelDetailsResponse SelectRecordBinConfigByID(SelectByIDBinLevelRequest objRequest)
        {
            SelectByIDBinLevelDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinLevelDetailsDAL();
                objResponse = (SelectByIDBinLevelDetailsResponse)objBaseAgentMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDBinLevelDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }

}
