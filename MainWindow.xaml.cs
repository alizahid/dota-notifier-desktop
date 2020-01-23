using QRCoder;
using shortid;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Dota_Notifier
{
    public partial class MainWindow : Window
    {
        private const string VERSION = "v1.0.0";

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

            ActiveWindow.Listen();

            SetUpDelaySelector();

            version.Text = VERSION;
        }

        private string GetId()
        {
            string id = Storage.Get("id");

            if (id == null)
            {
                id = ShortId.Generate(true);

                Storage.Put("id", id);
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

        private void SetUpDelaySelector()
        {
            string delay = Storage.Get("delay", "1500");

            foreach (ComboBoxItem item in delaySelector.Items)
            {
                Console.WriteLine("item: " + item.Content.ToString());

                if (item.Content.ToString().IndexOf(delay) == 0)
                {
                    item.IsSelected = true;
                }
            }

            delaySelector.SelectionChanged += (o, e) =>
            {
                string newDelay = new string(delaySelector.SelectedItem.ToString().Where(c => char.IsDigit(c)).ToArray());

                Storage.Put("delay", newDelay);
            };
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
