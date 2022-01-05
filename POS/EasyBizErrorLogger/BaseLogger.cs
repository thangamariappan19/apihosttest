using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizErrorLogger
{
    public abstract class BaseLogger
    {
        public abstract LogResponse WriteToLog(LogRequest LogRequestObj);
        public abstract LogResponse WriteToSyncLog(LogRequest LogRequestObj); 
    }
}
