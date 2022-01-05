using CommonRoutines;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Transactions.IPOS;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
    public class ItemSearchPresenter
    {
        IItemSearchView _IItemSearchView;
        public ItemSearchPresenter(IItemSearchView ViewObj)
        {
            _IItemSearchView = ViewObj;
        }
        public void GetSKUList()
        {
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.Count = 1;
                RequestData.RequestFrom = _IItemSearchView.RequestFrom;
                RequestData.SearchString = _IItemSearchView.SearchString;


                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var SKUList = new List<SKUMasterTypes>();
                    foreach (SKUMasterTypes objSKUMasterTypes in ResponseData.SKUMasterTypesList)
                    {
                        SKUMasterTypes TempSKUMasterTypes = new SKUMasterTypes();
                        TempSKUMasterTypes = DeepCopyCreator.SKUMasterDeepCopy(objSKUMasterTypes);
                        TempSKUMasterTypes.StylePrice = GetStylePricingBySKUCode(objSKUMasterTypes.SKUCode);
                        SKUList.Add(TempSKUMasterTypes);
                    }

                    if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
                    {
                        _IItemSearchView.DefaultSKUList = SKUList;
                    }
                    else
                    {
                        _IItemSearchView.SearchSKUList = SKUList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Decimal GetStylePricingBySKUCode(string SKUCode)
        {
            Decimal StylePrice = 0;
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new GetStylePricingBySKUCodeRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.SKUCode = SKUCode;

                var ResponseData = _SKUMasterBLL.SelectGetStylePricingBySKUCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    var StylePricingList = ResponseData.StylePricingList;

                    if (StylePricingList != null && StylePricingList.Count > 0)
                    {
                        var StylPriceData = StylePricingList.Where(x => x.PriceListID == _IItemSearchView.UserInformation.PriceListID).FirstOrDefault();

                        if (StylPriceData != null)
                        {
                            StylePrice = StylPriceData.Price;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StylePrice = 0;
            }
            return StylePrice;
        }
    }
}
