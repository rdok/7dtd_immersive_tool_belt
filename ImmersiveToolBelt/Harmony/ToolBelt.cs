using System;
using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(XUiC_ToolbeltWindow), nameof(XUiC_ToolbeltWindow.Update))]
    public class ToolBelt
    {
        private static ILogger _logger = new Logger();
        public static DateTime DelayTimerSetAt;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static bool Prefix(XUiC_ToolbeltWindow __instance)
        {
            var toolBelt = new XUiViewSeam(__instance.ViewComponent);
            var dateTimeSeam = new DateTimeSeam();
            Wrapper(toolBelt, dateTimeSeam);

            const bool allowOriginalMethodExecution = true;
            return allowOriginalMethodExecution;
        }

        public static void Wrapper(IXUiView toolBelt, IDateTime dateTime)
        {
            var now = dateTime.Now();

            if (ToolBeltEvent.BackpackOnOpen || ToolBeltEvent.SlotChanged)
            {
                _logger.Debug($"ToolBeltEvent.BackpackOnOpen: {ToolBeltEvent.BackpackOnOpen}");
                _logger.Debug($"ToolBeltEvent.SlotChanged: {ToolBeltEvent.SlotChanged}");
                toolBelt.ForceHide = false;
                toolBelt.IsVisible = true;
                _logger.Debug($"Showing tool belt.");

                if (DelayTimerSetAt == DateTime.MinValue) DelayTimerSetAt = now;
            }

            const int delayInSeconds = 3;
            var delayTimerInSeconds = (now - DelayTimerSetAt).TotalSeconds;
            var hideDelayElapsed = delayTimerInSeconds > delayInSeconds;

            if (!hideDelayElapsed) return;

            _logger.Debug($"{delayTimerInSeconds} seconds passed since Backpack closed, closing windowToolbelt.");

            DelayTimerSetAt = DateTime.MinValue;
            ToolBeltEvent.SlotChanged = false;
            toolBelt.ForceHide = true;
            toolBelt.IsVisible = false;
        }
    }
}