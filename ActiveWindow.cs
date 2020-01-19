using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace Dota_Notifier
{
    public class ActiveWindow
    {
        private static WinEventDelegate @delegate;

        public static void Listen()
        {
            @delegate = new WinEventDelegate(WinEventProc);

            SetWinEventHook(3, 3, IntPtr.Zero, @delegate, 0, 0, 0 | 2); // flags: http://www.pinvoke.net/default.aspx/user32.setwineventhook
        }

        public static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr handle, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            const int chars = 256;

            StringBuilder stringBuilder = new StringBuilder(chars);

            if (GetWindowText(handle, stringBuilder, chars) > 0)
            {
                if (stringBuilder.ToString() == "Dota 2")
                {
                    System.Threading.Thread.Sleep(1500);

                    IntPtr window = GetForegroundWindow();

                    Rect rect = new Rect();

                    GetWindowRect(window, ref rect);

                    Rectangle bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

                    Screenshot.Check(bounds);
                }
            }
        }

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr handle, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr handle, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowRect(IntPtr handle, ref Rect rect);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
