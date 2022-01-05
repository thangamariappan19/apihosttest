using EasyBizDBTypes.DataBaseSchema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.SyncSettings
{
    public interface IDataCompareView : IBaseView
    {
        //List<DataBaseInfo> DataBaseInfoList { get; set; }
        List<TableInfo> TableInfoList { get; set; }
        List<ColumnInfo> ColumnInfoList { get; set; }
        string BaseConnectionString { get; }
        string ToConnectionString { get; }
        DataTable DifferData { get; set; }
        string DbName { get; }
        string TableName { get; }
    }
}
