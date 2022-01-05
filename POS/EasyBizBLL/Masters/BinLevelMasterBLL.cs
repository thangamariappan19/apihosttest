using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.BinRequest;
using EasyBizResponse.Masters.BinMasterRespose;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class BinLevelMasterBLL
    {
        public SaveBinLevelResponse SaveBinConfigMaster(SaveBinLevelMasterRequest objRequest)
        {
            SaveBinLevelResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                if (objRequest.RequestDynamicData != null)
                {
                    objRequest.BinLevelMasterRecord = (BinLevelMasterTypes)objRequest.RequestDynamicData;
                }
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinLevelMasterDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objAgent = new BinLevelMasterTypes();
                    objAgent = (BinLevelMasterTypes)objRequest.RequestDynamicData;
                    objRequest.BinLevelMasterRecord = objAgent;

                }
                objResponse = (SaveBinLevelResponse)objBaseAgentMasterDAL.InsertRecord(objRequest);

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
                objResponse = new SaveBinLevelResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Bin Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllBinConfigMasterResponse SelectAllBinConfig(SelectByIDBinMasterRequest objRequest)
        {
            SelectAllBinConfigMasterResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinLevelMasterDAL();
                objResponse = (SelectAllBinConfigMasterResponse)objBaseAgentMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllBinConfigMasterResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
