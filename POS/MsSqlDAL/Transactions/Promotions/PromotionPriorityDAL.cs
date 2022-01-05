using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest;
using EasyBizRequest.Transactions.Promotions.PromotionPriority;
using EasyBizResponse;
using EasyBizResponse.Transactions.Promotions.PromotionPriority;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Promotions
{
    public class PromotionPriorityDAL : BasePromotionPriorityDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            SavePromotionPriorityRequest RequestData = (SavePromotionPriorityRequest)RequestObj;
            SavePromotionPriorityResponse ResponseData = new SavePromotionPriorityResponse();
            List<PromotionPriorityType> PromotionPriorityTypeList = RequestData.PromotionPriorityTypeData;
            StringBuilder sSql = new StringBuilder();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //transaction = _ConnectionObj.BeginTransaction();

                _CommandObj = new SqlCommand("InsertOrUpdatePromotionTypes", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                
                var PromotionPriority = _CommandObj.Parameters.Add("@PromotionPriority", SqlDbType.Xml);
                PromotionPriority.Direction = ParameterDirection.Input;
                PromotionPriority.Value = PromotionPriorityMasterXML(RequestData.PromotionPriorityTypeData);              

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@PrmoIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "PromotionPriority");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "PromotionPriority");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "PromotionPriority");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string PromotionPriorityMasterXML(List<PromotionPriorityType> PromotionPriorityList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (PromotionPriorityType objPromotionPriorityType in PromotionPriorityList)
            {
                sSql.Append("<PromotionPriorityTypeList>");
                sSql.Append("<ID>" + (objPromotionPriorityType.ID) + "</ID>");
                sSql.Append("<PriorityNo>" + (objPromotionPriorityType.PriorityNo) + "</PriorityNo>");
                sSql.Append("<PriceListID>" + (objPromotionPriorityType.PriceListID) + "</PriceListID>");
                sSql.Append("<PromotionID>" + (objPromotionPriorityType.PromotionID) + "</PromotionID>");
                sSql.Append("<PromotionName>" + (objPromotionPriorityType.PromotionName) + "</PromotionName>");
                sSql.Append("<PromotionCode>" + (objPromotionPriorityType.PromotionCode) + "</PromotionCode>");
                sSql.Append("<PriceListCode>" + (objPromotionPriorityType.PriceListCode) + "</PriceListCode>");
                sSql.Append("<Active>" + (objPromotionPriorityType.Active) + "</Active>");
                sSql.Append("</PromotionPriorityTypeList>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
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
            var PromotionPriorityTypeList = new List<PromotionPriorityType>();
            var RequestData = (SelectAllPromotionPriorityRequest)RequestObj;
            var ResponseData = new SelectAllPromotionPriorityResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from PromotionPriority with(NoLock) ORDER By PriorityNo ASC", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotionPriorityType = new PromotionPriorityType();


                        objPromotionPriorityType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionPriorityType.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;
                        objPromotionPriorityType.PromotionID = objReader["PromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionID"]) : 0;
                        objPromotionPriorityType.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotionPriorityType.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        PromotionPriorityTypeList.Add(objPromotionPriorityType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionPriorityTypeData = PromotionPriorityTypeList;
                    ResponseData.ResponseDynamicData = PromotionPriorityTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotion Priority");
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
            var PromotionPriorityList = new List<PromotionPriorityType>();
            var RequestData = (SelectByIDPromotionPriorityRequest)RequestObj;
            var ResponseData = new SelectByIDPromotionPriorityResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from PromotionPriority with(NoLock) where  PriceList='{0}' and Active='{1}'";
                sSql = string.Format(sSql, RequestData.ID,true);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotionPriorityType = new PromotionPriorityType();
                        objPromotionPriorityType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionPriorityType.PriceListID = objReader["PriceList"] != DBNull.Value ? Convert.ToInt32(objReader["PriceList"]) : 0;                           
                        objPromotionPriorityType.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;                           
                        objPromotionPriorityType.PromotionID = objReader["PromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionID"]) : 0;                           
                        objPromotionPriorityType.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotionPriorityType.PromotionCode = Convert.ToString(objReader["PromotionCode"]);

                        PromotionPriorityList.Add(objPromotionPriorityType);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionPriorityTypeList = PromotionPriorityList;
                    ResponseData.ResponseDynamicData = PromotionPriorityList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "PromotionPriority");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
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
