using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ItemTypeMasterRequest;
using EasyBizResponse.Masters.ItemTypeMasterResponse;
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
    public class ItemTypeMasterDAL : BaseItemTypeMasterDAL
    {


        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
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
            var ItemTypeMasterTypessMaster = new ItemTypeMasterTypes();
            var RequestData = (SelectByIDItemTypeRequest)RequestObj;
            var ResponseData = new SelectByIDItemTypeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = "Select * from ItemTypeMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objItemTypeMasterTypes = new ItemTypeMasterTypes();
                        objItemTypeMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objItemTypeMasterTypes.ItemTypeCode = Convert.ToString(objReader["ItemTypeCode"]);
                        objItemTypeMasterTypes.ItemTypeName = Convert.ToString(objReader["ItemTypeName"]);

                        objItemTypeMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objItemTypeMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objItemTypeMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objItemTypeMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objItemTypeMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objItemTypeMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.ItemTypeMasterTypesData = objItemTypeMasterTypes;
                        ResponseData.ResponseDynamicData = objItemTypeMasterTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
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
            var ItemTypeMasterTypes = new List<ItemTypeMasterTypes>();
            var RequestData = (SelectAllItemTypeMasterRequest)RequestObj;
            var ResponseData = new SelectAllItemTypeMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ItemTypeMaster with(NoLock)";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objItemTypeMasterTypes = new ItemTypeMasterTypes();
                        objItemTypeMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objItemTypeMasterTypes.ItemTypeCode = Convert.ToString(objReader["ItemTypeCode"]);
                        objItemTypeMasterTypes.ItemTypeName = Convert.ToString(objReader["ItemTypeName"]);

                        objItemTypeMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objItemTypeMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objItemTypeMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objItemTypeMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objItemTypeMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objItemTypeMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ItemTypeMasterTypes.Add(objItemTypeMasterTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ItemTypeMasterTypesList = ItemTypeMasterTypes;
                    ResponseData.ResponseDynamicData = ItemTypeMasterTypes;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Settings");
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
    }
}
