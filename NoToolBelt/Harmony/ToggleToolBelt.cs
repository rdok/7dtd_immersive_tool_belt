using System;
using HarmonyLib;
using NoToolBelt.Harmony;

namespace ImmersiveCrosshair.Harmony
{
    public class ToggleToolBelt
    {
        private static readonly ILogger Logger = new Logger();

        [HarmonyPatch(typeof(EntityPlayerLocal), "Update")]
        public static class EntityPlayerLocal_Update
        {
            public static void Postfix(EntityPlayerLocal __instance)
            {
                if (__instance == null || __instance.bPlayerStatsChanged)
                {
                    return;
                }

                var playerUI = __instance.PlayerUI;
                if (playerUI == null) return;

                var xui = playerUI.xui;
                if (xui == null) return;

                const string toolbeltGroupName = "toolbelt";
                const string windowToolbeltName = "windowToolbelt";

                var toolbeltGroup = xui.FindWindowGroupByName(toolbeltGroupName);
                if (toolbeltGroup == null) return;

                var toolbeltWindow = toolbeltGroup.GetChildById(windowToolbeltName);
                if (toolbeltWindow == null) return;

                var isInventoryOpen = xui.playerUI.windowManager.IsWindowOpen("backpack");
                var toolBeltWindowIsOpen = toolbeltWindow.ViewComponent.IsVisible;

                if (isInventoryOpen && !toolBeltWindowIsOpen)
                {
                    Logger.Info("Inventory open, showing windowToolbelt.");
                    toolbeltWindow.ViewComponent.ForceHide = false; 
                    toolbeltWindow.ViewComponent.IsVisible = true;
                }
                else if (!isInventoryOpen && toolBeltWindowIsOpen)
                {
                    Logger.Info("Inventory closed, hiding windowToolbelt.");
                    toolbeltWindow.ViewComponent.ForceHide = true;
                    toolbeltWindow.ViewComponent.IsVisible = false;
                }
            }
        }
    }
}