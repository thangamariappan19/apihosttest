using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDAL
{
    public class OracleSqlCommon
    {
        public string OracleConString = ConfigurationManager.AppSettings["OracleConString"];
        public void InitializeDataComponents(ref OracleConnection ConnectionObj, ref OracleCommand CommandObj)
        {
            ConnectionObj = new  OracleConnection();
            CommandObj = new OracleCommand();            

            ConnectionObj.ConnectionString = OracleConString;

            if (ConnectionObj.State == System.Data.ConnectionState.Closed)
            {
                ConnectionObj.Open();
            }
            CommandObj.Connection = ConnectionObj;
        }

        public void CloseConnection(OracleConnection ConnectionObj)
        {
            if (ConnectionObj.State == System.Data.ConnectionState.Open)
            {
                ConnectionObj.Close();
            }
        }

        public string GetSQLServerDateString(DateTime DateValue)
        {
            return DateValue.ToString("yyyy-MM-dd");
        }

        public string GetSQLServerDateTimeString(DateTime DateValue)
        {
            return DateValue.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public DateTime SetSQLServerDateTimeString(DateTime DateValue)
        {
            return Convert.ToDateTime(DateValue.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
