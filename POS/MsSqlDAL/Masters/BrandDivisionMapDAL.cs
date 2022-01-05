using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
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
    public class BrandDivisionMapDAL : BaseBrandDivisionMapDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveBrandDivisionMapRequest)RequestObj;
            var ResponseData = new SaveBrandDivisionMapResponse();
            var DivisionMasterList = RequestData.BrandDivisionList;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateBrandDivisionMap", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                //SqlParameter BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                //BrandID.Direction = ParameterDirection.Input;
                //BrandID.Value = RequestData.BrandDivisionRecord.BrandID;

                foreach (BrandDivisionTypes objBrandDivision in DivisionMasterList)
                {
                    sSql.Append("<BrandandDivisionMapping>");
                    sSql.Append("<ID>" + objBrandDivision.ID + "</ID>");
                    sSql.Append("<DivisionID>" + (objBrandDivision.DivisionID) + "</DivisionID>");
                    sSql.Append("<DiviisonCode>" + (objBrandDivision.DivisionCode) + "</DiviisonCode>");
                    sSql.Append("<DivisionName>" + objBrandDivision.DivisionName + "</DivisionName>");
                    sSql.Append("<Active>" + (objBrandDivision.Active) + "</Active>");
                    sSql.Append("<CreateBy>" + (objBrandDivision.CreateBy) + "</CreateBy>");
                    sSql.Append("<IsDeleted>" + (objBrandDivision.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<BrandID>" + objBrandDivision.BrandID + "</BrandID>");
                    sSql.Append("<BrandCode>" + (objBrandDivision.BrandCode) + "</BrandCode>");
                    sSql.Append("</BrandandDivisionMapping>");
                }

                var BrandDivisionData = _CommandObj.Parameters.Add("@BrandDivisionData", SqlDbType.Xml);
                BrandDivisionData.Direction = ParameterDirection.Input;
                BrandDivisionData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@BrandDivIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "BrandDivision Map");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "BrandDivision Map");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "BrandDivision Map");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.ItIsInRelationdhip.Replace("{}", "BrandDivision Map");
                }
            }


            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "BrandDivision Map");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;

        }
        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var BrandDivisionRecord = new BrandDivisionTypes();
            var RequestData = (DeleteBrandDivisionRequest)RequestObj;
            var ResponseData = new DeleteBrandDivisionMapResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from BrandandCollectionMapping where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "BrandDivision Map");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "BrandDivision Map");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }


        public override EasyBizResponse.Masters.BrandDivisionMapResponse.SelectBrandDivisionMapLookUpResponse SelectBrandDivisionMapLookUp(EasyBizRequest.Masters.BrandDivisionMapRequest.SelectBrandDivisionLookUpRequest ObjRequest)
        {
            var BrandDivisionList = new List<BrandDivisionTypes>();
            var RequestData = (SelectBrandDivisionLookUpRequest)ObjRequest;
            var ResponseData = new SelectBrandDivisionMapLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,[DivisionName],DivisionCode from BrandDivisionMapping with(NoLock) where Active='true'";

                if (RequestData.BrandID != 0)
                {
                    sQuery = sQuery + " and BrandID='" + RequestData.BrandID + "' or BrandCode='" + RequestData.BrandCode + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandDivision = new BrandDivisionTypes();
                        objBrandDivision.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandDivision.DivisionName = Convert.ToString(objReader["DivisionName"]);
                        objBrandDivision.DivisionCode = Convert.ToString(objReader["DivisionCode"]);            
                        BrandDivisionList.Add(objBrandDivision);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BrandDivisionList = BrandDivisionList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "BrandDivision Map");
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

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var BrandDivisionRecord = new BrandDivisionTypes();
            var RequestData = (SelectByBrandDivisionIDRequest)RequestObj;
            var ResponseData = new SelectByBrandDivisionIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from BrandandDivisionMapping with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandDivision = new BrandDivisionTypes();
                        objBrandDivision.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandDivision.DivisionCode = Convert.ToString(objReader["DivisionCode"]);
                        objBrandDivision.DivisionName = Convert.ToString(objReader["DivisionName"]);
                        objBrandDivision.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objBrandDivision.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrandDivision.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objBrandDivision.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objBrandDivision.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objBrandDivision.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objBrandDivision.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objBrandDivision.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.BrandDivisionRecord = objBrandDivision;
                        ResponseData.ResponseDynamicData = objBrandDivision;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "BrandDivision MAp");
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
            var BrandDivisionList = new List<BrandDivisionTypes>();
            var RequestData = (SelectAllBrandDivisionRequest)RequestObj;
            var ResponseData = new SelectAllBrandDivisionMapResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = null;
                
                if(RequestData.BrandID > 0)
                {
                    sSql = "Select DM.ID as DivisionID,dm.DivisionCode,dm.DivisionName,BDM.ID,bdm.BrandID,bdm.BrandCode,bdm.Active from DivisionMaster DM with(nolock) left join BrandandDivisionMapping BDM with(nolock) ON DM.DivisionCode=BDM.DiviisonCode  and dm.Active='True'";
                    sSql = sSql + " and BrandID=" + RequestData.BrandID;
                }                   
                else
                    sSql = "Select DM.ID as DivisionID,dm.DivisionCode,dm.DivisionName,BDM.ID,bdm.BrandID,bdm.BrandCode,bdm.Active from DivisionMaster DM with(nolock) join BrandandDivisionMapping BDM with(nolock) ON DM.DivisionCode=BDM.DiviisonCode  and dm.Active='True'";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandDivision = new BrandDivisionTypes();
                        objBrandDivision.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandDivision.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) : 0;
                        objBrandDivision.DivisionCode = Convert.ToString(objReader["DivisionCode"]);
                        objBrandDivision.DivisionName = Convert.ToString(objReader["DivisionName"]);
                        objBrandDivision.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objBrandDivision.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrandDivision.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        //objBrandDivision.BrandCode = Convert.ToString(objReader["BrandCode"]);

                        /*if (objBrandDivision.BrandID > 0)
                        {
                            objBrandDivision.Active = true;
                        }*/

                        BrandDivisionList.Add(objBrandDivision);
                    }
                    ResponseData.BrandDivisionList = BrandDivisionList;
                    ResponseData.ResponseDynamicData = BrandDivisionList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "BrandDivision Map");
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


        public override SelectBrandDivListforCategoryResponse SelectBrandDivisionListByBrand(SelectBrandDivListforCategoryRequest RequestObj)
        {
            var MASBrandDivisionForCategoryList = new List<BrandDivisionTypes>();
            var RequestData = (SelectBrandDivListforCategoryRequest)RequestObj;
            var ResponseData = new SelectBrandDivListforCategoryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from BrandDivisionTypes with(NoLock) where BrandID='" + RequestData.BrandID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandDivision = new BrandDivisionTypes();
                        objBrandDivision.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandDivision.DivisionCode = Convert.ToString(objReader["DivisionCode"]);
                        objBrandDivision.DivisionName = Convert.ToString(objReader["DivisionName"]);
                        objBrandDivision.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objBrandDivision.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objBrandDivision.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objBrandDivision.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objBrandDivision.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objBrandDivision.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objBrandDivision.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        MASBrandDivisionForCategoryList.Add(objBrandDivision);
                    }
                    ResponseData.BrandDivisionList = MASBrandDivisionForCategoryList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Division Map");
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
    }
}
