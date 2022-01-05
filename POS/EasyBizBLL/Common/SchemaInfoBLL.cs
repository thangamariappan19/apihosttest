using EasyBizDBTypes.Common;
using EasyBizFactory;
using EasyBizRequest.DBSchemaRequest;
using EasyBizResponse.DBSchemaReponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
    public class SchemaInfoBLL
    {
        public DataBaseInfoResponse GetDataBaseInfo(DataBaseInfoRequest objRequest)
        {
            DataBaseInfoResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSchemaInfoDAL = objFactory.GetDALRepository().GetSchemaInfoDAL();
                objResponse = (DataBaseInfoResponse)objSchemaInfoDAL.GetDataBaseInfo(objRequest);               
            }
            catch (Exception ex)
            {
                objResponse = new DataBaseInfoResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "GetDataBaseInfo");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public TableInfoByDBResponse GetTableInfo(TableInfoByDBRequest objRequest)
        {
            TableInfoByDBResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSchemaInfoDAL = objFactory.GetDALRepository().GetSchemaInfoDAL();
                objResponse = (TableInfoByDBResponse)objSchemaInfoDAL.GetTableInfo(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new TableInfoByDBResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "GetTableInfo");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public ColumnInfoByTableResponse GetColumnInfo(ColumnInfoByTableRequest objRequest)
        {
            ColumnInfoByTableResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objSchemaInfoDAL = objFactory.GetDALRepository().GetSchemaInfoDAL();
                objResponse = (ColumnInfoByTableResponse)objSchemaInfoDAL.GetColumnInfo(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new ColumnInfoByTableResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "GetColumnInfo");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
