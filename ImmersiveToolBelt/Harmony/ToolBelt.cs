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
                toolBelt.ForceHide = false;
                toolBelt.IsVisible = true;
                ToolBeltEvent.SlotChanged = false;
                DelayTimerSetAt = now;
                return;
            }

            if (DelayTimerSetAt == DateTime.MinValue) DelayTimerSetAt = now;

            const int delayInSeconds = 3;
            var delayTimerInSeconds = (now - DelayTimerSetAt).TotalSeconds;
            var hideDelayElapsed = delayTimerInSeconds > delayInSeconds;

            if (!hideDelayElapsed || !toolBelt.IsVisible) return;

            _logger.Info($"{delayTimerInSeconds} seconds passed since Backpack closed, closing windowToolbelt.");
            toolBelt.ForceHide = true;
            toolBelt.IsVisible = false;
            DelayTimerSetAt = DateTime.MinValue;
        }
    }
}