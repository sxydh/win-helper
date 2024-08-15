using System.Runtime.InteropServices;

namespace win_helper.Services
{
    internal class StayAwakeService
    {

        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint SetThreadExecutionState([In] uint esFlags);

        public void Active()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
        }

    }
}
