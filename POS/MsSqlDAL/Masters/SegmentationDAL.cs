using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizResponse.Masters.SegmentationMasterResponse;
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
    public class SegmentationDAL : BaseSegmentationMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSegmentRequest)RequestObj;
            var ResponseData = new SaveSegmentResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertSegmentationMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SegamationID", RequestData.SegmentationRecord.ID);
                _CommandObj.Parameters.AddWithValue("@SegamationName", RequestData.SegmentationRecord.SegmentName);
                _CommandObj.Parameters.AddWithValue("@MaxLength", RequestData.SegmentationRecord.MaxLength);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.SegmentationRecord.CreateBy);
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;
                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.Parameters.AddWithValue("@Active", RequestData.SegmentationRecord.Active);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.SegmentationRecord.Remarks);



                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Segmentation Type");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Segmentation Type");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segmentation Type");
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
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateSegmentRequest)RequestObj;
            var ResponseData = new UpdateSegmentResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdateSegmentationMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.SegmentMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@SegamationName", RequestData.SegmentMasterData.SegmentName);
                _CommandObj.Parameters.AddWithValue("@MaxLength", RequestData.SegmentMasterData.MaxLength);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.SegmentMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.SegmentMasterData.Active);
                //_CommandObj.Parameters.AddWithValue("@UpdateBy", RequestData.SegmentMasterData.UpdateBy);
                //_CommandObj.Parameters.AddWithValue("@SCN", RequestData.SegmentMasterData.SCN);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.SegmentMasterData.CreateBy);
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Segmentation Type");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Segmentation Type");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Segmentation Type");
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
        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (DeleteSegmentRequest)RequestObj;
            var ResponseData = new DeleteSengmentResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = "Delete from SegmentationMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Segment Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Segment Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SelectBySegmentIDRequest)RequestObj;
            var ResponseData = new SelectBySegmentIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                //string sSql = "Select * from CollectionMaster with(NoLock)  where ID='{0}'";
                //string sSql = "Select * from SegmentationMaster where SegmentName='SegmentName' ";
                string sSql = "Select * from SegmentationMaster  with(NoLock)  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSegmentMasterTypes = new SegmentMaster();
                        objSegmentMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSegmentMasterTypes.SegmentName = objReader["SegmentName"].ToString();
                        objSegmentMasterTypes.MaxLength = Convert.ToInt32(objReader["MaxLength"]);
                        objSegmentMasterTypes.Remarks = objReader["Remarks"].ToString();

                        objSegmentMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSegmentMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSegmentMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSegmentMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSegmentMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSegmentMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        ResponseData.SegmentMasterRecord = objSegmentMasterTypes;
                        ResponseData.ResponseDynamicData = objSegmentMasterTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Segment Master");
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
        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SegmentMasterTypes = new List<SegmentMaster>();
            var RequestData = (SelectAllSegmentRequest)RequestObj;
            var ResponseData = new SelectAllSegmentResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from SegmentationMaster ";

                

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSegmentMasterTypes = new SegmentMaster();
                        objSegmentMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSegmentMasterTypes.SegmentName = objReader["SegmentName"].ToString();
                        objSegmentMasterTypes.MaxLength = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["MaxLength"]) : 0;
                        objSegmentMasterTypes.Remarks = objReader["Remarks"].ToString();

                        objSegmentMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSegmentMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSegmentMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSegmentMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSegmentMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSegmentMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objSegmentMasterTypes.IsUsed = true;
                        objSegmentMasterTypes.DefaultDescription = true;

                        SegmentMasterTypes.Add(objSegmentMasterTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SegmentMasterList = SegmentMasterTypes;
                    ResponseData.ResponseDynamicData = SegmentMasterTypes;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Segment Master");
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

        //public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectAllSegmentResponse API_SelectALL(SelectAllSegmentRequest requestData)
        {
            var SegmentMasterTypes = new List<SegmentMaster>();
            var RequestData = (SelectAllSegmentRequest)requestData;
            var ResponseData = new SelectAllSegmentResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //string sSql = "Select * from SegmentationMaster ";

                var sSql = new StringBuilder();
                int myInt;
                bool isNumerical = int.TryParse(RequestData.SearchString, out myInt);

                if (isNumerical)
                {
                    sSql.Append("Select ID, SegmentName, MaxLength, Remarks, Active, RC.TOTAL_CNT [RecordCount] from SegmentationMaster with(NoLock) ");
                    sSql.Append("LEFT JOIN(Select  count(SM.ID) As TOTAL_CNT From SegmentationMaster SM with(NoLock) ");
                    sSql.Append("where SM.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or SM.SegmentName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or SM.MaxLength like isnull('%" + int.Parse(RequestData.SearchString) + "%','') ");
                    sSql.Append("or SM.Remarks like= isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");
                    sSql.Append("where Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or SegmentName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or MaxLength like isnull('%" + int.Parse(RequestData.SearchString) + "%','') ");
                    sSql.Append("or Remarks like= isnull('%" + RequestData.SearchString + "%','')) ");
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }
                else
                {
                    sSql.Append("Select ID, SegmentName, MaxLength, Remarks, Active, RecordCount = COUNT(*) OVER() from SegmentationMaster ");
                    sSql.Append("where Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or SegmentName = isnull('" + RequestData.SearchString + "','') ");
                    //sSql.Append("or MaxLength = isnull('" + RequestData.SearchString + "','') ");
                    sSql.Append("or Remarks = isnull('" + RequestData.SearchString + "','')) ");                    
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }


                //string sSql = "Select ID, SegmentName, MaxLength, Remarks, Active, RecordCount = COUNT(*) OVER() " +
                //   "from SegmentationMaster " +
                //   "where Active = " + RequestData.IsActive + " " +
                //       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //       "or SegmentName = isnull('" + RequestData.SearchString + "','') " +
                //       //"or MaxLength = isnull('" + RequestData.SearchString + "','') " +
                //       "or Remarks = isnull('" + RequestData.SearchString + "','')) " +
                //       "order by ID asc " +
                //       "offset " + RequestData.Offset + " rows " +
                //       "fetch first " + RequestData.Limit + " rows only";


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSegmentMasterTypes = new SegmentMaster();
                        objSegmentMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSegmentMasterTypes.SegmentName = objReader["SegmentName"].ToString();
                        objSegmentMasterTypes.MaxLength = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["MaxLength"]) : 0;
                        objSegmentMasterTypes.Remarks = objReader["Remarks"].ToString();

                        //objSegmentMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSegmentMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSegmentMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSegmentMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objSegmentMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSegmentMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objSegmentMasterTypes.IsUsed = true;
                        objSegmentMasterTypes.DefaultDescription = true;

                        SegmentMasterTypes.Add(objSegmentMasterTypes);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SegmentMasterList = SegmentMasterTypes;
                    //ResponseData.ResponseDynamicData = SegmentMasterTypes;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Segment Master");
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
    }
}