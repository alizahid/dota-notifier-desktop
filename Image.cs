using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dota_Notifier
{
    public class Image
    {
        public static ImageSource ConvertBitmapToImageSource(Bitmap image)
        {
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);

            BitmapData data = image.LockBits(
                rectangle,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );

            try
            {
                int size = (rectangle.Width * rectangle.Height) * 4;

                return BitmapSource.Create(
                    image.Width,
                    image.Height,
                    image.HorizontalResolution,
                    image.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    data.Scan0,
                    size,
                    data.Stride
                );
            }
            finally
            {
                image.UnlockBits(data);
            }
        }
    }
}
