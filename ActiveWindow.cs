using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Dota_Notifier
{
    public class ActiveWindow
    {
        private static WinEventDelegate @delegate;

        private static Func<string, string> listener;

        public static void Listen(Func<string, string> listener)
        {
            ActiveWindow.listener = listener;

            @delegate = new WinEventDelegate(WinEventProc);

            SetWinEventHook(3, 3, IntPtr.Zero, @delegate, 0, 0, 0 | 2); // flags: http://www.pinvoke.net/default.aspx/user32.setwineventhook
        }

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            const int chars = 256;

            StringBuilder stringBuilder = new StringBuilder(chars);

            if (GetWindowText(hwnd, stringBuilder, chars) > 0)
            {
                listener.DynamicInvoke(stringBuilder.ToString());
            }
        }
    }
}
