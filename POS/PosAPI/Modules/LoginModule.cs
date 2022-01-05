using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PosAPI.Modules
{
    public class LoginModule
    {
        public string GetConnectionString()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["db_connection"];
            return value;
            //return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public DateTime BusinessDate(int storeid)
        {
            DateTime Date;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = CommonModule.GetConnectionString();
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select Top 1 BusinessDate from ShiftLog where StoreID="+storeid + " Order by ID desc";
                    Date = Convert.ToDateTime(command.ExecuteScalar());                    
                }
            }
            return Date;
        }
        public LoginDTO Login(string user_name, string password)
        {
            LoginDTO User = null;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = CommonModule.GetConnectionString();
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "Select * from UserMaster where Active = 1 and UserName = '" + user_name + "' " +
                            "and Password = '" + password + "'";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    User = new LoginDTO()
                                    {
                                        EmployeeCode = reader["EmployeeCode"] == DBNull.Value ? "" : reader["EmployeeCode"].ToString(),
                                        EmployeeID = reader["EmployeeID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["EmployeeID"]),
                                        ID = reader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID"]),
                                        RoleCode = reader["RoleCode"] == DBNull.Value ? "" : reader["RoleCode"].ToString(),
                                        RoleID = reader["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RoleID"]),
                                        UserCode = reader["UserCode"] == DBNull.Value ? "" : reader["UserCode"].ToString(),
                                        UserName = reader["UserName"] == DBNull.Value ? "" : reader["UserName"].ToString(),
                                        Password = reader["Password"] == DBNull.Value ? "" : reader["Password"].ToString()
                                    };
                                    break;
                                }
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return User;
        }
    }
}