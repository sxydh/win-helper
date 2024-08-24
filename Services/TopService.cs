using System.Runtime.InteropServices;

namespace win_helper.Services
{
    internal class TopService : BaseService
    {

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOPMOST = 0x00000008;

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;

        private IntPtr _hookId = IntPtr.Zero;

        private readonly FnDelegate _fnDelegate;

        public TopService()
        {
            _fnDelegate = Fn;
        }

        public override void Active()
        {
            if (_hookId != IntPtr.Zero)
            {
                return;
            }
            _hookId = SetWindowsHookEx(WH_KEYBOARD_LL, _fnDelegate, IntPtr.Zero, 0);
        }

        public override void Inactive()
        {
            if (_hookId == IntPtr.Zero)
            {
                return;
            }
            if (UnhookWindowsHookEx(_hookId))
            {
                _hookId = IntPtr.Zero;
            }
        }

        private delegate IntPtr FnDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr Fn(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                bool ctrl = (Control.ModifierKeys & Keys.Control) != 0;
                bool shift = (Control.ModifierKeys & Keys.Shift) != 0;
                bool alt = (Control.ModifierKeys & Keys.Alt) != 0;

                if (ctrl && shift && alt && vkCode == (int)Keys.T)
                {
                    IntPtr hWnd = GetForegroundWindow();
                    int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
                    if ((exStyle & WS_EX_TOPMOST) == 0)
                    {
                        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
                    }
                    else
                    {
                        SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
                    }
                }
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, FnDelegate lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    }

}
