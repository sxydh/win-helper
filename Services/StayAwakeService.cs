using System.Runtime.InteropServices;

namespace win_helper.Services
{
    public class StayAwakeService : BaseService
    {

        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

        private readonly object _lock = new();
        private int _ifActive = 0;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint SetThreadExecutionState([In] uint esFlags);

        public override void Active()
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

        public override void Inactive()
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
