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
        private static DateTime DelayTimerSetAt;

        static bool Prefix(XUiC_ToolbeltWindow __instance)
        {
            _logger.Info("XUiC_ToolbeltWindow.Prefix.update");

            var toolBelt = __instance.ViewComponent;
            var dateTimeSeam = new DateTimeSeam();
            Wrapper(toolBelt, dateTimeSeam);

            const bool allowOriginalMethodExecution = true;

            return allowOriginalMethodExecution;
        }

        public static void Wrapper(XUiView toolBeltViewComponent, IDateTime dateTime)
        {
            var now = dateTime.Now();

                
            if (ToolBeltEvent.HasBackpackOnOpenEvent() || ToolBeltEvent.SlotChangedEvent())
            {
                _logger.Info("Backpack visible/slot changed, tool belt is not. Showing tool belt.");
                toolBeltViewComponent.ForceHide = false;
                toolBeltViewComponent.IsVisible = true;
                ToolBeltEvent.ResetShowEvents();
                DelayTimerSetAt = now;
                return;
            }

            if (DelayTimerSetAt == DateTime.MinValue)
            {
                DelayTimerSetAt = now;
            }

            const int hideDelaySeconds = 3;
            var secondsSinceBackpackHidden = (now - DelayTimerSetAt).TotalSeconds;
            var hideDelayElapsed = secondsSinceBackpackHidden >= hideDelaySeconds;

            if (!hideDelayElapsed)
            {
                _logger.Info($"{secondsSinceBackpackHidden} seconds passed since Backpack closed.");
                return;
            }

            _logger.Info($"{secondsSinceBackpackHidden} seconds passed since Backpack closed, hiding windowToolbelt.");
            toolBeltViewComponent.ForceHide = true;
            toolBeltViewComponent.IsVisible = false;
            DelayTimerSetAt = DateTime.MinValue;
            ToolBeltEvent.ResetHideEvents();
        }
    }
}