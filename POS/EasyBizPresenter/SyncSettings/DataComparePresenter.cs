using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizIView.SyncSettings;
using EasyBizRequest.DBSchemaRequest;
using EasyBizResponse.DBSchemaReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.SyncSettings
{
    public class DataComparePresenter
    {
        IDataCompareView _IDataCompareView;
        public DataComparePresenter(IDataCompareView ViewObj)
        {
            _IDataCompareView = ViewObj;
        }
        public void GetTables()
        {
            try
            {
                var _SchemaInfoBLL = new SchemaInfoBLL();
                var RequestData = new TableInfoByDBRequest();
                var ResponseData = new TableInfoByDBResponse();
                RequestData.DbName = _IDataCompareView.DbName;
                ResponseData = _SchemaInfoBLL.GetTableInfo(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDataCompareView.TableInfoList = ResponseData.TableInfoList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetColumns()
        {
            try
            {
                var _SchemaInfoBLL = new SchemaInfoBLL();
                var RequestData = new ColumnInfoByTableRequest();
                var ResponseData = new ColumnInfoByTableResponse();
                RequestData.DbName = _IDataCompareView.DbName;
                RequestData.TableName = _IDataCompareView.TableName;
                ResponseData = _SchemaInfoBLL.GetColumnInfo(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDataCompareView.ColumnInfoList = ResponseData.ColumnInfoList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
