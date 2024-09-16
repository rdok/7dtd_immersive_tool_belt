using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(XUiC_ToolbeltWindow), nameof(XUiC_ToolbeltWindow.Update))]
    public class ToolbelWindowPatch
    {
        private static readonly ToolBelt ToolBelt = new ToolBelt();
        private static readonly IDateTime Datetime = new DateTimeSeam();

        public static bool Prefix(XUiC_ToolbeltWindow __instance)
        {
            var toolBelt = new XUiViewSeam(__instance.ViewComponent);

            ToolBelt.Update(toolBelt, Datetime);

            const bool allowOriginalMethodExecution = true;

            return allowOriginalMethodExecution;
        }
    }
}