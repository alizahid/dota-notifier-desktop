using QRCoder;
using shortid;
using System.Drawing;
using System.Windows;
using Point = System.Windows.Point;

namespace Dota_Notifier
{
    public partial class MainWindow : Window
    {
        private readonly System.Windows.Forms.NotifyIcon NotifyIcon = new System.Windows.Forms.NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();

            NotifyIcon.Icon = new Icon("assets/dota2.ico");
            NotifyIcon.Click += ShowApp;
            NotifyIcon.Visible = true;

            Init();
        }

        private void Init()
        {
            Storage storage = new Storage();

            string id = storage.Get("id");

            if (id == null)
            {
                id = ShortId.Generate(true);

                storage.Put("id", id);
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap image = qrCode.GetGraphic(20, "#F44336", "#000");

            QrCode.Source = Image.ConvertBitmapToImageSource(image);

            image.Dispose();
        }

        private void ShowApp(object sender, System.EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Normal;

            this.Activate();
        }

        private void HideApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.ShowInTaskbar = false;
            this.WindowState = WindowState.Minimized;
        }

        private void CloseApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
