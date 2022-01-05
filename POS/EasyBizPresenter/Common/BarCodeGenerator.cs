using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Common
{
    public static class CodeGenerator
    {
        public static string ToSKUBarCode(this string Input)
        {
            string BarCode = string.Empty;
            try
            {
                string Base = DateTime.Now.ToString("ddMMyy");
                if (Input.Length <= 7)
                {
                    string DynamicNo = Input.PadLeft(7, '0');
                    BarCode = Base + DynamicNo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BarCode;
        }        
    }
}
