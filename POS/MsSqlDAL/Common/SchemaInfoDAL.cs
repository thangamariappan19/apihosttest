using EasyBizAbsDAL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.DataBaseSchema;
using EasyBizRequest;
using EasyBizRequest.DBSchemaRequest;
using EasyBizResponse;
using EasyBizResponse.DBSchemaReponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Common
{
    public class SchemaInfoDAL : BaseSchemaInfoDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; 
        Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override DataBaseInfoResponse GetDataBaseInfo(DataBaseInfoRequest RequestObj)
        {
            List<DataBaseInfo> DataBaseInfoList = new List<DataBaseInfo>();
            DataBaseInfoRequest RequestData = (DataBaseInfoRequest)RequestObj;
            DataBaseInfoResponse ResponseData = new DataBaseInfoResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SELECT * FROM sys.databases");
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        DataBaseInfo objDataBaseInfo = new DataBaseInfo();
                        objDataBaseInfo.ID = Convert.ToInt32(objReader["database_id"]);
                        objDataBaseInfo.DataBaseName = Convert.ToString(objReader["name"]);
                        DataBaseInfoList.Add(objDataBaseInfo);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DataBaseInfoList = DataBaseInfoList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DataBase Info");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override TableInfoByDBResponse GetTableInfo(TableInfoByDBRequest RequestObj)
        {
            List<TableInfo> TableInfoList = new List<TableInfo>();
            TableInfoByDBRequest RequestData = (TableInfoByDBRequest)RequestObj;
            TableInfoByDBResponse ResponseData = new TableInfoByDBResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SELECT * FROM " + RequestData.DbName + ".INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = N'" + RequestData.DbName + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        TableInfo objTableInfo = new TableInfo();
                        objTableInfo.TABLE_NAME = Convert.ToString(objReader["TABLE_NAME"]);
                        objTableInfo.TABLE_CATALOG = Convert.ToString(objReader["TABLE_CATALOG"]);
                        TableInfoList.Add(objTableInfo);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TableInfoList = TableInfoList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Schema Info");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override ColumnInfoByTableResponse GetColumnInfo(ColumnInfoByTableRequest RequestObj)
        {
            List<ColumnInfo> SchemaInfoList = new List<ColumnInfo>();
            ColumnInfoByTableRequest RequestData = (ColumnInfoByTableRequest)RequestObj;
            ColumnInfoByTableResponse ResponseData = new ColumnInfoByTableResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SELECT * FROM " + RequestData.DbName + ".INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + RequestData.TableName + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        ColumnInfo objSchemaInfo = new ColumnInfo();
                        objSchemaInfo.COLUMN_NAME = Convert.ToString(objReader["COLUMN_NAME"]);
                        objSchemaInfo.DATA_TYPE = Convert.ToString(objReader["DATA_TYPE"]);
                        objSchemaInfo.EXCEL_COLUMN = string.Empty;
                        SchemaInfoList.Add(objSchemaInfo);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ColumnInfoList = SchemaInfoList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Schema Info");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }
        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
