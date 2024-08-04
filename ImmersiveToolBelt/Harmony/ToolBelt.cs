using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), "Update")]
    public static class ToolBelt
    {
        private static ILogger _logger = new Logger();

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void Postfix(EntityPlayerLocal __instance)
        {
            var entityPlayerLocalWrapper = new EntityPlayerLocalSeam(__instance);
            UpdateVisibility(entityPlayerLocalWrapper);
        }

        public static void UpdateVisibility(IEntityPlayerLocal player)
        {
            if (player == null) return;
            if (player.bPlayerStatsChanged) return;

            var playerUI = player.PlayerUI;
            if (playerUI == null) return;

            var xui = playerUI.xui; // TODO: repalace withwindow manager check
            if (xui == null) return;

            var toolBeltWindowGroup = xui.FindWindowGroupByName("toolbelt");
            if (toolBeltWindowGroup == null) return;

            var toolBeltWindow = toolBeltWindowGroup.GetChildById("windowToolbelt");
            if (toolBeltWindow == null) return;

            var isBackpackOpen = playerUI.windowManager.IsWindowOpen("backpack");
            var toolBeltWindowIsOpen = toolBeltWindow.ViewComponent.IsVisible;

            if (isBackpackOpen && !toolBeltWindowIsOpen)
            {
                _logger.Info("Inventory open, showing windowToolbelt.");
                toolBeltWindow.ViewComponent.ForceHide = false;
                toolBeltWindow.ViewComponent.IsVisible = true;
            }
            else if (!isBackpackOpen && toolBeltWindowIsOpen)
            {
                _logger.Info("Inventory closed, hiding windowToolbelt.");
                toolBeltWindow.ViewComponent.ForceHide = true;
                toolBeltWindow.ViewComponent.IsVisible = false;
            }
        }
    }
}