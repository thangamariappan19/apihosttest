using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizResponse.Masters.TailoringMasterResponse;
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
    public class TailoringMasterDAL : BaseTailoringMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveTailoringRequest)RequestObj;
            var ResponseData = new SaveTailoringResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateTailoringUnit", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter TailoringUnitID = _CommandObj.Parameters.Add("@TailoringUnitID", SqlDbType.Int);
                TailoringUnitID.Direction = ParameterDirection.Input;
                TailoringUnitID.Value = RequestData.TailoringMasterRecord.ID;

                SqlParameter tailoringunitcode = _CommandObj.Parameters.Add("@tailoringunitcode", SqlDbType.NVarChar);
                tailoringunitcode.Direction = ParameterDirection.Input;
                tailoringunitcode.Value = RequestData.TailoringMasterRecord.tailoringunitcode;

                SqlParameter tailoringunitName = _CommandObj.Parameters.Add("@tailoringunitName", SqlDbType.NVarChar);
                tailoringunitName.Direction = ParameterDirection.Input;
                tailoringunitName.Value = RequestData.TailoringMasterRecord.tailoringunitName;              

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.TailoringMasterRecord.Active;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.TailoringMasterRecord.CountryID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.TailoringMasterRecord.CountryCode;
               
                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.TailoringMasterRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.TailoringMasterRecord.StoreCode;                             

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.TailoringMasterRecord.CreateBy;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.VarChar, 10);
                ID1.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Tailoring Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Tailoring Master");
                }
                else
                {
                    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
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
            var TailoringMasterRecord = new TailoringMasterTypes();
            var RequestData = (SelectByTailoringIDRequest)RequestObj;
            var ResponseData = new SelectByTailoringIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from TailoringUnitMaster  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTailoringMaster = new TailoringMasterTypes();
                        objTailoringMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTailoringMaster.tailoringunitcode = Convert.ToString(objReader["tailoringunitcode"]);
                        objTailoringMaster.tailoringunitName = Convert.ToString(objReader["tailoringunitName"]);
                        objTailoringMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objTailoringMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objTailoringMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objTailoringMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objTailoringMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objTailoringMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        ResponseData.TailoringMasterRecord = objTailoringMaster;

                        ResponseData.ResponseDynamicData = objTailoringMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Unit Master");
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
            var TailoringMasterList = new List<TailoringMasterTypes>();
            var RequestData = (SelectAllTailoringRequest)RequestObj;
            var ResponseData = new SelectAllTailoringResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from TailoringUnitMaster with(NoLock) where Active='{0}'";
                string sSql = "Select * from TailoringUnitMaster  ";



                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTailoringMaster = new TailoringMasterTypes();
                        objTailoringMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTailoringMaster.tailoringunitcode = Convert.ToString(objReader["tailoringunitcode"]);
                        objTailoringMaster.tailoringunitName = Convert.ToString(objReader["tailoringunitName"]);
                        objTailoringMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objTailoringMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objTailoringMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objTailoringMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objTailoringMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objTailoringMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        TailoringMasterList.Add(objTailoringMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TailoringMasterList = TailoringMasterList;

                    ResponseData.ResponseDynamicData = TailoringMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Unit Master");
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

		public override SelectAllTailoringMasterByStoreResponse SelectTailorMasterLookUp(SelectAllTailoringMasterByStoreRequest ObjRequest)
		{
			var TailoringMasterList = new List<TailoringMasterTypes>();
			var RequestData = (SelectAllTailoringMasterByStoreRequest)ObjRequest;
			var ResponseData = new SelectAllTailoringMasterByStoreResponse();
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				//string sSql = "Select * from TailoringUnitMaster with(NoLock) where Active='{0}'";
				string sSql = "SELECT * FROM TailoringUnitMaster WHERE StoreID = " + RequestData.StoreID;
				sSql = sSql + " AND Active = 1";


				_CommandObj = new SqlCommand(sSql, _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var objTailoringMaster = new TailoringMasterTypes();
						objTailoringMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objTailoringMaster.tailoringunitcode = Convert.ToString(objReader["tailoringunitcode"]);
						objTailoringMaster.tailoringunitName = Convert.ToString(objReader["tailoringunitName"]);
						objTailoringMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
						objTailoringMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
						objTailoringMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
						objTailoringMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
						objTailoringMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
						objTailoringMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

						TailoringMasterList.Add(objTailoringMaster);
					}
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.TailoringMasterList = TailoringMasterList;

					ResponseData.ResponseDynamicData = TailoringMasterList;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Unit Master");
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

        public override SelectAllTailoringResponse API_SelectALL(SelectAllTailoringRequest objRequest)
        {
            var TailoringMasterList = new List<TailoringMasterTypes>();
            var RequestData = (SelectAllTailoringRequest)objRequest;
            var ResponseData = new SelectAllTailoringResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from TailoringUnitMaster with(NoLock) where Active='{0}'";
                //string sSql = "Select * from TailoringUnitMaster  ";
                
                string sSql = "Select ID, Tailoringunitcode, TailoringunitName, CountryCode, StoreCode, Active, RecordCount = COUNT(*) OVER()  from TailoringUnitMaster " +
                    "where Active = " + RequestData.IsActive + " " +
                    "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or Tailoringunitcode = isnull('" + RequestData.SearchString + "','') " +
                          "or TailoringunitName = isnull('" + RequestData.SearchString + "','') " +
                          "or CountryCode = isnull('" + RequestData.SearchString + "','') " +
                          "or StoreCode = isnull('" + RequestData.SearchString + "','')) " +                  
                  "order by ID asc " +
                  "offset " + RequestData.Offset + " rows " +
                  "fetch first " + RequestData.Limit + " rows only";


                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTailoringMaster = new TailoringMasterTypes();
                        objTailoringMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTailoringMaster.tailoringunitcode = Convert.ToString(objReader["tailoringunitcode"]);
                        objTailoringMaster.tailoringunitName = Convert.ToString(objReader["tailoringunitName"]);
                        //objTailoringMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objTailoringMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        //objTailoringMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objTailoringMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objTailoringMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objTailoringMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        TailoringMasterList.Add(objTailoringMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TailoringMasterList = TailoringMasterList;

                    //ResponseData.ResponseDynamicData = TailoringMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Unit Master");
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
    }
}
