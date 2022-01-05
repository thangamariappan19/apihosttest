using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Language
{
  public interface ILanguageView:IBaseView
    {
        long ID { get; set; }
        string LanguageCode { get; set; }
        string LanguageName { get; set; }
        string Remarks { get; set; }
        bool Active { get; set; }
        List<LanguageMaster> LanguageMasterList { get; set; }
    }
}
