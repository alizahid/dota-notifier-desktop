using QRCoder;
using shortid;
using System.Drawing;
using System.Windows;

namespace Dota_Notifier
{
    public partial class MainWindow : Window
    {
        private readonly System.Windows.Forms.NotifyIcon NotifyIcon = new System.Windows.Forms.NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();

            Left = SystemParameters.WorkArea.Width - Width - 20;
            Top = SystemParameters.WorkArea.Height - Height - 20;

            NotifyIcon.Icon = new Icon("assets/dota2.ico");
            NotifyIcon.Click += ShowApp;
            NotifyIcon.Visible = true;

            string id = GetId();

            GenerateQR(id);

            // ActiveWindow.Listen(title => title);
        }

        private string GetId()
        {
            Storage storage = new Storage();

            string id = storage.Get("id");

            if (id == null)
            {
                id = ShortId.Generate(true);

                storage.Put("id", id);
            }

            return id;
        }

        private void GenerateQR(string id)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap image = qrCode.GetGraphic(20, "#F44336", "#000");

            QrCode.Source = Image.ConvertBitmapToImageSource(image);

            image.Dispose();
        }

        private void ShowApp(object sender, System.EventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = WindowState.Normal;

            Activate();
        }

        private void HideApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowInTaskbar = false;
            WindowState = WindowState.Minimized;
        }

        private void CloseApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
