using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Language
{
   public interface ILanguageList:IBaseView
    {
       List<LanguageMaster> LanguageMasterList { get; set; }
    }
}
