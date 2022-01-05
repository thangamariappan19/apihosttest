using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDocumentType
{
   public interface IDocumentList:IBaseView
    {
       List<DocumentTypes> DocumentTypeList { get; set; }
    } 
}
