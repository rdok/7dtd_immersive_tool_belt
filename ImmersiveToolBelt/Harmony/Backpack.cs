using HarmonyLib;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(XUiC_BackpackWindow), nameof(XUiC_BackpackWindow.OnClose))]
    public class Backpack
    {
        private static void Prefix(XUiC_BackpackWindow __instance)
        {
            ToolBeltEvent.BackpackOnClose = true;
        }
    }

    [HarmonyPatch(typeof(XUiC_BackpackWindow), nameof(XUiC_BackpackWindow.OnOpen))]
    public class BackpackOnOPen
    {
        private static void Prefix(XUiC_BackpackWindow __instance)
        {
            ToolBeltEvent.BackpackOnOpen = true;
        }
    }
}