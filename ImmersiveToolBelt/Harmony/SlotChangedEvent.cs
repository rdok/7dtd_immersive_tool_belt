using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.Update))]
    public class SlotChangedEvent
    {
        private static readonly ILogger Logger = new Logger();

        public static void Prefix(EntityPlayerLocal __instance)
        {
            var toolBeltEvent = new ToolBeltEventSeam();
            Wrapper(new EntityPlayerLocalSeam(__instance), toolBeltEvent);
        }

        public static void Wrapper(
            IEntityPlayerLocal entityPlayerLocal, IToolBeltEvent toolBeltEvent
        )
        {
            var playerInput = entityPlayerLocal.playerInput;
            if (playerInput == null) return;

            var slotChangedEvent =
                playerInput.InventorySlotWasPressed != -1 ||
                playerInput.InventorySlotLeft.WasPressed ||
                playerInput.InventorySlotRight.WasPressed;

            if (!slotChangedEvent) return;

            Logger.Debug("[EntityPlayerLocal]: slotChangedEvent");

            toolBeltEvent.Trigger();
        }
    }
}