using System;
using HarmonyLib;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(XUiC_BackpackWindow), nameof(XUiC_BackpackWindow.OnClose))]
    public class Backpack
    {
        private static readonly ILogger Logger = new Logger();

        static void Prefix(XUiC_BackpackWindow __instance)
        {
            Logger.Info("XUiC_BackpackWindow.Onclose.Postfix");

            ToolBeltEvent.DispatchBackpackOnClose();
        }
    }

    [HarmonyPatch(typeof(XUiC_BackpackWindow), nameof(XUiC_BackpackWindow.OnOpen))]
    public class BackpackOnOPen
    {
        private static readonly ILogger Logger = new Logger();

        static void Prefix(XUiC_BackpackWindow __instance)
        {
            Logger.Info("XUiC_BackpackWindow.Onclose.Postfix");

            ToolBeltEvent.DispatchBackpackOnOpen();
        }
    }
}