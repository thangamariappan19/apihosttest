using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.LabelPrintingRequest;
using EasyBizResponse.Masters.LabelPrintingResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
   public class LabelPrintingDAL : BaseLabelPrintingDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override CommonLabelPrintingReportResponse GetLabelPrintingReport(CommonLabelReportRequest ObjRequest)
        {
            var RequestData = (CommonLabelReportRequest)ObjRequest;
            var ResponseData = new CommonLabelPrintingReportResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();

            DataTable objDataTable = new DataTable();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SP_LabelPrintingReport", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StoreID;

                SqlParameter Department = _CommandObj.Parameters.Add("@Department", SqlDbType.VarChar);
                Department.Direction = ParameterDirection.Input;
                Department.Value = RequestData.Department;

                SqlParameter ProductCode = _CommandObj.Parameters.Add("@ProductCode", SqlDbType.VarChar);
                ProductCode.Direction = ParameterDirection.Input;
                ProductCode.Value = RequestData.ProductCode;

                SqlParameter ColorCode = _CommandObj.Parameters.Add("@ColorCode", SqlDbType.VarChar);
                ColorCode.Direction = ParameterDirection.Input;
                ColorCode.Value = RequestData.ColorCode;

                SqlParameter SizeCode = _CommandObj.Parameters.Add("@SizeCode", SqlDbType.VarChar);
                SizeCode.Direction = ParameterDirection.Input;
                SizeCode.Value = RequestData.SizeCode;

                SqlParameter NoOfLabel = _CommandObj.Parameters.Add("@NoOfLabel", SqlDbType.Int);
                NoOfLabel.Direction = ParameterDirection.Input;
                NoOfLabel.Value = RequestData.NoOfLabel;

                SqlParameter PrintPrice = _CommandObj.Parameters.Add("@PrintPrice", SqlDbType.Bit);
                PrintPrice.Direction = ParameterDirection.Input;
                PrintPrice.Value = RequestData.PrintPrice;

                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlDataReader objReader;

                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    objDataTable.Load(objReader);
                }

                if (objDataTable.Rows.Count > 0)
                {
                    ResponseData.LabelPrintingDataTable = objDataTable;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.LabelPrintingDataTable = new DataTable();
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Label Printing Report");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Label Printing Report");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
