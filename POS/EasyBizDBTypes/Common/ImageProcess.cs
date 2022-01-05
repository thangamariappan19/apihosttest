using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EasyBizDBTypes.Common
{
    public class DataBaseImageProcess
    {
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image image = null;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                image = Image.FromStream(ms, true);
            }
            catch
            {

            }
            return image;
        }
        public dynamic GetImageStream(Image myImage)
        {
            BitmapSource bitmapSource = null;
            try
            {                
                if (myImage != null)
                {
                    var bitmap = new Bitmap(myImage);
                    IntPtr bmpPt = bitmap.GetHbitmap();
                    bitmapSource =
                     System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                           bmpPt,
                           IntPtr.Zero,
                           Int32Rect.Empty,
                           BitmapSizeOptions.FromEmptyOptions());

                    //freeze bitmapSource and clear memory to avoid memory leaks
                    bitmapSource.Freeze();
                    DeleteObject(bmpPt);
                }
            }
            catch
            {
                bitmapSource = null;
            }
            return bitmapSource;
        }
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);
    }
}
