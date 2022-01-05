using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest.Masters.BinTransferMasterRequest;
using EasyBizResponse.Masters.BinTransferResponse;

namespace EasyBizBLL.Masters
{
    public class BinTransferMasterBLL
    {
        public SelectBinDetailsBySKUCodeResponse SelectBinTransferDetails(SelectBinDetailsBySKUCodeRequest objRequest)
        {
            SelectBinDetailsBySKUCodeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinTransferMasterDAL();
                objResponse = (SelectBinDetailsBySKUCodeResponse)objBaseAgentMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBinDetailsBySKUCodeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SaveBinTransferResponse UpdateBinTransferDetails(SaveBinTransferRequest objRequest)
        {
            SaveBinTransferResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseAgentMasterDAL = objFactory.GetDALRepository().GetBinTransferMasterDAL();
                objResponse = (SaveBinTransferResponse)objBaseAgentMasterDAL.UpdateRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SaveBinTransferResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Details");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
