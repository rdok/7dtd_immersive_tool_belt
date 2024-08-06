namespace ImmersiveToolBelt.Harmony
{
    public static class ToolBeltEvent
    {
        private static bool _isBackpackOnOpen;
        private static bool _isBackpackOnClose;
        public static bool SlotChanged { get; set; }

        public static bool BackpackOnOpen
        {
            get => _isBackpackOnOpen;
            set
            {
                _isBackpackOnOpen = value;
                _isBackpackOnClose = !value;
            }
        }

        public static bool BackpackOnClose
        {
            get => _isBackpackOnClose;
            set
            {
                _isBackpackOnClose = value;
                _isBackpackOnOpen = !value;
            }
        }
    }
}