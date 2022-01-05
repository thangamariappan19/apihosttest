using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDocumentType
{
   public interface IDocumentTypeView:IBaseView
    {
        int ID { get; set; }
        string DocumentCode { get; set; }
        string DocumentName { get; set; }
        string Description { get; set; }
        List<DocumentTypes> DocumentTypeList { get; set; }
        bool Active { get; set; }
    }
}
