using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
    public static class DocumnetNoGenerator
    {
        public static string ToDocumentNo(this string DocNum, string Prefix, string Suffix, int length, int StartNo, long EndNo, long RunningNo)
        {
            string DocumetNo = string.Empty;
            try
            {
                //string Base = Prefix + RunningNo + Suffix;
                string Base = Prefix + Suffix+ RunningNo  ;
                int RemainingLength = length - Base.Length;

                if (Base.Length <= length)
                {
                    string DummyDynamicString = string.Empty;
                    string DynamicString = DummyDynamicString.PadLeft(RemainingLength, '0');

                    if (DynamicString != string.Empty)
                    {
                        DocumetNo = Prefix + Suffix + DynamicString + RunningNo;                        
                    }
                    else
                    {
                        DocumetNo = Prefix + Suffix + RunningNo;
                    }
                }
                else
                {
                    DocumetNo = Prefix + Suffix + RunningNo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DocumetNo;
        }
    }
}
