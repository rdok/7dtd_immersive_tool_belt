using System;
using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), "Update")]
    public static class ToolBelt
    {
        private static ILogger _logger = new Logger();
        public static DateTime BackpackHiddenAt;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void Postfix(EntityPlayerLocal __instance)
        {
            var entityPlayerLocalWrapper = new EntityPlayerLocalSeam(__instance);
            var dateTimeSeam = new DateTimeSeam();
            PostfixWrapper(entityPlayerLocalWrapper, dateTimeSeam);
        }

        public static void PostfixWrapper(IEntityPlayerLocal player, IDateTime dateTime)
        {
            if (player == null) return;
            if (player.bPlayerStatsChanged) return;

            var playerUI = player.PlayerUI;
            if (playerUI == null) return;

            var xui = playerUI.xui; // TODO: replace with window manager check
            if (xui == null) return;

            var toolBeltWindowGroup = xui.FindWindowGroupByName("toolbelt");
            if (toolBeltWindowGroup == null) return;

            var toolBeltWindow = toolBeltWindowGroup.GetChildById("windowToolbelt");
            if (toolBeltWindow == null) return;

            var isBackpackVisible = playerUI.windowManager.IsWindowOpen("backpack");
            var isToolBeltVisible = toolBeltWindow.ViewComponent.IsVisible;

            if (isBackpackVisible == isToolBeltVisible)
            {
                return;
            }

            if (isBackpackVisible)
            {
                _logger.Info("Backpack visible, tool belt is not. Showing tool belt.");
                toolBeltWindow.ViewComponent.ForceHide = false;
                toolBeltWindow.ViewComponent.IsVisible = true;
                return;
            }

            var now = dateTime.Now();
            if (BackpackHiddenAt == DateTime.MinValue)
            {
                BackpackHiddenAt = now;
            }

            const int hideDelaySeconds = 3;
            var secondsSinceBackpackHidden = (now - BackpackHiddenAt).TotalSeconds;
            var hideDelayElapsed = secondsSinceBackpackHidden >= hideDelaySeconds;

            if (!hideDelayElapsed)
            {
                _logger.Info($"{secondsSinceBackpackHidden} seconds passed since Backpack closed.");
                return;
            }

            _logger.Info($"{secondsSinceBackpackHidden} seconds passed since Backpack closed, hiding windowToolbelt.");
            toolBeltWindow.ViewComponent.ForceHide = true;
            toolBeltWindow.ViewComponent.IsVisible = false;
            BackpackHiddenAt = DateTime.MinValue;
        }
    }
}