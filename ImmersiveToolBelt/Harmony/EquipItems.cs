using HarmonyLib;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(ItemActionEntryEquip), nameof(ItemActionEntryEquip.OnActivated))]
    public class ItemActionEntryEquipUpdate
    {
        private static void Prefix(EquipItems __instance)
        {
            ToolBeltEvent.DispatchSlotChangedEvent();
        }
    }
}