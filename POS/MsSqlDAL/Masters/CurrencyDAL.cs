using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CurrencyResponse;
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
 public  class CurrencyDAL:BaseCurrencyDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlTransaction transaction = null;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCurrencyRequest)RequestObj;
            var ResponseData = new SaveCurrencyResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
             

                _CommandObj = new SqlCommand("InsertCurrencyMaster", _ConnectionObj, transaction);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter CurrencyID = _CommandObj.Parameters.Add("@CurrencyID", SqlDbType.Int);
                CurrencyID.Direction = ParameterDirection.Input;
                CurrencyID.Value = RequestData.CurrencyMasterData.ID;

                SqlParameter CurrencyCode = _CommandObj.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar);
                CurrencyCode.Direction = ParameterDirection.Input;
                CurrencyCode.Value = RequestData.CurrencyMasterData.CurrencyCode;

                SqlParameter CurrencyName = _CommandObj.Parameters.Add("@CurrencyName", SqlDbType.NVarChar);
                CurrencyName.Direction = ParameterDirection.Input;
                CurrencyName.Value = RequestData.CurrencyMasterData.CurrencyName;

                SqlParameter CurrencySymbol = _CommandObj.Parameters.Add("@CurrencySymbol", SqlDbType.NVarChar);
                CurrencySymbol.Direction = ParameterDirection.Input;
                CurrencySymbol.Value = RequestData.CurrencyMasterData.CurrencySymbol;

                SqlParameter InternationalCode = _CommandObj.Parameters.Add("@InternationalCode", SqlDbType.NVarChar);
                InternationalCode.Direction = ParameterDirection.Input;
                InternationalCode.Value = RequestData.CurrencyMasterData.InternationalCode;

                SqlParameter DecimalPlaces = _CommandObj.Parameters.Add("@DecimalPlaces", SqlDbType.NVarChar);
                DecimalPlaces.Direction = ParameterDirection.Input;
                DecimalPlaces.Value = RequestData.CurrencyMasterData.DecimalPlaces;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CurrencyMasterData.Active;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CurrencyMasterData.CreateBy;

                SqlParameter CurrencyType = _CommandObj.Parameters.Add("@CurrencyType", SqlDbType.VarChar);
                CurrencyType.Direction = ParameterDirection.Input;
                CurrencyType.Value = RequestData.CurrencyMasterData.CurrencyType;

                SqlParameter MRoundValue = _CommandObj.Parameters.Add("@MRoundValue", SqlDbType.Money);
                MRoundValue.Direction = ParameterDirection.Input;
                MRoundValue.Value = RequestData.CurrencyMasterData.MRoundValue;

                SqlParameter InterDescription = _CommandObj.Parameters.Add("@InterDescription", SqlDbType.NVarChar);
                InterDescription.Direction = ParameterDirection.Input;
                InterDescription.Value = RequestData.CurrencyMasterData.InterDescription;

                SqlParameter HundredthName = _CommandObj.Parameters.Add("@HundredthName", SqlDbType.NVarChar);
                HundredthName.Direction = ParameterDirection.Input;
                HundredthName.Value = RequestData.CurrencyMasterData.HundredthName;

                SqlParameter English = _CommandObj.Parameters.Add("@English", SqlDbType.NVarChar);
                English.Direction = ParameterDirection.Input;
                English.Value = RequestData.CurrencyMasterData.English;

                SqlParameter EngHundredthName = _CommandObj.Parameters.Add("@EngHundredthName", SqlDbType.NVarChar);
                EngHundredthName.Direction = ParameterDirection.Input;
                EngHundredthName.Value = RequestData.CurrencyMasterData.EngHundredthName;

				SqlParameter CurrencyDetails = _CommandObj.Parameters.Add("@CurrencyDetails", SqlDbType.Xml);
				CurrencyDetails.Direction = ParameterDirection.Input;
				CurrencyDetails.Value = CurrencyDetailsXML(RequestData.CurrencyMasterData.CurrencyDetailsList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Currency");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Currency");
                   
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency");
                 
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Currency");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
                transaction.Rollback();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateCurrencyRequest)RequestObj;

            var ResponseData = new UpdateCurrencyResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateCurrencyMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.CurrencyMasterData.ID;

                SqlParameter CurrencyCode = _CommandObj.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar);
                CurrencyCode.Direction = ParameterDirection.Input;
                CurrencyCode.Value = RequestData.CurrencyMasterData.CurrencyCode;

                SqlParameter CurrencyName = _CommandObj.Parameters.Add("@CurrencyName", SqlDbType.NVarChar);
                CurrencyName.Direction = ParameterDirection.Input;
                CurrencyName.Value = RequestData.CurrencyMasterData.CurrencyName;

                SqlParameter CurrencySymbol = _CommandObj.Parameters.Add("@CurrencySymbol", SqlDbType.NVarChar);
                CurrencySymbol.Direction = ParameterDirection.Input;
                CurrencySymbol.Value = RequestData.CurrencyMasterData.CurrencySymbol;

                SqlParameter InternationalCode = _CommandObj.Parameters.Add("@InternationalCode", SqlDbType.NVarChar);
                InternationalCode.Direction = ParameterDirection.Input;
                InternationalCode.Value = RequestData.CurrencyMasterData.InternationalCode;

                SqlParameter DecimalPlaces = _CommandObj.Parameters.Add("@DecimalPlaces", SqlDbType.NVarChar);
                DecimalPlaces.Direction = ParameterDirection.Input;
                DecimalPlaces.Value = RequestData.CurrencyMasterData.DecimalPlaces;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CurrencyMasterData.Active;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.CurrencyMasterData.UpdateBy;

                SqlParameter UpdateOn = _CommandObj.Parameters.Add("@UpdateOn", SqlDbType.DateTime);
                UpdateOn.Direction = ParameterDirection.Input;
                UpdateOn.Value = RequestData.CurrencyMasterData.UpdateOn;

                SqlParameter CurrencyType = _CommandObj.Parameters.Add("@CurrencyType", SqlDbType.VarChar);
                CurrencyType.Direction = ParameterDirection.Input;
                CurrencyType.Value = RequestData.CurrencyMasterData.CurrencyType;

                SqlParameter MRoundValue = _CommandObj.Parameters.Add("@MRoundValue", SqlDbType.Money);
                MRoundValue.Direction = ParameterDirection.Input;
                MRoundValue.Value = RequestData.CurrencyMasterData.MRoundValue;

                SqlParameter InterDescription = _CommandObj.Parameters.Add("@InterDescription", SqlDbType.NVarChar);
                InterDescription.Direction = ParameterDirection.Input;
                InterDescription.Value = RequestData.CurrencyMasterData.InterDescription;

                SqlParameter HundredthName = _CommandObj.Parameters.Add("@HundredthName", SqlDbType.NVarChar);
                HundredthName.Direction = ParameterDirection.Input;
                HundredthName.Value = RequestData.CurrencyMasterData.HundredthName;

                SqlParameter English = _CommandObj.Parameters.Add("@English", SqlDbType.NVarChar);
                English.Direction = ParameterDirection.Input;
                English.Value = RequestData.CurrencyMasterData.English;

                SqlParameter EngHundredthName = _CommandObj.Parameters.Add("@EngHundredthName", SqlDbType.NVarChar);
                EngHundredthName.Direction = ParameterDirection.Input;
                EngHundredthName.Value = RequestData.CurrencyMasterData.EngHundredthName;

				SqlParameter CurrencyDetails = _CommandObj.Parameters.Add("@CurrencyDetails", SqlDbType.Xml);
				CurrencyDetails.Direction = ParameterDirection.Input;
				CurrencyDetails.Value = CurrencyDetailsXML(RequestData.CurrencyMasterData.CurrencyDetailsList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Currency");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Currency");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Currency");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Currency");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

		public string CurrencyDetailsXML(List<CurrencyDetails> CurrencyDetailsList)
		{
			StringBuilder sSql = new StringBuilder();
			var sqlCommon = new MsSqlCommon();
			foreach (CurrencyDetails objCurrencyDetails in CurrencyDetailsList)
			{
				sSql.Append("<CurrencyDetailsData>");
				sSql.Append("<ID>" + objCurrencyDetails.ID + "</ID>");
				sSql.Append("<CurrencyID>" + objCurrencyDetails.CurrencyID + "</CurrencyID>");
				sSql.Append("<CurrencyCode>" + objCurrencyDetails.CurrencyCode + "</CurrencyCode>");
				sSql.Append("<CurrencyValue>" + objCurrencyDetails.CurrencyValue + "</CurrencyValue>");
				sSql.Append("<Remarks>" + objCurrencyDetails.Remarks + "</Remarks>");
				sSql.Append("</CurrencyDetailsData>");
			}
			//return sSql.ToString().Replace("&", "&#38;");
			return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
			//return sSql.ToString();
		} 
        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var CurrencyMasterRecord = new CurrencyMaster();
            var RequestData = (DeleteCurrencyRequest)RequestObj;
            var ResponseData = new DeleteCurrencyResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("Delete from CurrencyMaster where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Currency");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Currency");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var CurrencyMasterRecord = new CurrencyMaster();
            var RequestData = (SelectByIDCurrencyRequest)RequestObj;
            var ResponseData = new SelectByIDCurrencyResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from CurrencyMaster with(NoLock) where ID='" + RequestData.ID + "'", _ConnectionObj);
               //_CommandObj = new SqlCommand("Select * from CurrencyMaster with(NoLock) where IsBaseCurrency='true'", _ConnectionObj);
               // _CommandObj = new SqlCommand("Select * from CurrencyMaster with(NoLock) where IsBaseCurrency='true'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCurrencyMaster = new CurrencyMaster();

                        objCurrencyMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCurrencyMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCurrencyMaster.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        objCurrencyMaster.InterDescription = Convert.ToString(objReader["InterDescription"]);
                        objCurrencyMaster.HundredthName = Convert.ToString(objReader["HundredthName"]);
                        objCurrencyMaster.English = Convert.ToString(objReader["English"]);
                        objCurrencyMaster.EngHundredthName = Convert.ToString(objReader["EngHundredthName"]);
                        objCurrencyMaster.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
                        objCurrencyMaster.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
                        objCurrencyMaster.DecimalPlaces =objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) :0;
                        //objCurrencyMaster.MRoundValue = objReader["MRoundValue"] != DBNull.Value ? Convert.ToInt32(objReader["MRoundValue"]) : 0;
                        objCurrencyMaster.MRoundValue = objReader["MRoundValue"] != DBNull.Value ? Convert.ToDecimal(objReader["MRoundValue"].ToString()) : 0;
                        objCurrencyMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCurrencyMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCurrencyMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCurrencyMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCurrencyMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCurrencyMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCurrencyMaster.CurrencyType = Convert.ToString(objReader["CurrencyType"]);
                        objCurrencyMaster.IsBaseCurrency = objReader["IsBaseCurrency"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseCurrency"]) : false;

						objCurrencyMaster.CurrencyDetailsList = new List<CurrencyDetails>();
						var objSelectCurrencyDetailsRequest = new SelectCurrencyDetailsRequest();
						var objSelectCurreucyDetailsResponse = new SelectCurreucyDetailsResponse();						
						objSelectCurrencyDetailsRequest.ID = objCurrencyMaster.ID;
						objSelectCurrencyDetailsRequest.CurrencyCode = objCurrencyMaster.CurrencyCode;
						objSelectCurrencyDetailsRequest.ConnectionString = RequestData.ConnectionString;
						objSelectCurreucyDetailsResponse = SelectCurrencyDetails(objSelectCurrencyDetailsRequest);
						if (objSelectCurreucyDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
						{
							objCurrencyMaster.CurrencyDetailsList = objSelectCurreucyDetailsResponse.CurrencyDetailsList;
						}

                        ResponseData.CurrencyMasterRecord = objCurrencyMaster;
                        ResponseData.ResponseDynamicData = objCurrencyMaster;
						ResponseData.ResponseDynamicData.CurrencyDetailsList = objCurrencyMaster.CurrencyDetailsList;
                    }
					//List<CurrencyDetails> Currency_DetailsList = GetDetails(ResponseData.CurrencyMasterRecord.ID, RequestObj);
					//ResponseData.CurrencyMasterRecord.CurrencyDetailsList = Currency_DetailsList;
					//ResponseData.ResponseDynamicData.CurrencyDetailsList = Currency_DetailsList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Currency");
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


		public List<CurrencyDetails> GetDetails(int CurrencyID, BaseRequestType RequestObj)
		{
			var CurrencyMasterRecord = new CurrencyMaster();
			var RequestData = RequestObj;
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			var CurrencyDetailsList = new List<CurrencyDetails>();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				_CommandObj = new SqlCommand("Select * from CurrencyDetails with(NoLock) where CurrencyID = " + CurrencyID.ToString(), _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var objCurrencyDetails = new CurrencyDetails();
						objCurrencyDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objCurrencyDetails.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
						objCurrencyDetails.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
						objCurrencyDetails.CurrencyValue = objReader["CurrencyValue"] != DBNull.Value ? Convert.ToDecimal(objReader["CurrencyValue"]) : 0;
						objCurrencyDetails.Remarks = Convert.ToString(objReader["Remarks"]);
						CurrencyDetailsList.Add(objCurrencyDetails);
					}
				}
			}
			catch (Exception ex)
			{				
			}
			finally
			{
				sqlCommon.CloseConnection(_ConnectionObj);
			}
			return CurrencyDetailsList;
		}

		public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
		{
			var CurrencyMasterList = new List<CurrencyMaster>();
			var RequestData = (SelectAllCurrencyRequest)RequestObj;
			var ResponseData = new SelectAllCurrencyResponse();
			SqlDataReader objReader;
			DataTable DT = new DataTable();
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				string sSql = ("Select * from CurrencyMaster with(NoLock) ORDER BY ID DESC");
				_CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();

				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var objCurrencyMaster = new CurrencyMaster();

						objCurrencyMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objCurrencyMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
						objCurrencyMaster.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
						objCurrencyMaster.InterDescription = Convert.ToString(objReader["InterDescription"]);
						objCurrencyMaster.HundredthName = Convert.ToString(objReader["HundredthName"]);
						objCurrencyMaster.English = Convert.ToString(objReader["English"]);
						objCurrencyMaster.EngHundredthName = Convert.ToString(objReader["EngHundredthName"]);
						objCurrencyMaster.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
						objCurrencyMaster.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
						objCurrencyMaster.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
						objCurrencyMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
						objCurrencyMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
						objCurrencyMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
						objCurrencyMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
						objCurrencyMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
						objCurrencyMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
						objCurrencyMaster.CurrencyType = Convert.ToString(objReader["CurrencyType"]);
						objCurrencyMaster.IsBaseCurrency = objReader["IsBaseCurrency"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseCurrency"]) : false;
						//objCurrencyMaster.ViewCurrencyDetailsList = GetDetails(objCurrencyMaster.ID, RequestObj);

						objCurrencyMaster.ViewCurrencyDetailsList = new List<CurrencyDetails>();
						var objSelectCurrencyDetailsRequest = new SelectCurrencyDetailsRequest();
						var objSelectCurreucyDetailsResponse = new SelectCurreucyDetailsResponse();
						objSelectCurrencyDetailsRequest.ID = objCurrencyMaster.ID;
						objSelectCurrencyDetailsRequest.CurrencyCode = objCurrencyMaster.CurrencyCode;
						objSelectCurrencyDetailsRequest.ConnectionString = RequestData.ConnectionString;
						objSelectCurreucyDetailsResponse = SelectCurrencyDetails(objSelectCurrencyDetailsRequest);
						if (objSelectCurreucyDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
						{
							objCurrencyMaster.ViewCurrencyDetailsList = objSelectCurreucyDetailsResponse.CurrencyDetailsList;
						}


						CurrencyMasterList.Add(objCurrencyMaster);
					}
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.CurrencyMasterList = CurrencyMasterList;
					ResponseData.ResponseDynamicData = CurrencyMasterList;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Role Master");
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


		//public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
		//{
		//	var CurrencyMasterList = new List<CurrencyMaster>();
		//	var RequestData = (SelectAllCurrencyRequest)RequestObj;
		//	var ResponseData = new SelectAllCurrencyResponse();
		//	SqlDataAdapter objAdapter;
		//	DataTable DT = new DataTable();
		//	var sqlCommon = new MsSqlCommon();
		//	try
		//	{
		//		_ConnectionString = RequestData.ConnectionString;
		//		_RequestFrom = RequestData.RequestFrom;

		//		sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
		//		string sSql = ("Select * from CurrencyMaster with(NoLock)");
		//		_CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
		//		_CommandObj.CommandType = CommandType.Text;
		//		//objReader = _CommandObj.ExecuteReader();
		//		objAdapter = new SqlDataAdapter(_CommandObj);
		//		objAdapter.Fill(DT);
		//		//if (objReader.HasRows)
		//		if (DT != null)
		//		{
		//			//while (objReader.Read())
		//			foreach(DataRow objReader in DT.Rows)
		//			{
		//				var objCurrencyMaster = new CurrencyMaster();

		//				objCurrencyMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
		//				objCurrencyMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
		//				objCurrencyMaster.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
		//				objCurrencyMaster.InterDescription = Convert.ToString(objReader["InterDescription"]);
		//				objCurrencyMaster.HundredthName = Convert.ToString(objReader["HundredthName"]);
		//				objCurrencyMaster.English = Convert.ToString(objReader["English"]);
		//				objCurrencyMaster.EngHundredthName = Convert.ToString(objReader["EngHundredthName"]);
		//				objCurrencyMaster.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
		//				objCurrencyMaster.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
		//				objCurrencyMaster.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
		//				objCurrencyMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
		//				objCurrencyMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
		//				objCurrencyMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
		//				objCurrencyMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
		//				objCurrencyMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
		//				objCurrencyMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
		//				objCurrencyMaster.CurrencyType = Convert.ToString(objReader["CurrencyType"]);
		//				objCurrencyMaster.IsBaseCurrency = objReader["IsBaseCurrency"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseCurrency"]) : false;
		//				objCurrencyMaster.ViewCurrencyDetailsList = GetDetails(objCurrencyMaster.ID, RequestObj);
		//				CurrencyMasterList.Add(objCurrencyMaster);
		//			}
		//			ResponseData.StatusCode = Enums.OpStatusCode.Success;
		//			ResponseData.CurrencyMasterList = CurrencyMasterList;
		//			ResponseData.ResponseDynamicData = CurrencyMasterList;
		//		}
		//		else
		//		{
		//			ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
		//			ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Role Master");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
		//		ResponseData.DisplayMessage = ex.Message;
		//	}
		//	finally
		//	{
		//		sqlCommon.CloseConnection(_ConnectionObj);
		//	}
		//	return ResponseData;
		//}

        public override BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override SelectCurrencyLookUpResponse SelectCurrencyLookUp(SelectCurrencyLookUpRequest ObjRequest)
        {
            var CurrencyList = new List<CurrencyMaster>();

            var RequestData = (SelectCurrencyLookUpRequest)ObjRequest;
            var ResponseData = new SelectCurrencyLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[CurrencyName],CurrencyCode,InternationalCode,IsBaseCurrency,Active from CurrencyMaster with(NoLock) where Active='true'";// and CurrencyType like '%" + RequestData.CurrencyType + "%'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCurrency = new CurrencyMaster();
                        objCurrency.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCurrency.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        objCurrency.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCurrency.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
                        objCurrency.IsBaseCurrency = objReader["IsBaseCurrency"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseCurrency"]) : false;
                        objCurrency.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        CurrencyList.Add(objCurrency);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CurrencyMasterList = CurrencyList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Currency");
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

		public override SelectCurreucyDetailsResponse SelectCurrencyDetails(SelectCurrencyDetailsRequest ObjRequest)
		{
			var CurrencyMasterRecord = new CurrencyMaster();
			var RequestData = (SelectCurrencyDetailsRequest)ObjRequest;
			var ResponseData = new SelectCurreucyDetailsResponse();
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			var CurrencyDetailsList = new List<CurrencyDetails>();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

				string SQry = "";
				if(!string.IsNullOrEmpty(RequestData.CurrencyCode))
				{
					SQry = "Select * from CurrencyDetails with(NoLock) where CurrencyCode ='" + RequestData.CurrencyCode + "'";
				}
				else
				{
					SQry = "Select * from CurrencyDetails with(NoLock) where CurrencyID='" + RequestData.ID + "'";
				}

				_CommandObj = new SqlCommand(SQry, _ConnectionObj);

				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var objCurrencyDetails = new CurrencyDetails();
						objCurrencyDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objCurrencyDetails.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
						objCurrencyDetails.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
						objCurrencyDetails.CurrencyValue = objReader["CurrencyValue"] != DBNull.Value ? Convert.ToDecimal(objReader["CurrencyValue"]) : 0;
                        objCurrencyDetails.PaymentValue =  0;
                        objCurrencyDetails.TotalValue = 0;
						objCurrencyDetails.Remarks = Convert.ToString(objReader["Remarks"]);
						CurrencyDetailsList.Add(objCurrencyDetails);
					}

					ResponseData.CurrencyDetailsList = CurrencyDetailsList;
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Currency");
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

        public override SelectAllCurrencyResponse API_SelectALL(SelectAllCurrencyRequest requestData)
        {
            var CurrencyMasterList = new List<CurrencyMaster>();
            var RequestData = (SelectAllCurrencyRequest)requestData;
            var ResponseData = new SelectAllCurrencyResponse();
            SqlDataReader objReader;
            DataTable DT = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = ("Select * from CurrencyMaster with(NoLock) ORDER BY ID DESC");

                var sSql = new StringBuilder();
                int myInt;
                bool isNumerical = int.TryParse(RequestData.SearchString, out myInt);

                if (isNumerical)
                {
                    sSql.Append("Select ID, CurrencyCode, CurrencyName, CurrencySymbol, InterDescription, DecimalPlaces, CurrencyType, Active, RC.TOTAL_CNT [RecordCount] ");
                    sSql.Append("from CurrencyMaster with(NoLock) ");
                    sSql.Append("LEFT JOIN(Select  count(CM.ID) As TOTAL_CNT From CurrencyMaster CM with(NoLock) ");

                    sSql.Append("where CM.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or CM.CurrencyCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencyName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencySymbol like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.InterDescription like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.DecimalPlaces like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencyType like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");


                    sSql.Append("where Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or CurrencyCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CurrencyName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CurrencySymbol like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or InterDescription like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or DecimalPlaces like isnull('%" + int.Parse(RequestData.SearchString) + "%','') ");
                    sSql.Append("or CurrencyType like isnull('%" + RequestData.SearchString + "%','')) ");
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }
                else
                {
                    sSql.Append("Select ID, CurrencyCode, CurrencyName, CurrencySymbol, InterDescription, DecimalPlaces, CurrencyType, Active, RC.TOTAL_CNT [RecordCount] ");
                    sSql.Append("from CurrencyMaster with(NoLock) ");
                    sSql.Append("LEFT JOIN(Select  count(CM.ID) As TOTAL_CNT From CurrencyMaster CM with(NoLock) ");

                    sSql.Append("where CM.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or CM.CurrencyCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencyName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencySymbol like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.InterDescription like isnull('%" + RequestData.SearchString + "%','') ");
                    //sSql.Append("or CM.DecimalPlaces like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencyType like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                    sSql.Append("where Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or CurrencyCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CurrencyName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CurrencySymbol like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or InterDescription like isnull('%" + RequestData.SearchString + "%','') ");
                    //sSql.Append("or DecimalPlaces like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CurrencyType like isnull('%" + RequestData.SearchString + "%','')) ");
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }                

                //string sSql = "Select ID, CurrencyCode, CurrencyName, CurrencySymbol, InterDescription, DecimalPlaces, CurrencyType, Active, RecordCount = COUNT(*) OVER() " +
                //   "from CurrencyMaster with(NoLock) " +
                //   "where Active = " + RequestData.IsActive + " " +
                //       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //       "or CurrencyCode = isnull('" + RequestData.SearchString + "','') " +
                //       "or CurrencyName = isnull('" + RequestData.SearchString + "','') " +
                //       "or CurrencySymbol = isnull('" + RequestData.SearchString + "','') " +
                //       "or InterDescription = isnull('" + RequestData.SearchString + "','') " +
                //       "or DecimalPlaces = isnull('" + RequestData.SearchString + "','') " +
                //       "or CurrencyType = isnull('" + RequestData.SearchString + "','')) " +
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

                        var objCurrencyMaster = new CurrencyMaster();

                        objCurrencyMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCurrencyMaster.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCurrencyMaster.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        objCurrencyMaster.InterDescription = Convert.ToString(objReader["InterDescription"]);
                        //objCurrencyMaster.HundredthName = Convert.ToString(objReader["HundredthName"]);
                        //objCurrencyMaster.English = Convert.ToString(objReader["English"]);
                        //objCurrencyMaster.EngHundredthName = Convert.ToString(objReader["EngHundredthName"]);
                        objCurrencyMaster.CurrencySymbol = Convert.ToString(objReader["CurrencySymbol"]);
                        //objCurrencyMaster.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
                        objCurrencyMaster.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        //objCurrencyMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCurrencyMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCurrencyMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCurrencyMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objCurrencyMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCurrencyMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCurrencyMaster.CurrencyType = Convert.ToString(objReader["CurrencyType"]);
                        //objCurrencyMaster.IsBaseCurrency = objReader["IsBaseCurrency"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseCurrency"]) : false;
                        //objCurrencyMaster.ViewCurrencyDetailsList = GetDetails(objCurrencyMaster.ID, RequestObj);

                        objCurrencyMaster.ViewCurrencyDetailsList = new List<CurrencyDetails>();
                        var objSelectCurrencyDetailsRequest = new SelectCurrencyDetailsRequest();
                        var objSelectCurreucyDetailsResponse = new SelectCurreucyDetailsResponse();
                        objSelectCurrencyDetailsRequest.ID = objCurrencyMaster.ID;
                        objSelectCurrencyDetailsRequest.CurrencyCode = objCurrencyMaster.CurrencyCode;
                        objSelectCurrencyDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectCurreucyDetailsResponse = SelectCurrencyDetails(objSelectCurrencyDetailsRequest);
                        if (objSelectCurreucyDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCurrencyMaster.ViewCurrencyDetailsList = objSelectCurreucyDetailsResponse.CurrencyDetailsList;
                        }


                        CurrencyMasterList.Add(objCurrencyMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CurrencyMasterList = CurrencyMasterList;
                    ResponseData.ResponseDynamicData = CurrencyMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Currency");
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

        public override SelectCurrencyLookUpResponse API_SelectCurrencyLookUp(SelectCurrencyLookUpRequest objRequest)
        {
            var CurrencyList = new List<CurrencyMaster>();

            var RequestData = (SelectCurrencyLookUpRequest)objRequest;
            var ResponseData = new SelectCurrencyLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,[CurrencyName],CurrencyCode,InternationalCode from CurrencyMaster with(NoLock) where Active='true' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCurrency = new CurrencyMaster();
                        objCurrency.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCurrency.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        objCurrency.CurrencyCode = Convert.ToString(objReader["CurrencyCode"]);
                        objCurrency.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
                        // objCurrency.IsBaseCurrency = objReader["IsBaseCurrency"] != DBNull.Value ? Convert.ToBoolean(objReader["IsBaseCurrency"]) : false;
                        CurrencyList.Add(objCurrency);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CurrencyMasterList = CurrencyList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Currency");
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
