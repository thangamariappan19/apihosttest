using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Common
{
   public class AutoUpdateTypes
    {
        public int ID { get; set; }

        public int StoreID { get; set; }

        public int PosID { get; set; }

        public string DbVersion { get; set; }

        public string AppVersion { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
