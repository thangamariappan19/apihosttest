using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Masters.DesignMasterResponse;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.ScaleMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseStyleMasterDAL : BaseDAL
    {
       public abstract SelectScaleDetailsResponse SelectStyleWithScaleDetails(SelectScaleDetailsRequest ObjRequest);
       public abstract SelectColorDetailsResponse SelectStyleWithColorDetails(SelectColorDetailsRequest ObjRequest);
       public abstract SelectStyleLookUpResponse SelectStyleLookUp(SelectStyleLookUpRequest ObjRequest);
       public abstract SelectItemImageResponse SelectStyleWithItemImageDetails(SelectItemImageRequest ObjRequest);
       public abstract StyleCodeGeneratingResponse SelectStyleCode(StyleCodeGeneratingRequest ObjRequest);
       public abstract SelectStyleWithScaleforStockResponse SelectStyleWithScaleForStock(SelectStyleWithScaleforStockRequest ObjRequest);
       public abstract SaveStyleResponse ImportExcelStyleInsert(SaveStyleRequest ObjRequest);

       public abstract SelectDesignDevelopmentOfficeLookUpResponse SelectDesignDevelopmentOfficeLookUp(SelectDesignDevelopmentOfficeLookUpRequest ObjRequest);

       public abstract SelectDesignGradeLookUpResponse SelectDesignGradeLookUp(SelectDesignGradeLookUpRequest ObjRequest);
       //public abstract SaveStyleResponse ImportExcelStyleWithColorInsert(SaveStyleRequest ObjRequest);
       //public abstract SaveStyleResponse ImportExcelStyleWithScaleInsert(SaveStyleRequest ObjRequest);

       public abstract InserSKUImageResponse InsertSKUImages(InserSKUImageRequest ObjRequest);
       public abstract GetStyleNameResponse GetStyleName(GetStyleNameRequest objRequest);

        public abstract SelectAllStyleResponse API_SelectAll(SelectAllStyleRequest objRequest);

        public abstract SelectAllStyleResponse API_GetStyleColorScale(SelectAllStyleRequest objRequest);
        public abstract SelectItemImageResponse SelectStyleWithItemImageDetailsNew(SelectItemImageRequest ObjRequest);
        public abstract SelectScaleDetailsResponse SelectStyleWithScaleDetailsNew(SelectScaleDetailsRequest ObjRequest);
        public abstract SelectColorDetailsResponse SelectStyleWithColorDetailsNew(SelectColorDetailsRequest ObjRequest);


    }
}
