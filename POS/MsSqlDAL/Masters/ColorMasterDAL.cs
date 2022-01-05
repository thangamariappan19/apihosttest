using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizResponse.Masters.ColorMasterResponse;
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
   public class ColorMasterDAL : BaseColorMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveColorRequest)RequestObj;
            var ResponseData = new SaveColorResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertColorMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ColorID = _CommandObj.Parameters.Add("@ColorID", SqlDbType.Int);
                ColorID.Direction = ParameterDirection.Input;
                ColorID.Value = RequestData.ColorRecord.ID;

                var InternalCode = _CommandObj.Parameters.Add("@InternalCode", SqlDbType.NVarChar);
                InternalCode.Direction = ParameterDirection.Input;
                InternalCode.Value = RequestData.ColorRecord.InternalCode;

                var ColorCode = _CommandObj.Parameters.Add("@ColorCode", SqlDbType.NVarChar);
                ColorCode.Direction = ParameterDirection.Input;
                ColorCode.Value = RequestData.ColorRecord.ColorCode;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ColorRecord.Description;

                var Shade = _CommandObj.Parameters.Add("@Shade", SqlDbType.NVarChar);
                Shade.Direction = ParameterDirection.Input;
                Shade.Value = RequestData.ColorRecord.Shade;

                var NRFCode = _CommandObj.Parameters.Add("@NRFCode", SqlDbType.NVarChar);
                NRFCode.Direction = ParameterDirection.Input;
                NRFCode.Value = RequestData.ColorRecord.NRFCode;

                var Colors = _CommandObj.Parameters.Add("@Color", SqlDbType.Int);
                Colors.Direction = ParameterDirection.Input;
                Colors.Value = RequestData.ColorRecord.Colors;

                var Attribute1 = _CommandObj.Parameters.Add("@Attribute1", SqlDbType.NVarChar);
                Attribute1.Direction = ParameterDirection.Input;
                Attribute1.Value = RequestData.ColorRecord.Attribute1;

                var Attribute2 = _CommandObj.Parameters.Add("@Attribute2", SqlDbType.NVarChar);
                Attribute2.Direction = ParameterDirection.Input;
                Attribute2.Value = RequestData.ColorRecord.Attribute2;

                var MultiColorImage = _CommandObj.Parameters.Add("@MultiColorImage", SqlDbType.NVarChar);
                MultiColorImage.Direction = ParameterDirection.Input;
                MultiColorImage.Value = RequestData.ColorRecord.MultiColorImage;      

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ColorRecord.CreateBy;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.ColorRecord.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ColorRecord.Active;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Color");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Color");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Color");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Color Master");
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
            var RequestData = (UpdateColorRequest)RequestObj;
            var ResponseData = new UpdateColorResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;


                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdateColorMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ColorRecord.ID;

                var InternalCode = _CommandObj.Parameters.Add("@InternalCode", SqlDbType.NVarChar);
                InternalCode.Direction = ParameterDirection.Input;
                InternalCode.Value = RequestData.ColorRecord.InternalCode;

                var ColorCode = _CommandObj.Parameters.Add("@ColorCode", SqlDbType.NVarChar);
                ColorCode.Direction = ParameterDirection.Input;
                ColorCode.Value = RequestData.ColorRecord.ColorCode;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ColorRecord.Description;

                var Shade = _CommandObj.Parameters.Add("@Shade", SqlDbType.NVarChar);
                Shade.Direction = ParameterDirection.Input;
                Shade.Value = RequestData.ColorRecord.Shade;

                var NRFCode = _CommandObj.Parameters.Add("@NRFCode", SqlDbType.NVarChar);
                NRFCode.Direction = ParameterDirection.Input;
                NRFCode.Value = RequestData.ColorRecord.NRFCode;

                var Colors = _CommandObj.Parameters.Add("@Color", SqlDbType.Int);
                Colors.Direction = ParameterDirection.Input;
                Colors.Value = RequestData.ColorRecord.Colors;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ColorRecord.Active;


                var Attribute1 = _CommandObj.Parameters.Add("@Attribute1", SqlDbType.NVarChar);
                Attribute1.Direction = ParameterDirection.Input;
                Attribute1.Value = RequestData.ColorRecord.Attribute1;

                var Attribute2 = _CommandObj.Parameters.Add("@Attribute2", SqlDbType.NVarChar);
                Attribute2.Direction = ParameterDirection.Input;
                Attribute2.Value = RequestData.ColorRecord.Attribute2;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.ColorRecord.Remarks;

                var MultiColorImage = _CommandObj.Parameters.Add("@MultiColorImage", SqlDbType.NVarChar);
                MultiColorImage.Direction = ParameterDirection.Input;
                MultiColorImage.Value = RequestData.ColorRecord.MultiColorImage;      

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.ColorRecord.UpdateBy;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.ColorRecord.SCN;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Color");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Color");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Color");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Color Master");
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
            var StateColorRecord = new ColorMaster();
            var RequestData = (DeleteColorRequest)RequestObj;
            var ResponseData = new DeleteColorResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from ColorMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Color Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Color Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ColorRecord = new ColorMaster();
            var RequestData = (SelectByColorIDRequest)RequestObj;
            var ResponseData = new SelectByColorIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ColorMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objColorMasterRecord = new ColorMaster();
                        objColorMasterRecord.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objColorMasterRecord.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        objColorMasterRecord.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objColorMasterRecord.Description = Convert.ToString(objReader["Description"]);
                        objColorMasterRecord.Shade = Convert.ToString(objReader["Shade"]);
                        objColorMasterRecord.Remarks = Convert.ToString(objReader["Remarks"]);
                        objColorMasterRecord.NRFCode = Convert.ToString(objReader["NRFCode"]);
                        objColorMasterRecord.Colors = objReader["Colors"] != DBNull.Value ? Convert.ToInt32(objReader["Colors"]) : 0;
                        objColorMasterRecord.Attribute1 = Convert.ToString(objReader["Attribute1"]);
                        objColorMasterRecord.Attribute2 = Convert.ToString(objReader["Attribute2"]);
                        objColorMasterRecord.MultiColorImage = objReader["MultiColorImage"] != DBNull.Value ? Convert.ToString(objReader["MultiColorImage"]) : string.Empty;
                        objColorMasterRecord.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objColorMasterRecord.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objColorMasterRecord.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objColorMasterRecord.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objColorMasterRecord.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objColorMasterRecord.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.ColorRecord = objColorMasterRecord;
                        ResponseData.ResponseDynamicData = objColorMasterRecord;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Color Master");
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
            var ColorList = new List<ColorMaster>();
            var RequestData = (SelectAllColorRequest)RequestObj;
            var ResponseData = new SelectAllColorResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ColorMaster";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objColorMaster = new ColorMaster();
                        objColorMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objColorMaster.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        objColorMaster.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objColorMaster.Description = Convert.ToString(objReader["Description"]);
                        objColorMaster.Shade = Convert.ToString(objReader["Shade"]);
                        objColorMaster.NRFCode = Convert.ToString(objReader["NRFCode"]);
                        objColorMaster.Colors = objReader["Colors"] != DBNull.Value ? Convert.ToInt32(objReader["Colors"]) : 0;
                        objColorMaster.Attribute1 = Convert.ToString(objReader["Attribute1"]);
                        objColorMaster.Attribute2 = Convert.ToString(objReader["Attribute2"]);
                        objColorMaster.MultiColorImage = objReader["MultiColorImage"] != DBNull.Value ? Convert.ToString(objReader["MultiColorImage"]) : string.Empty;
                        objColorMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objColorMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objColorMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objColorMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objColorMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objColorMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ColorList.Add(objColorMaster);                      
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ColorList = ColorList;
                    ResponseData.ResponseDynamicData = ColorList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Color Master");
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

        public override SelectColorLookUpResponse SelectColorLookUp(SelectColorLookUpRequest ObjRequest)
        {
            var ColorList = new List<ColorMaster>();          
            var RequestData = (SelectColorLookUpRequest)ObjRequest;
            var ResponseData = new SelectColorLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID as ColorID,ColorCode,[Description] from ColorMaster with(NoLock)  where Active='true'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objColor = new ColorMaster();
                        objColor.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objColor.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objColor.Description = Convert.ToString(objReader["Description"]);
                        ColorList.Add(objColor);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ColorList = ColorList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Color Master");
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

        public override SelectAllColorResponse API_SelectALL(SelectAllColorRequest requestData)
        {
            var ColorList = new List<ColorMaster>();
            var RequestData = (SelectAllColorRequest)requestData;
            var ResponseData = new SelectAllColorResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from ColorMaster";

                string sSql = "Select ID, ColorCode, Description, Shade, NrfCode, Attribute1, Attribute2, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from ColorMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(CM.ID) As TOTAL_CNT From ColorMaster CM with(NoLock) " +
                   "where CM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or CM.ColorCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CM.Description like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CM.Shade like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CM.NrfCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CM.Attribute1 like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CM.Attribute2 like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ColorCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Description like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Shade like isnull('%" + RequestData.SearchString + "%','') " +
                       "or NrfCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Attribute1 like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Attribute2 like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objColorMaster = new ColorMaster();
                        objColorMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objColorMaster.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        objColorMaster.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objColorMaster.Description = Convert.ToString(objReader["Description"]);
                        objColorMaster.Shade = Convert.ToString(objReader["Shade"]);
                        objColorMaster.NRFCode = Convert.ToString(objReader["NRFCode"]);
                        //objColorMaster.Colors = objReader["Colors"] != DBNull.Value ? Convert.ToInt32(objReader["Colors"]) : 0;
                        objColorMaster.Attribute1 = Convert.ToString(objReader["Attribute1"]);
                        objColorMaster.Attribute2 = Convert.ToString(objReader["Attribute2"]);
                        //objColorMaster.MultiColorImage = objReader["MultiColorImage"] != DBNull.Value ? Convert.ToString(objReader["MultiColorImage"]) : string.Empty;
                        //objColorMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objColorMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objColorMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objColorMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objColorMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objColorMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ColorList.Add(objColorMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.ColorList = ColorList;
                    ResponseData.ResponseDynamicData = ColorList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Color Master");
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
