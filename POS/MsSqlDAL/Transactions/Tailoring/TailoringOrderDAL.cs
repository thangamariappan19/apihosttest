using EasyBizAbsDAL.Transactions.Tailoring;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Tailoring;
using EasyBizRequest.Transactions.Tailoring;
using EasyBizResponse.Transactions.Tailoring;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Tailoring
{
	public class TailoringOrderDAL: BaseTailoringOrderDAL
	{
		SqlConnection _ConnectionObj;
		SqlCommand _CommandObj;
		string _ConnectionString; Enums.RequestFrom _RequestFrom;

		#region "Tailoring Order"
		public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
		{
			var RequestData = (SaveTailoringOrderRequest)RequestObj;
			var ResponseData = new SaveTailoringOrderResponse();
			StringBuilder sSql = new StringBuilder();
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				_CommandObj = new SqlCommand("InsertOrUpdateTailoringOrder", _ConnectionObj);
				_CommandObj.CommandType = CommandType.StoredProcedure;

				SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
				ID.Direction = ParameterDirection.Input;
				ID.Value = RequestData.TailoringOrderHeaderRecord.ID;

				SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
				DocumentNo.Direction = ParameterDirection.Input;
				DocumentNo.Value = RequestData.TailoringOrderHeaderRecord.DocumentNo;

				SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
				DocumentDate.Direction = ParameterDirection.Input;
				DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.TailoringOrderHeaderRecord.DocumentDate);

				SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);
				StoreCode.Direction = ParameterDirection.Input;
				StoreCode.Value = RequestData.TailoringOrderHeaderRecord.StoreCode;

				SqlParameter CustomerCode = _CommandObj.Parameters.Add("@CustomerCode", SqlDbType.NVarChar);
				CustomerCode.Direction = ParameterDirection.Input;
				CustomerCode.Value = RequestData.TailoringOrderHeaderRecord.CustomerCode;

				SqlParameter ExpectedDeliveryDate = _CommandObj.Parameters.Add("@ExpectedDeliveryDate", SqlDbType.DateTime);
				ExpectedDeliveryDate.Direction = ParameterDirection.Input;
				ExpectedDeliveryDate.Value = sqlCommon.GetSQLServerDateString(RequestData.TailoringOrderHeaderRecord.ExpectedDeliveryDate);

				SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
				CreateBy.Direction = ParameterDirection.Input;
				CreateBy.Value = RequestData.TailoringOrderHeaderRecord.CreateBy;

				SqlParameter TailoringOrderDetails = _CommandObj.Parameters.Add("@TailoringOrderDetails", SqlDbType.Xml);
				TailoringOrderDetails.Direction = ParameterDirection.Input;
				TailoringOrderDetails.Value = TailoringOrderDetailsXML(RequestData.TailoringOrderDetailsList);
				//StockRequestDetails.Value = StockRequestDetails.ToString().Replace("&", "&#38;");

				SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 10);
				ID2.Direction = ParameterDirection.Output;

				SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
				StatusCode.Direction = ParameterDirection.Output;

				SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
				StatusMsg.Direction = ParameterDirection.Output;

				_CommandObj.CommandType = CommandType.StoredProcedure;
				_CommandObj.ExecuteNonQuery();

				string strStatusCode = StatusCode.Value.ToString();
				if (strStatusCode == "1")
				{
					ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Tailoring Order");
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.IDs = ID2.Value.ToString();
				}
				else if (strStatusCode == "2")
				{
					ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Tailoring Order");
				}
				else
				{
					ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Order");
				}
			}
			catch (Exception ex)
			{
				ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
				ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Order");
				ResponseData.ExceptionMessage = ex.Message;
				ResponseData.StackTrace = ex.StackTrace;
			}
			finally
			{
				sqlCommon.CloseConnection(_ConnectionObj);
			}

			return ResponseData;
		}
		public string TailoringOrderDetailsXML(List<TailoringOrderDetails> TailoringOrderDetailsList)
		{
			StringBuilder sSql = new StringBuilder();
			var sqlCommon = new MsSqlCommon();
			foreach (TailoringOrderDetails objTailoringOrderDetails in TailoringOrderDetailsList)
			{
				sSql.Append("<TailoringOrderDetailsData>");
				sSql.Append("<ID>" + objTailoringOrderDetails.ID + "</ID>");
				sSql.Append("<TailoringOrderID>" + objTailoringOrderDetails.TailoringOrderID + "</TailoringOrderID>");
				sSql.Append("<SKUCode>" + objTailoringOrderDetails.SKUCode + "</SKUCode>");
				sSql.Append("<OrderQuantity>" + (objTailoringOrderDetails.OrderQuantity) + "</OrderQuantity>");
				sSql.Append("<TailoringRemarks>" + objTailoringOrderDetails.TailoringRemarks + "</TailoringRemarks>");
				sSql.Append("<AtTailor>" + objTailoringOrderDetails.AtTailor + "</AtTailor>");
				sSql.Append("<ReceivedFromTailor>" + objTailoringOrderDetails.ReceivedFromTailor + "</ReceivedFromTailor>");
				sSql.Append("<DeliveredQuantity>" + (objTailoringOrderDetails.DeliveredQuantity) + "</DeliveredQuantity>");
				sSql.Append("<CustomerDeliveryDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrderDetails.CustomerDeliveryDate) + "</CustomerDeliveryDate>");
				sSql.Append("<TailorDeliveryDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrderDetails.TailorDeliveryDate) + "</TailorDeliveryDate>");

				sSql.Append("<OrderStatus>" + objTailoringOrderDetails.OrderStatus + "</OrderStatus>");
				sSql.Append("<CreateBy>" + objTailoringOrderDetails.CreateBy + "</CreateBy>");
				sSql.Append("<UpdateBy>" + objTailoringOrderDetails.UpdateBy + "</UpdateBy>");
				sSql.Append("</TailoringOrderDetailsData>");

			}
			//return sSql.ToString().Replace("&", "&#38;");
			return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
			//return sSql.ToString();
		}   
		public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
		{
			var TailoringOrderRecord = new TailoringOrder();
			var RequestData = (DeleteTailoringOrderRequest)RequestObj;
			var ResponseData = new DeleteTailoringOrderResponse();
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				string sSql = "Delete from TailoringOrderDetails where TailoringOrderID={0} ; Delete from TailoringOrder where ID={0}";
				sSql = string.Format(sSql, RequestData.ID);

				_CommandObj = new SqlCommand(sSql, _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				_CommandObj.ExecuteNonQuery();
				ResponseData.StatusCode = Enums.OpStatusCode.Success;
				ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Tailoring Order");
			}
			catch
			{
				ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
				ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Tailoring Order");
			}
			finally
			{
				sqlCommon.CloseConnection(_ConnectionObj);
			}
			return ResponseData;
		}
		public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
		{
			var TailoringOrderRecord = new TailoringOrder();
			var RequestData = (SelectByIDTailoringOrderRequest)RequestObj;
			var ResponseData = new SelectByIDTailoringOrderResponse();
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				string sSql = "Select * from TailoringOrder with(NoLock) where ID='{0}' ";
				sSql = string.Format(sSql, RequestData.ID);

				_CommandObj = new SqlCommand(sSql, _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read()) 
					{
						var objTailoringOrder = new TailoringOrder();
						objTailoringOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;										
						objTailoringOrder.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
						objTailoringOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
						objTailoringOrder.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
						objTailoringOrder.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : "";
						objTailoringOrder.ExpectedDeliveryDate = objReader["ExpectedDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ExpectedDeliveryDate"]) : DateTime.Now;
						objTailoringOrder.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
						objTailoringOrder.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
						objTailoringOrder.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
						objTailoringOrder.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

						objTailoringOrder.TailoringOrderDetailsList = new List<TailoringOrderDetails>();
						SelectTailoringOrderDetailsRequest objSelectTailoringOrderDetailsRequest = new SelectTailoringOrderDetailsRequest();
						SelectTailoringOrderDetailsResponse objSelectTailoringOrderDetailsResponse = new SelectTailoringOrderDetailsResponse();
						objSelectTailoringOrderDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objSelectTailoringOrderDetailsResponse = SelectTailoringOrderDetails(objSelectTailoringOrderDetailsRequest);
						if (objSelectTailoringOrderDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
						{
							objTailoringOrder.TailoringOrderDetailsList = objSelectTailoringOrderDetailsResponse.TailoringOrderDetailsList;
						}

						ResponseData.TailoringOrderRecord = objTailoringOrder;
						ResponseData.ResponseDynamicData = objTailoringOrder;
					}

					ResponseData.StatusCode = Enums.OpStatusCode.Success;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Order");
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
		public override SelectTailoringOrderDetailsResponse SelectTailoringOrderDetails(SelectTailoringOrderDetailsRequest ObjRequest)
		{
			var TailoringOrderDetailsList = new List<TailoringOrderDetails>();
			var RequestData = (SelectTailoringOrderDetailsRequest)ObjRequest;
			var ResponseData = new SelectTailoringOrderDetailsResponse();
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				var sSql = new StringBuilder();
                if (RequestData.CheckLoggedIn == "Deliver")
                {
                    sSql.Append("select * from TailoringOrderDetails ");
                    sSql.Append("where  TailoringOrderID = " + RequestData.ID + " and OrderStatus <> 'CLOSE' ");
                    sSql.Append("order by id  asc");
                }
                else
                {
                    sSql.Append("select * from TailoringOrderDetails ");
                    sSql.Append("where  TailoringOrderID = " + RequestData.ID + "");
                    sSql.Append("order by id  asc");
                }

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				_CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var obj_TailoringOrderDetails = new TailoringOrderDetails();
						obj_TailoringOrderDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						obj_TailoringOrderDetails.TailoringOrderID = objReader["TailoringOrderID"] != DBNull.Value ? Convert.ToInt32(objReader["TailoringOrderID"]) : 0;
						obj_TailoringOrderDetails.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
						obj_TailoringOrderDetails.OrderQuantity = objReader["OrderQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["OrderQuantity"]) : 0;
						obj_TailoringOrderDetails.TailoringRemarks = objReader["TailoringRemarks"] != DBNull.Value ? Convert.ToString(objReader["TailoringRemarks"]) : "";
						obj_TailoringOrderDetails.AtTailor = objReader["AtTailor"] != DBNull.Value ? Convert.ToInt32(objReader["AtTailor"]) : 0;
						obj_TailoringOrderDetails.ReceivedFromTailor = objReader["ReceivedFromTailor"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedFromTailor"]) : 0;
                        if (RequestData.FromDeliverCode == "DeliverToCustomer")
                        {
                            obj_TailoringOrderDetails.AlreadyDeliveredQuantity = objReader["DeliveredQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DeliveredQuantity"]) : 0;
                            obj_TailoringOrderDetails.DeliveredQuantity = 0;
                        }
                        else
                        {
                            obj_TailoringOrderDetails.DeliveredQuantity = objReader["DeliveredQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DeliveredQuantity"]) : 0;                
                        }
                       
						obj_TailoringOrderDetails.CustomerDeliveryDate = objReader["CustomerDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["CustomerDeliveryDate"]) : DateTime.Now;
						obj_TailoringOrderDetails.TailorDeliveryDate = objReader["TailorDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["TailorDeliveryDate"]) : DateTime.Now;
						obj_TailoringOrderDetails.OrderStatus = objReader["OrderStatus"] != DBNull.Value ? Convert.ToString(objReader["OrderStatus"]) : "";
						obj_TailoringOrderDetails.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
						obj_TailoringOrderDetails.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
						obj_TailoringOrderDetails.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
						obj_TailoringOrderDetails.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
						TailoringOrderDetailsList.Add(obj_TailoringOrderDetails);
					}
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.TailoringOrderDetailsList = TailoringOrderDetailsList;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Order Details");
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
		public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
		{
			var TailoringOrderList = new List<TailoringOrder>();
			var RequestData = (SelectAllTailoringOrderRequest)RequestObj;
			var ResponseData = new SelectAllTailoringOrderResponse();
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				string sQuery = string.Empty;
				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

				//if (RequestData.Mode == "Edit")
				//{
				sQuery = "SELECT * FROM TailoringOrder WHERE StoreCode = '{0}'";
				sQuery = string.Format(sQuery, RequestData.StoreCode);
				//}
				//else
				//{
				//	sQuery = "Select * from StockRequestHeader with(NoLock) where status = 'Open'";
				//}

				_CommandObj = new SqlCommand(sQuery, _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var objTailoringOrder = new TailoringOrder();
						objTailoringOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objTailoringOrder.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
						objTailoringOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
						objTailoringOrder.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
						objTailoringOrder.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : "";
						objTailoringOrder.ExpectedDeliveryDate = objReader["ExpectedDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ExpectedDeliveryDate"]) : DateTime.Now;
						objTailoringOrder.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
						objTailoringOrder.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
						objTailoringOrder.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
						objTailoringOrder.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
						TailoringOrderList.Add(objTailoringOrder);
					}
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.TailoringOrderList = TailoringOrderList;
					ResponseData.ResponseDynamicData = TailoringOrderList;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Order");
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
		public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
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
		#endregion

		#region "Despatch To Tailoring Unit"
		public override SelectAllOPENTailoringOrderResponse SelectAllOPENTailoringOrder(SelectAllOPENTailoringOrderRequest ObjRequest)
		{
			var TailoringOrderList = new List<TailoringOrder>();
			var RequestData = (SelectAllOPENTailoringOrderRequest)ObjRequest;
			var ResponseData = new SelectAllOPENTailoringOrderResponse();
			SqlDataReader objReader;
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				string sQuery = string.Empty;
				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

				sQuery = "SELECT * FROM TailoringOrder WHERE OrderStatus = 'OPEN' AND DeliveredToTailor = 0 AND StoreCode = '" + RequestData.StoreCode + "'";

				_CommandObj = new SqlCommand(sQuery, _ConnectionObj);
				_CommandObj.CommandType = CommandType.Text;
				objReader = _CommandObj.ExecuteReader();
				if (objReader.HasRows)
				{
					while (objReader.Read())
					{
						var objTailoringOrder = new TailoringOrder();
						objTailoringOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
						objTailoringOrder.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
						objTailoringOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
						objTailoringOrder.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
						objTailoringOrder.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : "";
						objTailoringOrder.ExpectedDeliveryDate = objReader["ExpectedDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ExpectedDeliveryDate"]) : DateTime.Now;
						objTailoringOrder.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
						objTailoringOrder.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
						objTailoringOrder.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
						objTailoringOrder.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

						objTailoringOrder.OrderStatus = objReader["OrderStatus"] != DBNull.Value ? Convert.ToString(objReader["OrderStatus"]) : "";
						objTailoringOrder.TailoringUnitCode = objReader["TailoringUnitCode"] != DBNull.Value ? Convert.ToString(objReader["TailoringUnitCode"]) : "";
						objTailoringOrder.DeliveredToTailor = objReader["DeliveredToTailor"] != DBNull.Value ? Convert.ToBoolean(objReader["DeliveredToTailor"]) : false;

						TailoringOrderList.Add(objTailoringOrder);
					}
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.TailoringOrderList = TailoringOrderList;
					ResponseData.ResponseDynamicData = TailoringOrderList;
				}
				else
				{
					ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
					ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tailoring Order");
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
		public override SaveDespatchToTailorResponse SaveDespatchToTailor(SaveDespatchToTailorRequest ObjRequest)
		{
			var RequestData = (SaveDespatchToTailorRequest)ObjRequest;
			var ResponseData = new SaveDespatchToTailorResponse();
			StringBuilder sSql = new StringBuilder();
			var sqlCommon = new MsSqlCommon();
			try
			{
				_ConnectionString = RequestData.ConnectionString;
				_RequestFrom = RequestData.RequestFrom;

				sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
				_CommandObj = new SqlCommand("SaveDespatchToTailoringUnit", _ConnectionObj);
				_CommandObj.CommandType = CommandType.StoredProcedure;

				SqlParameter TailoringUnitCode = _CommandObj.Parameters.Add("@TailoringUnitCode", SqlDbType.VarChar);
				TailoringUnitCode.Direction = ParameterDirection.Input;
				TailoringUnitCode.Value = RequestData.TailoringUnitCode;

				SqlParameter TailorDeliveryDate = _CommandObj.Parameters.Add("@TailorDeliveryDate", SqlDbType.DateTime);
				TailorDeliveryDate.Direction = ParameterDirection.Input;
				TailorDeliveryDate.Value = sqlCommon.GetSQLServerDateString(RequestData.TailorDeliveryDate);


				SqlParameter TailoringOrderList = _CommandObj.Parameters.Add("@TailoringOrderList", SqlDbType.Xml);
				TailoringOrderList.Direction = ParameterDirection.Input;
				TailoringOrderList.Value = TailoringOrderListXML(RequestData.TailoringOrderList);
				//StockRequestDetails.Value = StockRequestDetails.ToString().Replace("&", "&#38;");

				SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 500);
				ID2.Direction = ParameterDirection.Output;

				SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
				StatusCode.Direction = ParameterDirection.Output;

				SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
				StatusMsg.Direction = ParameterDirection.Output;

				_CommandObj.CommandType = CommandType.StoredProcedure;
				_CommandObj.ExecuteNonQuery();

				string strStatusCode = StatusCode.Value.ToString();
				if (strStatusCode == "1")
				{
					ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Tailoring Order Despatch");
					ResponseData.StatusCode = Enums.OpStatusCode.Success;
					ResponseData.IDs = ID2.Value.ToString();
				}
				else if (strStatusCode == "2")
				{
					ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Tailoring Order Despatch");
				}
				else
				{
					ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Order Despatch");
				}
			}
			catch (Exception ex)
			{
				ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
				ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tailoring Order");
				ResponseData.ExceptionMessage = ex.Message;
				ResponseData.StackTrace = ex.StackTrace;
			}
			finally
			{
				sqlCommon.CloseConnection(_ConnectionObj);
			}

			return ResponseData;
		}
		public string TailoringOrderListXML(List<TailoringOrder> TailoringOrderList)
		{
			StringBuilder sSql = new StringBuilder();
			var sqlCommon = new MsSqlCommon();
			foreach (TailoringOrder objTailoringOrder in TailoringOrderList)
			{
				if(objTailoringOrder.DeliveredToTailor == true)
				{
					sSql.Append("<TailoringOrderListData>");
					sSql.Append("<ID>" + objTailoringOrder.ID + "</ID>");
					sSql.Append("<DocumentNo>" + objTailoringOrder.DocumentNo + "</DocumentNo>");
					sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrder.DocumentDate) + "</DocumentDate>");
					sSql.Append("<StoreCode>" + (objTailoringOrder.StoreCode) + "</StoreCode>");
					sSql.Append("<CustomerCode>" + objTailoringOrder.CustomerCode + "</CustomerCode>");
					sSql.Append("<ExpectedDeliveryDate>" + objTailoringOrder.ExpectedDeliveryDate + "</ExpectedDeliveryDate>");
					sSql.Append("<CreateBy>" + objTailoringOrder.CreateBy + "</CreateBy>");					
					sSql.Append("<UpdateBy>" + objTailoringOrder.UpdateBy + "</UpdateBy>");
					sSql.Append("<OrderStatus>" + objTailoringOrder.OrderStatus + "</OrderStatus>");
					sSql.Append("</TailoringOrderListData>");
				}				
			}
			//return sSql.ToString().Replace("&", "&#38;");
			return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
			//return sSql.ToString();
		}   
		#endregion

        #region "Receive From Tailor"
        public override SelectTailoringOrderForReceiveFromTailorResponse SelectTailoringOrderForReceiveFromTailor(SelectTailoringOrderForReceiveFromTailorRequest ObjRequest)
        {
            var TailoringOrderList = new List<TailoringOrder>();
            var RequestData = (SelectTailoringOrderForReceiveFromTailorRequest)ObjRequest;
            var ResponseData = new SelectTailoringOrderForReceiveFromTailorResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SelectTailoringOrderNotReceived", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StoreCode;

                SqlParameter TailoringUnitCode = _CommandObj.Parameters.Add("@TailoringUnitCode", SqlDbType.VarChar);
                TailoringUnitCode.Direction = ParameterDirection.Input;
                TailoringUnitCode.Value = RequestData.TailoringUnitCode;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTailoringOrder = new TailoringOrder();
                        objTailoringOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTailoringOrder.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        objTailoringOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objTailoringOrder.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        objTailoringOrder.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : "";
                        objTailoringOrder.ExpectedDeliveryDate = objReader["ExpectedDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ExpectedDeliveryDate"]) : DateTime.Now;
                        objTailoringOrder.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTailoringOrder.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTailoringOrder.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objTailoringOrder.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

                        objTailoringOrder.OrderStatus = objReader["OrderStatus"] != DBNull.Value ? Convert.ToString(objReader["OrderStatus"]) : "";
                        objTailoringOrder.TailoringUnitCode = objReader["TailoringUnitCode"] != DBNull.Value ? Convert.ToString(objReader["TailoringUnitCode"]) : "";
                        objTailoringOrder.DeliveredToTailor = objReader["DeliveredToTailor"] != DBNull.Value ? Convert.ToBoolean(objReader["DeliveredToTailor"]) : false;

                        TailoringOrderList.Add(objTailoringOrder);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TailoringOrderList = TailoringOrderList;
                    ResponseData.ResponseDynamicData = TailoringOrderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Receive From Tailor");
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
        public override SelectTailoringOrderDetailsForReceiveFromTailorResponse SelectTailoringOrderDetailsForReceiveFromTailor(SelectTailoringOrderDetailsForReceiveFromTailorRequest ObjRequest)
        {
            var TailoringOrderDetailsList = new List<TailoringOrderDetails>();
            var RequestData = (SelectTailoringOrderDetailsForReceiveFromTailorRequest)ObjRequest;
            var ResponseData = new SelectTailoringOrderDetailsForReceiveFromTailorResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SelectTailoringOrderDetailsNotReceived", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ID;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var obj_TailoringOrderDetails = new TailoringOrderDetails();
                        obj_TailoringOrderDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        obj_TailoringOrderDetails.TailoringOrderID = objReader["TailoringOrderID"] != DBNull.Value ? Convert.ToInt32(objReader["TailoringOrderID"]) : 0;
                        obj_TailoringOrderDetails.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
                        obj_TailoringOrderDetails.OrderQuantity = objReader["OrderQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["OrderQuantity"]) : 0;
                        obj_TailoringOrderDetails.TailoringRemarks = objReader["TailoringRemarks"] != DBNull.Value ? Convert.ToString(objReader["TailoringRemarks"]) : "";
                        obj_TailoringOrderDetails.AtTailor = objReader["AtTailor"] != DBNull.Value ? Convert.ToInt32(objReader["AtTailor"]) : 0;
                        obj_TailoringOrderDetails.ReceivedFromTailor = objReader["ReceivedFromTailor"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedFromTailor"]) : 0;
                        obj_TailoringOrderDetails.ReceivedTailor = 0;
                        obj_TailoringOrderDetails.DeliveredQuantity = objReader["DeliveredQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DeliveredQuantity"]) : 0;
                        obj_TailoringOrderDetails.CustomerDeliveryDate = objReader["CustomerDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["CustomerDeliveryDate"]) : DateTime.Now;
                        obj_TailoringOrderDetails.TailorDeliveryDate = objReader["TailorDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["TailorDeliveryDate"]) : DateTime.Now;
                        obj_TailoringOrderDetails.OrderStatus = objReader["OrderStatus"] != DBNull.Value ? Convert.ToString(objReader["OrderStatus"]) : "";
                        obj_TailoringOrderDetails.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        obj_TailoringOrderDetails.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        obj_TailoringOrderDetails.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        obj_TailoringOrderDetails.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        TailoringOrderDetailsList.Add(obj_TailoringOrderDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TailoringOrderDetailsList = TailoringOrderDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Receive From Tailor");
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
        public override SaveReceiveFromTailoringOrderResponse SaveReceiveFromTailoring(SaveReceiveFromTailoringOrderRequest ObjRequest)
        {
            var RequestData = (SaveReceiveFromTailoringOrderRequest)ObjRequest;
            var ResponseData = new SaveReceiveFromTailoringOrderResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SaveReceiveFromTailor", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ReceivedDate = _CommandObj.Parameters.Add("@ReceivedDate", SqlDbType.DateTime);
                ReceivedDate.Direction = ParameterDirection.Input;
                ReceivedDate.Value = sqlCommon.GetSQLServerDateString(RequestData.ReceivedDate);
                
                SqlParameter TailoringOrderDetailList = _CommandObj.Parameters.Add("@TailoringOrderDetailList", SqlDbType.Xml);
                TailoringOrderDetailList.Direction = ParameterDirection.Input;
                TailoringOrderDetailList.Value = ReceivedTailoringOrderDetailsXML(RequestData.TailoringOrderList);
                //StockRequestDetails.Value = StockRequestDetails.ToString().Replace("&", "&#38;");

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Receive From Tailor");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Receive From Tailor");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Receive From Tailor");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Receive From Tailor");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public string ReceivedTailoringOrderDetailsXML(List<TailoringOrder> TailoringOrderList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();

            foreach(TailoringOrder objTailoringOrder in TailoringOrderList)
            {
                foreach (TailoringOrderDetails objTailoringOrderDetails in objTailoringOrder.TailoringOrderDetailsList)
                {
                    sSql.Append("<TailoringOrderDetailsData>");
                    sSql.Append("<ID>" + objTailoringOrderDetails.ID + "</ID>");
                    sSql.Append("<TailoringOrderID>" + objTailoringOrderDetails.TailoringOrderID + "</TailoringOrderID>");
                    sSql.Append("<SKUCode>" + objTailoringOrderDetails.SKUCode + "</SKUCode>");
                    sSql.Append("<OrderQuantity>" + (objTailoringOrderDetails.OrderQuantity) + "</OrderQuantity>");
                    sSql.Append("<TailoringRemarks>" + objTailoringOrderDetails.TailoringRemarks + "</TailoringRemarks>");
                    sSql.Append("<AtTailor>" + objTailoringOrderDetails.AtTailor + "</AtTailor>");
                    sSql.Append("<ReceivedFromTailor>" + objTailoringOrderDetails.ReceivedTailor + "</ReceivedFromTailor>");
                    sSql.Append("<DeliveredQuantity>" + (objTailoringOrderDetails.DeliveredQuantity) + "</DeliveredQuantity>");
                    sSql.Append("<CustomerDeliveryDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrderDetails.CustomerDeliveryDate) + "</CustomerDeliveryDate>");
                    sSql.Append("<TailorDeliveryDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrderDetails.TailorDeliveryDate) + "</TailorDeliveryDate>");
                    sSql.Append("<OrderStatus>" + objTailoringOrderDetails.OrderStatus + "</OrderStatus>");
                    sSql.Append("<CreateBy>" + objTailoringOrderDetails.CreateBy + "</CreateBy>");
                    sSql.Append("<UpdateBy>" + objTailoringOrderDetails.UpdateBy + "</UpdateBy>");
                    sSql.Append("</TailoringOrderDetailsData>");

                }
            }
            
            //return sSql.ToString().Replace("&", "&#38;");
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString();
        }   
        #endregion
        #region "Deliver To Customer"
        public override SelectTailoringOrderDeliverToCustomerResponse SelectTailoringOrderDeliverToCustomerDetails(SelectTailoringOrderForDeliverToCustomerRequest ObjRequest)
        {
            var TailoringOrderList = new List<TailoringOrder>();
            var RequestData = (SelectTailoringOrderForDeliverToCustomerRequest)ObjRequest;
            var ResponseData = new SelectTailoringOrderDeliverToCustomerResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SelectTailoringOrderNotNotDeliverToCustomer", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StoreCode;

                SqlParameter TailoringUnitCode = _CommandObj.Parameters.Add("@TailoringUnitCode", SqlDbType.VarChar);
                TailoringUnitCode.Direction = ParameterDirection.Input;
                TailoringUnitCode.Value = RequestData.TailoringUnitCode;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTailoringOrder = new TailoringOrder();
                        objTailoringOrder.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTailoringOrder.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        objTailoringOrder.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objTailoringOrder.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        objTailoringOrder.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : "";
                        objTailoringOrder.ExpectedDeliveryDate = objReader["ExpectedDeliveryDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ExpectedDeliveryDate"]) : DateTime.Now;
                        
                        objTailoringOrder.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTailoringOrder.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTailoringOrder.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objTailoringOrder.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

                        objTailoringOrder.OrderStatus = objReader["OrderStatus"] != DBNull.Value ? Convert.ToString(objReader["OrderStatus"]) : "";
                        objTailoringOrder.TailoringUnitCode = objReader["TailoringUnitCode"] != DBNull.Value ? Convert.ToString(objReader["TailoringUnitCode"]) : "";
                        objTailoringOrder.DeliveredToTailor = objReader["DeliveredToTailor"] != DBNull.Value ? Convert.ToBoolean(objReader["DeliveredToTailor"]) : false;

                        TailoringOrderList.Add(objTailoringOrder);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TailoringOrderList = TailoringOrderList;
                    ResponseData.ResponseDynamicData = TailoringOrderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Receive From Tailor");
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

        public override SaveDeliverToCustomerResponse SaveDeliverToCustomer(SaveDeliverToCustomerRequest ObjRequest)
        {
            var RequestData = (SaveDeliverToCustomerRequest)ObjRequest;
            var ResponseData = new SaveDeliverToCustomerResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("SaveDeliverToCustomer", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ReceivedDate = _CommandObj.Parameters.Add("@ReceivedDate", SqlDbType.DateTime);
                ReceivedDate.Direction = ParameterDirection.Input;
                ReceivedDate.Value = sqlCommon.GetSQLServerDateString(RequestData.ReceivedDate);

                SqlParameter TailoringOrderDetailList = _CommandObj.Parameters.Add("@TailoringOrderDetailList", SqlDbType.Xml);
                TailoringOrderDetailList.Direction = ParameterDirection.Input;
                TailoringOrderDetailList.Value = DeliverToCustomerDetailsXML(RequestData.TailoringOrderList);
                //StockRequestDetails.Value = StockRequestDetails.ToString().Replace("&", "&#38;");

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                SqlParameter ID3 = _CommandObj.Parameters.Add("@ID3", SqlDbType.Int);
                ID3.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                string strID = ID3.Value.ToString();
                string num = ID2.Value.ToString();
                if (strStatusCode == "1")
                {

                    SelectStatus(num);

                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Deliver To Customer");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Deliver To Customer");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Deliver To Customer");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Deliver To Customer");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        private void SelectStatus(String num)
        {
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                 string str =string.Empty;
                
                             
                 string[] IDArray = null;
                IDArray = num.Split(',');
                
                foreach (string ReturnValue in IDArray)
                {
                    StringBuilder sSbl = new StringBuilder();
                    string sSql = string.Empty;
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);            
                   
                    sSbl.Append("select * from TailoringOrderDetails TD inner join TailoringOrder TAO on TD.TailoringOrderID=tao.ID where tao.DocumentNo in ('" + ReturnValue + "') and td.orderstatus<>'CLOSE'");
                    sSql = string.Format(sSbl.ToString());
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();

                    if (objReader.HasRows)
                    {
                        sqlCommon.CloseConnection(_ConnectionObj);
                    }
                    else
                    {
                        UpdateStatus(ReturnValue);
                        sqlCommon.CloseConnection(_ConnectionObj);
                    }

                }                             
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }
       
        private void UpdateStatus(string num)
        {
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string str = string.Empty;               
                string[] IDArray = null;
                IDArray = num.Split(',');
                foreach (string ReturnValue in IDArray)
                {
                    //string ID = Convert.ToString(ReturnValue.Split('|')[0]);
                    //if (str == null || str == "")
                    //{
                    //    str = "'" + ID + "'";
                    //}
                    //else
                    //    str = str + ",'" + ID + "'";

                    StringBuilder sSql = new StringBuilder();
                    sSql.Append("update TailoringOrder set OrderStatus='CLOSED' where DocumentNo in ('" + ReturnValue + "')");
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    _CommandObj.ExecuteNonQuery();
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }           

        public string DeliverToCustomerDetailsXML(List<TailoringOrder> TailoringOrderList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();

            foreach (TailoringOrder objTailoringOrder in TailoringOrderList)
            {
                foreach (TailoringOrderDetails objTailoringOrderDetails in objTailoringOrder.TailoringOrderDetailsList)
                {
                    sSql.Append("<TailoringOrderDetailsData>");
                    sSql.Append("<ID>" + objTailoringOrderDetails.ID + "</ID>");
                    sSql.Append("<TailoringOrderID>" + objTailoringOrderDetails.TailoringOrderID + "</TailoringOrderID>");
                    sSql.Append("<SKUCode>" + objTailoringOrderDetails.SKUCode + "</SKUCode>");
                    sSql.Append("<OrderQuantity>" + (objTailoringOrderDetails.OrderQuantity) + "</OrderQuantity>");
                    sSql.Append("<TailoringRemarks>" + objTailoringOrderDetails.TailoringRemarks + "</TailoringRemarks>");
                    sSql.Append("<AtTailor>" + objTailoringOrderDetails.AtTailor + "</AtTailor>");
                    sSql.Append("<ReceivedFromTailor>" + objTailoringOrderDetails.ReceivedFromTailor + "</ReceivedFromTailor>");
                    //if (objTailoringOrderDetails.OrderQuantity < objTailoringOrderDetails.DeliveredQuantity)
                    //{
                    //    sSql.Append("<DeliveredQuantity>" + 0 + "</DeliveredQuantity>");
                    //}
                    //else
                    //{
                    //    sSql.Append("<DeliveredQuantity>" + (objTailoringOrderDetails.DeliveredQuantity) + "</DeliveredQuantity>");
                    //} 
                    sSql.Append("<DeliveredQuantity>" + (objTailoringOrderDetails.DeliveredQuantity) + "</DeliveredQuantity>");
                    sSql.Append("<CustomerDeliveryDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrderDetails.CustomerDeliveryDate) + "</CustomerDeliveryDate>");
                    sSql.Append("<TailorDeliveryDate>" + sqlCommon.GetSQLServerDateString(objTailoringOrderDetails.TailorDeliveryDate) + "</TailorDeliveryDate>");
                    //sSql.Append("<OrderStatus>" + objTailoringOrderDetails.OrderStatus + "</OrderStatus>");
                    if (objTailoringOrderDetails.OrderQuantity == objTailoringOrderDetails.DeliveredQuantity + objTailoringOrderDetails.AlreadyDeliveredQuantity)
                    {
                        sSql.Append("<OrderStatus>" + "CLOSE" + "</OrderStatus>");
                    }
                    else if (objTailoringOrderDetails.OrderQuantity > objTailoringOrderDetails.DeliveredQuantity && objTailoringOrderDetails.DeliveredQuantity != 0 || objTailoringOrderDetails.AlreadyDeliveredQuantity!=0)
                    //else if (objTailoringOrderDetails.OrderQuantity  > objTailoringOrderDetails.AlreadyDeliveredQuantity)
                    {
                        sSql.Append("<OrderStatus>" + "PARTIAL CLOSE" + "</OrderStatus>");
                    }
                    else
                    {
                        sSql.Append("<OrderStatus>" + "OPEN" + "</OrderStatus>");
                    }
                    sSql.Append("<CreateBy>" + objTailoringOrderDetails.CreateBy + "</CreateBy>");
                    sSql.Append("<UpdateBy>" + objTailoringOrderDetails.UpdateBy + "</UpdateBy>");
                    sSql.Append("</TailoringOrderDetailsData>");

                }
            }

            //return sSql.ToString().Replace("&", "&#38;");
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString();
        }

        #endregion

       
    }
}
