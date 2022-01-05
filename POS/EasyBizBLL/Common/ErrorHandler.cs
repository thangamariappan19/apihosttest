using EasyBizErrorLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
    public class ErrorHandler
    {
        private BaseLogger _Logger;
        public void SetLogger(BaseLogger Logger)
        {
            _Logger = Logger;
        }
        public BaseLogger GetLogger()
        {
            //create the logger based on Configuration...Text File logger for now
            BaseLogger tempObj = new TextFileLogger();

            return tempObj;
        }
        public LogResponse WriteToLog(string Source, string Message)
        {
            LogRequest requestObj = new LogRequest();
            requestObj.Source = Source;
            requestObj.Message = Message;
            return _Logger.WriteToLog(requestObj);
        }
    }
}
