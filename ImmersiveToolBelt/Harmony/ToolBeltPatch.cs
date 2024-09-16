using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(XUiC_ToolbeltWindow), nameof(XUiC_ToolbeltWindow.Update))]
    public class ToolBeltPatch
    {
        private static ILogger _logger = new Logger();

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static bool Prefix(XUiC_ToolbeltWindow __instance)
        {
            var toolBeltEvent = ServiceRegistry.Resolve<IToolBeltEvent>();
            var viewSeam = ServiceRegistry.Resolve<ViewComponent, IXUiView>(viewComponent);
            var dateTimeSeam = new DateTimeSeam();
            Wrapper(toolBelt, dateTimeSeam, toolBeltEvent);

            const bool allowOriginalMethodExecution = true;
            return allowOriginalMethodExecution;
        }

        public static void Wrapper(
            IXUiView toolBelt, IDateTime dateTime, IToolBeltEvent toolBeltEvent
        )
        {
            var now = dateTime.Now();

            if (toolBeltEvent.IsAlive())
            {
                toolBelt.ForceHide = false;
                toolBelt.IsVisible = true;
                _logger.Debug("Showing tool belt.");
            }

            if (toolBeltEvent.BackpackOnOpen) return;

            const int delayInSeconds = 3;
            var delayTimerInSeconds = (now - toolBeltEvent.ChangedAt).TotalSeconds;
            var hideDelayElapsed = delayTimerInSeconds > delayInSeconds;

            if (!hideDelayElapsed) return;

            _logger.Debug($"{delayTimerInSeconds} seconds passed since Backpack closed, closing windowToolbelt.");

            toolBelt.ForceHide = true;
            toolBelt.IsVisible = false;
            toolBeltEvent.Dispose();
        }
    }
}