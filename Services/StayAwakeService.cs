using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace win_helper.Services
{
    internal class StayAwakeService
    {

        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

        private static readonly object _lock = new();
        private static int _ifActive = 0;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint SetThreadExecutionState([In] uint esFlags);

        public static void Active()
        {
            lock (_lock)
            {
                if (_ifActive == 1)
                {
                    return;
                }
                SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
                _ifActive = 1;
            }
        }

        public static void Inactive()
        {
            lock (_lock)
            {
                if (_ifActive == 0)
                {
                    return;
                }
                SetThreadExecutionState(ES_CONTINUOUS);
                _ifActive = 0;
            }
        }

    }
}
