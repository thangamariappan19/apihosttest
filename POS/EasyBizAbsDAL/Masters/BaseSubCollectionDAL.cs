using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using EasyBizResponse.Masters.SubCollectionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseSubCollectionDAL:BaseDAL
    {


       public abstract SelectSubCollectionLookUpResponse SelectSubCollectionLookUp(SelectSubCollectionLookUpRequest ObjRequest);

       public abstract SelectSubCollectionListForCollectionResponse SelectSubCollectionByCollection(SelectSubCollectionListForCollectionRequest RequestObj);
       public abstract SelectAllSubCollectionResponse SelectAllSubCollectionDetails(SelectAllSubCollectionRequest RequestObj);
        public abstract SelectAllSubCollectionResponse API_SelectALL(SelectAllSubCollectionRequest requestData);
    }
}
