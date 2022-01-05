using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest;
using EasyBizRequest.Transactions.POS.TransactionStatusRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.POS.TransactionStatusResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.POS
{
    public class TransactionStatusDAL : BaseTransactionStatusDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var TransactionStatusTypesList = new List<TransactionStatusTypes>();
            var RequestData = (SelectAllTransactionStatusRequest)RequestObj;
            var ResponseData = new SelectAllTransactionStatusResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from TransactionStatus with(NoLock) where Active='true'";
                sSql = string.Format(sSql, RequestData.ShowInActiveRecords);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionStatusTypes = new TransactionStatusTypes();
                        objTransactionStatusTypes.ID = Convert.ToInt32(objReader["ID"]);
                        objTransactionStatusTypes.Code = Convert.ToString(objReader["Code"]);
                        objTransactionStatusTypes.Name = Convert.ToString(objReader["Name"]);
                        TransactionStatusTypesList.Add(objTransactionStatusTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TransactionStatusList = TransactionStatusTypesList;
                    ResponseData.ResponseDynamicData = TransactionStatusTypesList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Transaction Status");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
