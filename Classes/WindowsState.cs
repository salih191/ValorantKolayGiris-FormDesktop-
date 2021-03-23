using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ValorantKolayGiris_FormDesktop_.Classes
{
    public class WindowsState
    {
        public static bool Durum(string uygulama)
        {
            Process[] processes = Process.GetProcesses();
            if (EkrandaMi(uygulama, processes)) return true;

            //if (EkranaGetir(uygulama, processes))
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        SendKeys.Send("{TAB}");
            //    }

            //    return true;
            //}
            return EkranaGetir(uygulama, processes);
        }
        private static bool EkrandaMi(string uygulama, Process[] processes)
        {
            foreach (Process p in processes)
            {
                if (p.ProcessName.IndexOf(uygulama) >= 0)
                {
                    IntPtr app_hwnd;
                    WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                    app_hwnd = p.MainWindowHandle;
                    GetWindowPlacement(app_hwnd, ref wp);

                    if (wp.showCmd == 1)
                    {
                        int x= wp.rcNormalPosition.left+65;
                        int y = wp.rcNormalPosition.top+185;
                        Cursor.Position = new Point(x, y);
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool EkranaGetir(string uygulama, Process[] processes)
        {
            foreach (Process p in processes)
            {
                if (p.ProcessName.IndexOf(uygulama) >= 0)
                {
                    IntPtr app_hwnd;
                    WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                    app_hwnd = p.MainWindowHandle;
                    GetWindowPlacement(app_hwnd, ref wp);
                    wp.showCmd = 1;
                    SetWindowPlacement(app_hwnd, ref wp);
                }
            }

            if (EkrandaMi(uygulama,processes))
            {
                return true;
            }

            return false;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetWindowPlacement(IntPtr hWnd,
            ref WINDOWPLACEMENT lpwndpl);

        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINTAPI ptMinPosition;
            public POINTAPI ptMaxPosition;
            public RECT rcNormalPosition;
        }

    }
}