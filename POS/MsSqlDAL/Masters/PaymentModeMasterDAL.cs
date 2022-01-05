using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.PaymentModeMaterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.PaymentModeMasterResponse;
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
    public class PaymentModeMasterDAL : EasyBizAbsDAL.Masters.BasePaymentModeMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

       
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SavePaymentModeMasterRequest)RequestObj;
            var ResponseData = new SavePaymentModeMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertPaymentModeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@PaymentModeCode", RequestData.PaymentModeTypesData.PaymentModeCode); 
                _CommandObj.Parameters.AddWithValue("@PaymentModeName", RequestData.PaymentModeTypesData.PaymentModeName); 

                _CommandObj.Parameters.AddWithValue("@SortOrder", RequestData.PaymentModeTypesData.SortOrder);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PaymentModeTypesData.Remarks);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.PaymentModeTypesData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PaymentModeTypesData.Active);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Payment Mode Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Mode Master");
                    ResponseData.ExceptionMessage = StatusMsg.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override SelectAllPaymentModeMasterResponse SelectAll(SelectAllPaymentModeMasterRequest objRequest)
        {
            var PaymentTypeDataList = new List<PaymentModeTypes>();

            var RequestData = new SelectAllPaymentModeMasterRequest();
            var ResponseData = new SelectAllPaymentModeMasterResponse();

            RequestData = (SelectAllPaymentModeMasterRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                string sSql = " Select pm.ID, pm.Payment_mode_code, pm.payment_mode_name                          " +
                            " 	, pm.Sort_order, pm.Remarks, pm.Active, rc.total_cnt [RecordCount]                " +
                            " from PaymentmodeMaster pm                                                           " +
                            " left join(Select  COUNT(id) AS total_cnt                                            " +
                            " 		from PaymentmodeMaster                                                        " +
                            " 		where Active = " + RequestData.IsActive + " " +
                            "           and (isnull('" + RequestData.SearchString + "','') = '' " +
                            "           or Payment_mode_code like isnull('%" + RequestData.SearchString + "%','')      " +
                            " 			or payment_mode_name like isnull('%" + RequestData.SearchString + "%',''))                                     " +
                            " ) AS rc on 1= 1                                                                     " +
                            " where Active = " + RequestData.IsActive + " " +
                            "   and (isnull('" + RequestData.SearchString + "','') = '' " +
                            "   or Payment_mode_code like isnull('%" + RequestData.SearchString + "%','')       " +
                            " 	or payment_mode_name like isnull('%" + RequestData.SearchString + "%',''))                                             " +
                            " order by ID asc " +
                            " offset " + RequestData.Offset + " rows fetch first " + RequestData.Limit + " rows only                              ";
               

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objPaymentModeMaster = new PaymentModeTypes();

                        objPaymentModeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objPaymentModeMaster.PaymentModeCode = objReader["Payment_mode_code"].ToString();
                        objPaymentModeMaster.PaymentModeName = objReader["payment_mode_name"].ToString();
                        objPaymentModeMaster.SortOrder = objReader["Sort_order"].ToString();
                        objPaymentModeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                       
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                        PaymentTypeDataList.Add(objPaymentModeMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PaymentModeMasterDate = PaymentTypeDataList;
                    //ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Mode Master");
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

        public override SelectPaymentModeLooKUpResponse SelectPaymentModeRecord(SelectPaymentModeLooKUpRequest RequestObj)
        {
            var PaymentTypeDataList = new List<PaymentModeTypes>();
            var RequestData = (SelectPaymentModeLooKUpRequest)RequestObj;
            var ResponseData = new SelectPaymentModeLooKUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select ID,Payment_mode_code,payment_mode_name,Sort_order,Active from PaymentmodeMaster with(NoLock) where Active='True' order by Sort_order asc";
                sSql = string.Format(sSql);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPaymentModeMaster = new PaymentModeTypes();
                        objPaymentModeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentModeMaster.PaymentModeCode = objReader["Payment_mode_code"].ToString();
                        objPaymentModeMaster.PaymentModeName = objReader["payment_mode_name"].ToString();
                        objPaymentModeMaster.SortOrder = objReader["Sort_order"].ToString();
                        objPaymentModeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                    
                        PaymentTypeDataList.Add(objPaymentModeMaster);
                       
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PaymentModeMasterDate = PaymentTypeDataList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Mode Master");

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

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var PaymentModeRecord = new PaymentModeTypes();
            var RequestData = (SelectByIDPaymentModeMasterRequest)RequestObj;
            var ResponseData = new SelectByIDPaymentModeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from PaymentmodeMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPaymentModeMaster = new PaymentModeTypes();
                        objPaymentModeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentModeMaster.PaymentModeCode = objReader["Payment_mode_code"].ToString();
                        objPaymentModeMaster.PaymentModeName = objReader["payment_mode_name"].ToString();
                        objPaymentModeMaster.SortOrder = objReader["Sort_order"].ToString();
                        objPaymentModeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPaymentModeMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objPaymentModeMaster.CreateOn = objReader["CreatOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatOn"]) : DateTime.Now;
                        objPaymentModeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPaymentModeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPaymentModeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                       
                        ResponseData.PaymentModeTypeRecord = objPaymentModeMaster;
                        ResponseData.IDs = objPaymentModeMaster.ID.ToString();
                        //ResponseData.ResponseDynamicData = ;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Mode Master");

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

       

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdatePaymentModeMasterRequest)RequestObj;
            var ResponseData = new UpdatePaymentModeMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdatePaymentModeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@ID", RequestData.PaymentModeMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@PaymentModeCode", RequestData.PaymentModeMasterData.PaymentModeCode); // enterprise server Id
                _CommandObj.Parameters.AddWithValue("@PaymentModeName", RequestData.PaymentModeMasterData.PaymentModeName);
                _CommandObj.Parameters.AddWithValue("@SortOrder", RequestData.PaymentModeMasterData.SortOrder);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PaymentModeMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.PaymentModeMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PaymentModeMasterData.Active);

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Payment Mode Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Mode Master");
                    ResponseData.ExceptionMessage = StatusMsg.Value.ToString();
                }



            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
          
        }
    }
}
