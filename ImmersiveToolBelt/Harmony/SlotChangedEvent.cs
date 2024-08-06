using HarmonyLib;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.Update))]
    public class SlotChangedEvent
    {
        private static readonly ILogger _logger = new Logger();

        private static void Prefix(EntityPlayerLocal __instance)
        {
            var playerInput = __instance.playerInput;

            if (playerInput == null) return;

            var slotChangedEvent = playerInput.InventorySlotWasPressed != -1 || playerInput.Scroll.WasPressed;

            if (!slotChangedEvent) return;

            _logger.Info($"{__instance.GetType().Name}.ToolBeltScrolled.{nameof(Prefix)}: InventorySlotWasPressed");

            ToolBeltEvent.DispatchSlotChangedEvent();
        }
    }
}