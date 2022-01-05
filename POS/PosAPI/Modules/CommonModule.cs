using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PosAPI.Modules
{
    public static class CommonModule
    {
        public static string GetConnectionString()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["db_connection"];
            return EncrypterDecrypter.Decrypt(value);
            //return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}