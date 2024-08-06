using System;

namespace ImmersiveToolBelt.Harmony
{
    public static class ToolBeltEvent
    {
        public static DateTime CreatedAt;
        public static bool BackpackOnOpen;
        public static bool BackpackOnClose;
        public static bool ItemHeldEvent;
        private static readonly ILogger Logger = new Logger();

        public static void DispatchBackpackOnClose()
        {
            CreatedAt = DateTime.Now;
            BackpackOnClose = true;
            BackpackOnOpen = false;
        }

        public static bool HasHideEvent()
        {
            return CreatedAt != DateTime.MinValue;
        }

        public static bool HasBackpackEvent()
        {
            return BackpackOnOpen;
        }

        public static void DispatchBackpackOnOpen()
        {
            BackpackOnOpen = true;
            BackpackOnClose = false;
        }

        public static void ResetShowEvents()
        {
            ItemHeldEvent = false;
        }

        public static bool HasBackpackOnOpenEvent()
        {
            return BackpackOnOpen;
        }

        public static bool SlotChangedEvent()
        {
            return ItemHeldEvent;
        }

        public static void DispatchSlotChangedEvent()
        {
            ItemHeldEvent = true;
            CreatedAt = DateTime.Now;
        }

        public static void ResetHideEvents()
        {
            CreatedAt = DateTime.MinValue;
        }

        public static bool HasBackpackOnCloseEvent()
        {
            return BackpackOnClose;
        }
    }
}