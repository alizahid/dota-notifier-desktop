using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Dota_Notifier
{
    public class Screenshot
    {
        public static void TakeScreenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

            bitmap.Save("dota2.jpg", ImageFormat.Jpeg);

            bitmap.Dispose();
            graphics.Dispose();
        }
    }
}
