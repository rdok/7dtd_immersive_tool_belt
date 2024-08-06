using HarmonyLib;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(XUiC_BackpackWindow), nameof(XUiC_BackpackWindow.OnClose))]
    public class Backpack
    {
        private static readonly ILogger Logger = new Logger();

        private static void Prefix(XUiC_BackpackWindow __instance)
        {
            ToolBeltEvent.DispatchBackpackOnClose();
        }
    }

    [HarmonyPatch(typeof(XUiC_BackpackWindow), nameof(XUiC_BackpackWindow.OnOpen))]
    public class BackpackOnOPen
    {
        private static readonly ILogger Logger = new Logger();

        private static void Prefix(XUiC_BackpackWindow __instance)
        {
            ToolBeltEvent.DispatchBackpackOnOpen();
        }
    }
}