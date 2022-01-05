using EasyBizAbsDAL;
using EasyBizAbsDAL.Common;
using MsSqlDAL;
using MsSqlDAL.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizFactory
{
    public class DALFactory
    {
        public BaseDALRepository GetDALRepository()
        {
            BaseDALRepository tempObj = null;
            string DB = ConfigurationManager.AppSettings["BackendDB"];

            //return the DB specific DAL based on configuration...MSSQL for now
            if (DB == "MSSQL")
            {
                tempObj = new MsSqlDALRepository();
                //tempObj.SetLogger(GetLogger());
            }
            //if (DB == "ORACLE")
            //{
            //    tempObj = new OracleDALRepository();
            //    //tempObj.SetLogger(GetLogger());
            //}
            return tempObj;

        }
    }
}
