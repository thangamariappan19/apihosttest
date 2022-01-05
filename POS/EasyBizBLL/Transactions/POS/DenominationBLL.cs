using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.DenominationRequest;
using EasyBizResponse.Transactions.POS.DenominationResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class DenominationBLL
    {
        public SaveDenominationResponse SaveDenomination(SaveDenominationRequest objRequest)
        {
            SaveDenominationResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseDenominationDAL = objFactory.GetDALRepository().GetDenominationDAL();
                objResponse = (SaveDenominationResponse)objBaseDenominationDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                   // objRequest.RequestFrom = objRequest.RequestFrom;
                   // objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.DENOMINATION;
                    //objRequest.ProcessMode = Enums.ProcessMode.New;

                   // BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.DenominationBLL", "SaveDenomination");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveDenominationResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Denomination");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
