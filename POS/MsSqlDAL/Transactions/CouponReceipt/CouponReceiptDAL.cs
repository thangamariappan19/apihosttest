using EasyBizAbsDAL.Transactions.CouponReceipt;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponReceipt;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.CouponReceipt
{
   public class CouponReceiptDAL : BaseCouponReceiptDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCouponReceiptRequest)RequestObj;
            var ResponseData = new SaveCouponReceiptResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateCouponreceipt", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter CouponReceiptID = _CommandObj.Parameters.Add("@CouponReceiptID", SqlDbType.Int);
                CouponReceiptID.Direction = ParameterDirection.Input;
                CouponReceiptID.Value = RequestData.CouponReceiptHeaderRecord.ID;

                SqlParameter CouponCode = _CommandObj.Parameters.Add("@CouponCode", SqlDbType.NVarChar);
                CouponCode.Direction = ParameterDirection.Input;
                CouponCode.Value = RequestData.CouponReceiptHeaderRecord.CouponCode;

                SqlParameter CouponID = _CommandObj.Parameters.Add("@CouponID", SqlDbType.Int);
                CouponID.Direction = ParameterDirection.Input;
                CouponID.Value = RequestData.CouponReceiptHeaderRecord.CouponID;

                SqlParameter CurrentLocation = _CommandObj.Parameters.Add("@CurrentLocation", SqlDbType.NVarChar);
                CurrentLocation.Direction = ParameterDirection.Input;
                CurrentLocation.Value = RequestData.CouponReceiptHeaderRecord.CurrentLocation;  

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CouponReceiptHeaderRecord.Active;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CouponReceiptHeaderRecord.CreateBy;

                SqlParameter CouponReceiptDetails = _CommandObj.Parameters.Add("@CouponReceiptDetails", SqlDbType.Xml);
                CouponReceiptDetails.Direction = ParameterDirection.Input;
                CouponReceiptDetails.Value = CouponReceiptDetailMasterXML(RequestData.CouponReceiptDetailsList);


                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon Receipt");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon Receipt");
                }
                else
                {
                    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Transfer");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string CouponReceiptDetailMasterXML(List<CouponReceiptDetails> CouponReceiptDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (CouponReceiptDetails objCouponReceiptDetails in CouponReceiptDetailMasterList)
            {
                if (objCouponReceiptDetails.IsSaved != true)
                {
                    sSql.Append("<CouponReceiptDetailsData>");
                    sSql.Append("<ID>" + objCouponReceiptDetails.ID + "</ID>");
                    sSql.Append("<CouponReceiptHeaderID>" + objCouponReceiptDetails.CouponReceiptHeaderID + "</CouponReceiptHeaderID>");
                    sSql.Append("<CouponSerialCode>" + objCouponReceiptDetails.CouponSerialCode + "</CouponSerialCode>");
                    sSql.Append("<IssuedStatus>" + objCouponReceiptDetails.IssuedStatus + "</IssuedStatus>");
                    sSql.Append("<RedeemedStatus>" + (objCouponReceiptDetails.RedeemedStatus) + "</RedeemedStatus>");
                    sSql.Append("<PhysicalStore>" + (objCouponReceiptDetails.PhysicalStore) + "</PhysicalStore>");
                    sSql.Append("<SCN>" + (objCouponReceiptDetails.SCN) + "</SCN>");
                    sSql.Append("</CouponReceiptDetailsData>");
                }

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
            var CouponReceiptHeaderRecord = new CouponReceiptHeader();
            var RequestData = (SelectByIDCouponReceiptRequest)RequestObj;
            var ResponseData = new SelectByIDCouponReceiptResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from CouponReceiptHeader  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponReceiptHeader = new CouponReceiptHeader();
                        objCouponReceiptHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponReceiptHeader.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        objCouponReceiptHeader.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponReceiptHeader.CurrentLocation = Convert.ToString(objReader["CurrentLocation"]);
                        objCouponReceiptHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponReceiptHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponReceiptHeader.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;



                        objCouponReceiptHeader.CouponReceiptDetailsList = new List<CouponReceiptDetails>();

                        SelectByCouponReceiptDetailsRequest objSelectByCouponReceiptDetailsRequest = new SelectByCouponReceiptDetailsRequest();
                        SelectByCouponReceiptDetailsResponse objSelectByCouponReceiptDetailsResponse = new SelectByCouponReceiptDetailsResponse();
                        objSelectByCouponReceiptDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByCouponReceiptDetailsResponse = SelectByCouponReceiptDetails(objSelectByCouponReceiptDetailsRequest);
                        if (objSelectByCouponReceiptDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCouponReceiptHeader.CouponReceiptDetailsList = objSelectByCouponReceiptDetailsResponse.CouponReceiptDetailsRecord;
                        }


                        ResponseData.CouponReceiptHeaderRecord = objCouponReceiptHeader;
                        ResponseData.ResponseDynamicData = objCouponReceiptHeader;

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Receipt Master");
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
        public override SelectByCouponReceiptDetailsResponse SelectByCouponReceiptDetails(SelectByCouponReceiptDetailsRequest ObjRequest)
        {
            var CouponReceiptDetailsList = new List<CouponReceiptDetails>();
            var RequestData = (SelectByCouponReceiptDetailsRequest)ObjRequest;
            var ResponseData = new SelectByCouponReceiptDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from CouponReceiptDetails ");
                sSql.Append("where  CouponReceiptHeaderID=" + RequestData.ID + " ");
                sSql.Append("order by id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponReceiptDetails = new CouponReceiptDetails();
                        objCouponReceiptDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponReceiptDetails.CouponReceiptHeaderID = objReader["CouponReceiptHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponReceiptHeaderID"]) : 0;

                        objCouponReceiptDetails.CouponSerialCode = Convert.ToString(objReader["CouponSerialCode"]);
                        objCouponReceiptDetails.IssuedStatus = objReader["IssuedStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuedStatus"]) : false;
                        objCouponReceiptDetails.RedeemedStatus = objReader["RedeemedStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["RedeemedStatus"]) : false;
                        objCouponReceiptDetails.PhysicalStore = Convert.ToString(objReader["PhysicalStore"]);
                        objCouponReceiptDetails.IsSaved = objReader["IsSaved"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSaved"]) : true;


                        CouponReceiptDetailsList.Add(objCouponReceiptDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponReceiptDetailsRecord = CouponReceiptDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CouponReceipt");
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
            var CouponReceiptHeaderList = new List<CouponReceiptHeader>();
            var RequestData = (SelectAllCouponReceiptRequest)RequestObj;
            var ResponseData = new SelectAllCouponReceiptResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from CouponReceiptHeader  ";



                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponTransfer = new CouponReceiptHeader();
                        objCouponTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponTransfer.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        objCouponTransfer.CouponCode = Convert.ToString(objReader["CouponCode"]);                        
                        objCouponTransfer.CurrentLocation = Convert.ToString(objReader["CurrentLocation"]);                      
                        objCouponTransfer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponTransfer.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponTransfer.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        CouponReceiptHeaderList.Add(objCouponTransfer);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponReceiptHeaderList = CouponReceiptHeaderList;

                    ResponseData.ResponseDynamicData = CouponReceiptHeaderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Receipt Master");
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

        public override GetSerialNumberResponse SelectByCouponReceiptDetails(GetSerialNumberRequest ObjRequest)
        {
            var CouponReceiptDetailsList = new List<CouponReceiptDetails>();
            var RequestData = (GetSerialNumberRequest)ObjRequest;
            var ResponseData = new GetSerialNumberResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetCouponSerialNo", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                if (RequestData.CouponCode != null && RequestData.FromSerialNum != null && RequestData.ToSerialNum != null)
                {
                    _CommandObj.Parameters.AddWithValue("@CouponCode", RequestData.CouponCode);
                    _CommandObj.Parameters.AddWithValue("@StartNo", RequestData.FromSerialNum);
                    _CommandObj.Parameters.AddWithValue("@EndNo", RequestData.ToSerialNum);                   
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@CouponCode", "");
                    _CommandObj.Parameters.AddWithValue("@StartNo", "");
                    _CommandObj.Parameters.AddWithValue("@@EndNo", "");                                     
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponReceiptDetails = new CouponReceiptDetails();
                        objCouponReceiptDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objCouponReceiptDetails.CouponReceiptHeaderID = objReader["CouponReceiptHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponReceiptHeaderID"]) : 0;

                        objCouponReceiptDetails.CouponSerialCode = Convert.ToString(objReader["CouponSerialCode"]);
                        objCouponReceiptDetails.IssuedStatus = objReader["IssuedStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuedStatus"]) : false;
                        objCouponReceiptDetails.RedeemedStatus = objReader["RedeemedStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["RedeemedStatus"]) : false;
                        objCouponReceiptDetails.PhysicalStore = Convert.ToString(objReader["PhysicalStore"]);
                        objCouponReceiptDetails.IsSaved = objReader["IsSaved"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSaved"]) : false;

                        CouponReceiptDetailsList.Add(objCouponReceiptDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponReceiptDetailsRecord = CouponReceiptDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CouponReceipt");
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

        public override SelectAllCouponReceiptResponse API_SelectALLCouponReceipt(SelectAllCouponReceiptRequest RequestObj)
        {
            var CouponReceiptHeaderList = new List<CouponReceiptHeader>();
            var RequestData = (SelectAllCouponReceiptRequest)RequestObj;
            var ResponseData = new SelectAllCouponReceiptResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sbSql.Append("Select ID,CouponID,CouponCode,CurrentLocation,Active,CreateBy,SCN, RC.TOTAL_CNT [RecordCount] from CouponReceiptHeader ");
                sbSql.Append(" LEFT JOIN(Select  count(CR.ID) As TOTAL_CNT From CouponReceiptHeader CR with(NoLock) ");
                sbSql.Append(" where CR.Active = " + RequestData.IsActive + " ");
                sbSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sbSql.Append("or CR.CouponCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or CR.CurrentLocation like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                sbSql.Append(" where Active = " + RequestData.IsActive + " ");
                sbSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sbSql.Append("or CouponCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or CurrentLocation like isnull('%" + RequestData.SearchString + "%',''))");
                sbSql.Append("order by CouponCode asc ");
                sbSql.Append("offset " + RequestData.Offset + " rows ");
                sbSql.Append("fetch first " + RequestData.Limit + " rows only");


                _CommandObj = new SqlCommand(sbSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponTransfer = new CouponReceiptHeader();
                        objCouponTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponTransfer.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        objCouponTransfer.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponTransfer.CurrentLocation = Convert.ToString(objReader["CurrentLocation"]);
                        objCouponTransfer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponTransfer.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponTransfer.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        CouponReceiptHeaderList.Add(objCouponTransfer);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponReceiptHeaderList = CouponReceiptHeaderList;

                    ResponseData.ResponseDynamicData = CouponReceiptHeaderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Receipt Master");
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

        public override GetSerialNumberResponse API_GetSerialNumberDetails(GetSerialNumberRequest ObjRequest)
        {
            var RequestData = (GetSerialNumberRequest)ObjRequest;
            var ResponseData = new GetSerialNumberResponse();
            var TempCouponReceiptDetailsList = new List<CouponReceiptDetails>();
            try
            {
               
                if (ObjRequest.FromSerialNum != "" && ObjRequest.ToSerialNum != "")
                {
                    int index = ObjRequest.FromSerialNum.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                    string chars = ObjRequest.FromSerialNum.Substring(0, index);
                    int num = Int32.Parse(ObjRequest.FromSerialNum.Substring(index));

                    int index1 = ObjRequest.ToSerialNum.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                    string chars1 = ObjRequest.ToSerialNum.Substring(0, index1);
                    int num1 = Int32.Parse(ObjRequest.ToSerialNum.Substring(index1));

                    int Fromlength = ObjRequest.FromSerialNum.Length;
                    int Tolength = ObjRequest.ToSerialNum.Length;

                    if (chars == chars1 && Fromlength <= Tolength)
                    {
                        string cell = ObjRequest.FromSerialNum;
                        int row, a = getIndexofNumber(cell);
                        string Numberpart = cell.Substring(a, cell.Length - a);
                        row = Convert.ToInt32(Numberpart);

                        string cell1 = ObjRequest.ToSerialNum;
                        int row1, a1 = getIndexofNumber(cell1);
                        string Numberpart1 = cell1.Substring(a1, cell1.Length - a1);
                        row1 = Convert.ToInt32(Numberpart1);



                        int index2 = ObjRequest.FromSerialNum.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                        string chars2 = ObjRequest.FromSerialNum.Substring(0, index2);
                        int num2 = Int32.Parse(ObjRequest.FromSerialNum.Substring(index2));

                        int index3 = ObjRequest.ToSerialNum.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                        string chars3 = ObjRequest.FromSerialNum.Substring(0, index3);
                        int num3 = Int32.Parse(ObjRequest.ToSerialNum.Substring(index3));

                        for (int num4; num2 <= num3; num2++)
                        {
                            var objCashInCashOutDetails = new CouponReceiptDetails();

                            int length = Numberpart.Length;
                            string str = "D" + length;
                            String Serailcode = chars2 + Convert.ToString(num2.ToString(str));
                            var DayInData = TempCouponReceiptDetailsList.Where(x => x.CouponSerialCode == Serailcode).FirstOrDefault();
                            if (DayInData == null)
                            {
                                objCashInCashOutDetails.CouponSerialCode = chars2 + Convert.ToString(num2.ToString(str));
                                objCashInCashOutDetails.IssuedStatus = false;
                                objCashInCashOutDetails.PhysicalStore = "MainServer";
                                objCashInCashOutDetails.RedeemedStatus = false;
                                TempCouponReceiptDetailsList.Add(objCashInCashOutDetails);
                            }
                            else
                            {

                            }
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.CouponReceiptDetailsRecord = TempCouponReceiptDetailsList;
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Serial num series is mismatched..");
                     
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Receipt Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        private void AddSubBrandRow(string fromSerialNum, string toSerialNum)
        {
            var TempCouponReceiptDetailsList = new List<CouponReceiptDetails>();
            var ResponseData = new GetSerialNumberResponse();

            

        }

        private int getIndexofNumber(string cell)
        {
            int indexofNum = -1;
            foreach (char c in cell)
            {
                indexofNum++;
                if (Char.IsDigit(c))
                {
                    return indexofNum;
                }
            }
            return indexofNum;
        }
    }
}
