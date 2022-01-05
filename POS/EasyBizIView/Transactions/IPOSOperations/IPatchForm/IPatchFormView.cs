using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Transactions.IPOSOperations.IPatchForm
{
    public interface IPatchFormView : IBaseView
    {
      
         int ID { get; set; }      
         String ApplicationType { get; set; }       
         String ApplicationVersion { get; set; }      
         String DBVersion { get; set; }
         Byte[] AppPatchFile { get; set; }
        String Extension { get; set; }

    }
}
