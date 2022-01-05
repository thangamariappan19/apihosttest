using EasyBizAbsDAL.Transactions.Pricing;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Pricing
{
    public class PricePointDAL : BasePricePointDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SavePricePointRequest)RequestObj;
            var ResponseData = new SavePricePointResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateBulkPricePoint", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter Mode = _CommandObj.Parameters.Add("@Mode", SqlDbType.Int);
                Mode.Direction = ParameterDirection.Input;
                Mode.Value = RequestData.Mode;

                SqlParameter PricePointCode = _CommandObj.Parameters.Add("@PricePointCode", SqlDbType.NVarChar);
                PricePointCode.Direction = ParameterDirection.Input;
                PricePointCode.Value = RequestData.PricePointCode;

                SqlParameter PricePointName = _CommandObj.Parameters.Add("@PricePointName", SqlDbType.NVarChar);
                PricePointName.Direction = ParameterDirection.Input;
                PricePointName.Value = RequestData.PricePointName;

                SqlParameter PricePointData = _CommandObj.Parameters.Add("@PricePointData", SqlDbType.Xml);
                PricePointData.Direction = ParameterDirection.Input;
                PricePointData.Value = PricePointXml(RequestData.PricePointList);

                SqlParameter PricePointRangeData = _CommandObj.Parameters.Add("@PricePointRangeData", SqlDbType.Xml);
                PricePointRangeData.Direction = ParameterDirection.Input;
                PricePointRangeData.Value = PricePointRangeXml(RequestData.PricePointRange);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter InsertedIds = _CommandObj.Parameters.Add("@InsertedIds", SqlDbType.VarChar, int.MaxValue);
                InsertedIds.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {

                    if(RequestData.Mode==1)
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Price Point");
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Price Point");
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = Convert.ToString(InsertedIds.Value);
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price Point");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Point");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Point");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var PricePoint = new PricePoint();
            var RequestData = (DeletePricePointRequest)RequestObj;
            var ResponseData = new DeletePricePointResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from  PricePoint where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "PricePoint");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "PricePoint");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var PricePointList = new List<PricePoint>();
            var RequestData = (SelectPricePointByIDRequest)RequestObj;
            var ResponseData = new SelectPricePointByIDResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sbSql.Append("Select pp.*,bm.BrandName,cm.CurrencyName from PricePoint pp with(NoLock) ");
                sbSql.Append("inner join CurrencyMaster cm with(NoLock) on pp.BaseCurrencyID=cm.ID ");
                sbSql.Append("inner join BrandMaster bm with(NoLock) on pp.BrandID=bm.ID where pp.PricePointCode='" + RequestData.PricePointCode + "'");

                //if (!RequestData.ShowInActiveRecords)
                //{
                //    sbSql.Append(" pp.Active='True'");
                //}
                sSql = string.Format(sbSql.ToString(), RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPricePoint = new PricePoint();
                        objPricePoint.ID = Convert.ToInt32(objReader["ID"]);
                        objPricePoint.PricePointCode = Convert.ToString(objReader["PricePointCode"]);
                        objPricePoint.PricePointName = Convert.ToString(objReader["PricePointName"]);
                        objPricePoint.BrandID = Convert.ToInt32(objReader["BrandID"]);
                        objPricePoint.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                        objPricePoint.Remarks = Convert.ToString(objReader["Remarks"]);
                        objPricePoint.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objPricePoint.BaseCurrencyCode = Convert.ToString(objReader["BaseCurrencyCode"]);

                        objPricePoint.BrandName = Convert.ToString(objReader["BrandName"]);
                        objPricePoint.CurrencyName = Convert.ToString(objReader["CurrencyName"]);

                        objPricePoint.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPricePoint.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPricePoint.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPricePoint.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPricePoint.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPricePoint.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        GetPricePointRangeListRequest objGetPricePointRangeListRequest = new GetPricePointRangeListRequest();
                        objGetPricePointRangeListRequest.PricePointID = objPricePoint.ID;
                        //objGetPricePointRangeListRequest.PricePointCode = objPricePoint.PricePointCode;

                        GetPricePointRangeListResponse objGetPricePointRangeListResponse = new GetPricePointRangeListResponse();
                        objGetPricePointRangeListResponse = GetPricePointRangeList(objGetPricePointRangeListRequest);

                        objPricePoint.PricePointRangeList = new List<PricePointRange>();
                        if (objGetPricePointRangeListResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPricePoint.PricePointRangeList = objGetPricePointRangeListResponse.PricePointRangeList;
                        }

                        PricePointList.Add(objPricePoint);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PricePointList = PricePointList;
                    ResponseData.ResponseDynamicData = PricePointList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Point");
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

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var PricePointList = new List<PricePoint>();
            var RequestData = (SelectAllPricePointRequest)RequestObj;
            var ResponseData = new SelectAllPricePointResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.ProcessMode == Enums.ProcessMode.ViewMode)
                {
                    sbSql.Append("Select DISTINCT pp.PricePointCode,pp.PricePointName,pp.Active,cm.CurrencyName from PricePoint pp with(NoLock) ");
                    sbSql.Append("inner join CurrencyMaster cm with(NoLock) on pp.BaseCurrencyID=cm.ID ");
                }
                else
                {
                    sbSql.Append("Select pp.*,bm.BrandName,cm.CurrencyName from PricePoint pp with(NoLock) ");
                    sbSql.Append("inner join CurrencyMaster cm with(NoLock) on pp.BaseCurrencyID=cm.ID ");
                    sbSql.Append("inner join BrandMaster bm with(NoLock) on pp.BrandID=bm.ID ");
                }

                _CommandObj = new SqlCommand(sbSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPricePoint = new PricePoint();
                        if (RequestData.ProcessMode == Enums.ProcessMode.ViewMode)
                        {
                            objPricePoint.PricePointCode = Convert.ToString(objReader["PricePointCode"]);
                            objPricePoint.PricePointName = Convert.ToString(objReader["PricePointName"]);
                            objPricePoint.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                            objPricePoint.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        }
                        else
                        {
                            objPricePoint.ID = Convert.ToInt32(objReader["ID"]);
                            objPricePoint.PricePointCode = Convert.ToString(objReader["PricePointCode"]);
                            objPricePoint.PricePointName = Convert.ToString(objReader["PricePointName"]);
                            objPricePoint.BrandID = Convert.ToInt32(objReader["BrandID"]);
                            objPricePoint.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                            objPricePoint.Remarks = Convert.ToString(objReader["Remarks"]);
                            objPricePoint.BrandCode = Convert.ToString(objReader["BrandCode"]);
                            objPricePoint.BaseCurrencyCode = Convert.ToString(objReader["BaseCurrencyCode"]);
                            objPricePoint.BrandName = Convert.ToString(objReader["BrandName"]);
                            objPricePoint.CurrencyName = Convert.ToString(objReader["CurrencyName"]);

                            objPricePoint.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                            objPricePoint.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                            objPricePoint.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                            objPricePoint.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                            objPricePoint.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                            objPricePoint.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                            if (RequestData.ProcessMode != Enums.ProcessMode.ViewMode)
                            {
                                GetPricePointRangeListRequest objGetPricePointRangeListRequest = new GetPricePointRangeListRequest();
                                objGetPricePointRangeListRequest.PricePointID = objPricePoint.ID;
                                //objGetPricePointRangeListRequest.PricePointCode = objPricePoint.PricePointCode;

                                GetPricePointRangeListResponse objGetPricePointRangeListResponse = new GetPricePointRangeListResponse();
                                objGetPricePointRangeListResponse = GetPricePointRangeList(objGetPricePointRangeListRequest);

                                objPricePoint.PricePointRangeList = new List<PricePointRange>();
                                if (objGetPricePointRangeListResponse.StatusCode == Enums.OpStatusCode.Success)
                                {
                                    objPricePoint.PricePointRangeList = objGetPricePointRangeListResponse.PricePointRangeList;
                                }
                            }
                        }
                        PricePointList.Add(objPricePoint);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PricePointList = PricePointList;
                    ResponseData.ResponseDynamicData = PricePointList; ;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Point");
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public string PricePointXml(List<PricePoint> PricePointList)
        {
            StringBuilder sXml = new StringBuilder();
            foreach (PricePoint objPricePointRange in PricePointList)
            {
                sXml.Append("<PricePointData>");
                sXml.Append("<ID>" + objPricePointRange.ID + "</ID>");
                sXml.Append("<PricePointCode>" + objPricePointRange.PricePointCode + "</PricePointCode>");
                sXml.Append("<PricePointName>" + objPricePointRange.PricePointName + "</PricePointName>");
                sXml.Append("<BrandID>" + objPricePointRange.BrandID + "</BrandID>");
                sXml.Append("<BrandCode>" + objPricePointRange.BrandCode + "</BrandCode>");
                sXml.Append("<BaseCurrencyID>" + objPricePointRange.BaseCurrencyID + "</BaseCurrencyID>");
                sXml.Append("<BaseCurrencyCode>" + objPricePointRange.BaseCurrencyCode + "</BaseCurrencyCode>");
                sXml.Append("<CreateBy>" + objPricePointRange.CreateBy + "</CreateBy>");
                sXml.Append("</PricePointData>");
            }
            return sXml.ToString();
        }

        public string PricePointRangeXml(List<PricePointRange> PricePointList)
        {
            StringBuilder sXml = new StringBuilder();

            if (PricePointList != null && PricePointList.Count > 0)
            {
                    foreach (PricePointRange objPricePointRange in PricePointList)
                    {
                        sXml.Append("<PricePointRangeData>");
                        sXml.Append("<ID>" + objPricePointRange.ID + "</ID>");
                        sXml.Append("<PricePointID>" + objPricePointRange.PricePointID + "</PricePointID>");
                        sXml.Append("<RangeFrom>" + objPricePointRange.RangeFrom + "</RangeFrom>");
                        sXml.Append("<RangeTo>" + objPricePointRange.RangeTo + "</RangeTo>");
                        sXml.Append("<CurrencyID>" + objPricePointRange.CurrencyID + "</CurrencyID>");
                        sXml.Append("<InternationalCode>" + objPricePointRange.InternationalCode + "</InternationalCode>");
                        sXml.Append("<Price>" + objPricePointRange.Price + "</Price>");
                        sXml.Append("<PricePointCode>" + objPricePointRange.PricePointCode + "</PricePointCode>");
                        sXml.Append("<BrandCode>" + objPricePointRange.BrandCode + "</BrandCode>");
                        sXml.Append("</PricePointRangeData>");
                    }
                
            }
            return sXml.ToString();
        }

        public override GetPricePointRangeListResponse GetPricePointRangeList(GetPricePointRangeListRequest RequestObj)
        {
            var PricePointRangeList = new List<PricePointRange>();
            var RequestData = (GetPricePointRangeListRequest)RequestObj;
            var ResponseData = new GetPricePointRangeListResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sbSql.Append("Select * from PricePointRange ppr with(NoLock) ");

                if (RequestData.PricePointID != 0)
                {
                    sbSql.Append("where PricePointID={0}");
                }

                sSql = string.Format(sbSql.ToString(), RequestData.PricePointID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPricePointRange = new PricePointRange();
                        objPricePointRange.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPricePointRange.PricePointID = objReader["PricePointID"] != DBNull.Value ? Convert.ToInt32(objReader["PricePointID"]) : 0;
                        objPricePointRange.RangeFrom = objReader["RangeFrom"] != DBNull.Value ? Convert.ToDecimal(objReader["RangeFrom"]) : 0;
                        objPricePointRange.RangeTo = objReader["RangeTo"] != DBNull.Value ? Convert.ToDecimal(objReader["RangeTo"]) : 0;
                        objPricePointRange.CurrencyID = objReader["CurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["CurrencyID"]) : 0;
                        objPricePointRange.InternationalCode = Convert.ToString(objReader["InternationalCode"]);
                        objPricePointRange.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;

                        objPricePointRange.PricePointCode = objReader["PricePointCode"] != DBNull.Value ? Convert.ToString(objReader["PricePointCode"]) : string.Empty;
                        objPricePointRange.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : string.Empty;

                        PricePointRangeList.Add(objPricePointRange);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PricePointRangeList = PricePointRangeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Point");
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

        public override SelectAllPricePointResponse API_SelectALL(SelectAllPricePointRequest requestData)
        {
            var PricePointList = new List<PricePoint>();
            var RequestData = (SelectAllPricePointRequest)requestData;
            var ResponseData = new SelectAllPricePointResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sbSql.Append("Select DISTINCT pp.PricePointCode,pp.PricePointName,pp.Active,cm.CurrencyName,RC.TOTAL_CNT [RecordCount] from PricePoint pp with(NoLock) ");
                sbSql.Append("inner join CurrencyMaster cm with(NoLock) on pp.BaseCurrencyID=cm.ID ");
                sbSql.Append("LEFT JOIN(Select  count(pp1.ID) As TOTAL_CNT From PricePoint pp1 with(NoLock) ");
                sbSql.Append("inner join CurrencyMaster cm1 with(NoLock) on pp1.BaseCurrencyID=cm1.ID ");
                sbSql.Append(" where pp1.Active = " + RequestData.IsActive + " ");
                sbSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sbSql.Append("or pp1.PricePointCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or pp1.PricePointName like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or cm1.CurrencyName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ");

                sbSql.Append(" where pp.Active = " + RequestData.IsActive + " ");
                sbSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sbSql.Append("or pp.PricePointCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or pp.PricePointName like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or cm.CurrencyName like isnull('%" + RequestData.SearchString + "%','')) ");
                sbSql.Append(" order by PricePointCode asc ");
                sbSql.Append("offset " + RequestData.Offset + " rows ");
                sbSql.Append("fetch first " + RequestData.Limit + " rows only");



                _CommandObj = new SqlCommand(sbSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPricePoint = new PricePoint();
                        /*if (RequestData.ProcessMode == Enums.ProcessMode.ViewMode)
                        {*/
                            objPricePoint.PricePointCode = Convert.ToString(objReader["PricePointCode"]);
                            objPricePoint.PricePointName = Convert.ToString(objReader["PricePointName"]);
                            objPricePoint.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                            objPricePoint.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                            ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                        //}
                        /*else
                        {
                            objPricePoint.ID = Convert.ToInt32(objReader["ID"]);
                            objPricePoint.PricePointCode = Convert.ToString(objReader["PricePointCode"]);
                            objPricePoint.PricePointName = Convert.ToString(objReader["PricePointName"]);
                            objPricePoint.BrandID = Convert.ToInt32(objReader["BrandID"]);
                            objPricePoint.BaseCurrencyID = Convert.ToInt32(objReader["BaseCurrencyID"]);
                            objPricePoint.Remarks = Convert.ToString(objReader["Remarks"]);
                            objPricePoint.BrandCode = Convert.ToString(objReader["BrandCode"]);
                            objPricePoint.BaseCurrencyCode = Convert.ToString(objReader["BaseCurrencyCode"]);
                            objPricePoint.BrandName = Convert.ToString(objReader["BrandName"]);
                            objPricePoint.CurrencyName = Convert.ToString(objReader["CurrencyName"]);

                            objPricePoint.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                            objPricePoint.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                            objPricePoint.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                            objPricePoint.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                            objPricePoint.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                            objPricePoint.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                            if (RequestData.ProcessMode != Enums.ProcessMode.ViewMode)
                            {
                                GetPricePointRangeListRequest objGetPricePointRangeListRequest = new GetPricePointRangeListRequest();
                                objGetPricePointRangeListRequest.PricePointID = objPricePoint.ID;
                                //objGetPricePointRangeListRequest.PricePointCode = objPricePoint.PricePointCode;

                                GetPricePointRangeListResponse objGetPricePointRangeListResponse = new GetPricePointRangeListResponse();
                                objGetPricePointRangeListResponse = GetPricePointRangeList(objGetPricePointRangeListRequest);

                                objPricePoint.PricePointRangeList = new List<PricePointRange>();
                                if (objGetPricePointRangeListResponse.StatusCode == Enums.OpStatusCode.Success)
                                {
                                    objPricePoint.PricePointRangeList = objGetPricePointRangeListResponse.PricePointRangeList;
                                }
                            }
                        }*/
                        PricePointList.Add(objPricePoint);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PricePointList = PricePointList;
                    ResponseData.ResponseDynamicData = PricePointList; ;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Point");
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

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
