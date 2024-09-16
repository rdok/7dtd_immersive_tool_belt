using HarmonyLib;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal), nameof(EntityPlayerLocal.Update))]
    public class SlotChangedEvent
    {
        public static void Prefix(EntityPlayerLocal __instance)
        {
            Wrapper(new EntityPlayerLocalSeam(__instance));
        }

        public static void Wrapper(IEntityPlayerLocal entityPlayerLocal)
        {
            var playerInput = entityPlayerLocal.playerInput;
            if (playerInput == null) return;

            var slotChangedEvent =
                playerInput.InventorySlotWasPressed != -1 ||
                playerInput.InventorySlotLeft.WasPressed ||
                playerInput.InventorySlotRight.WasPressed;

            if (!slotChangedEvent) return;

            ToolBeltEvent.SlotChanged = true;
        }
    }
}