using EasyBizDBTypes.Settings;
using MsSqlDAL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Settings
{
    public class DBConnectionsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string ConnectionString;
        public List<DBConnection> DBConnectionList()
        {
            var ConnectionList = new List<DBConnection>();
         
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref ConnectionString);

                string sSql = "Select * from DBConnections ";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDBConnections = new DBConnection();
                        objDBConnections.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDBConnections.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objDBConnections.ConnectionType = Convert.ToString(objReader["ConnectionType"]);
                        objDBConnections.ConnectionString = Convert.ToString(objReader["ConnectionString"]);
                        ConnectionList.Add(objDBConnections);
                    }                  
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ConnectionList;
        }
    }
}
