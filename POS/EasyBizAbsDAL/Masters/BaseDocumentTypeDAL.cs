using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizResponse.Masters.DocumentTypeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
  public abstract  class BaseDocumentTypeDAL:BaseDAL
    {
      public abstract SelectDocumentLookUpResponse SelectDocumentLookUp(SelectDocumentLookUpRequest ObjRequest);
    }
}
