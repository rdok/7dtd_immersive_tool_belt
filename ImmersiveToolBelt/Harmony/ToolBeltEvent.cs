using System;

namespace ImmersiveToolBelt.Harmony
{
    public static class ToolBeltEvent
    {
        private static bool _isBackpackOnOpen;
        private static bool _isBackpackOnClose;
        public static DateTime ChangedAt { get; private set; } = DateTime.Now;

        public static bool BackpackOnOpen
        {
            get => _isBackpackOnOpen;
            set
            {
                _isBackpackOnOpen = value;
                _isBackpackOnClose = !value;
                Trigger();
            }
        }

        public static bool BackpackOnClose
        {
            get => _isBackpackOnClose;
            set
            {
                _isBackpackOnClose = value;
                _isBackpackOnOpen = !value;
                Trigger();
            }
        }

        public static void Trigger()
        {
            ToolBeltEvent.ChangedAt = DateTime.Now;
        }

        public static void Dispose()
        {
            ChangedAt = DateTime.MinValue;
        }

        public static bool IsAlive()
        {
            return ToolBeltEvent.ChangedAt != DateTime.MinValue;
        }
    }
}