using EasyBizRequest.DBSchemaRequest;
using EasyBizResponse.DBSchemaReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Common
{
    public abstract class BaseSchemaInfoDAL : BaseDAL
    {
        public abstract DataBaseInfoResponse GetDataBaseInfo(DataBaseInfoRequest RequestObj);

        public abstract TableInfoByDBResponse GetTableInfo(TableInfoByDBRequest RequestObj);

        public abstract ColumnInfoByTableResponse GetColumnInfo(ColumnInfoByTableRequest RequestObj);
    }
}
