using System;
using HarmonyLib;

namespace ImmersiveToolBelt.Harmony
{
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.setHeldItemByIndex))]
    public class SlotChangedEvent
    {
        private static readonly ILogger Logger = new Logger();

        static void Prefix(Inventory __instance, int _inventoryIdx, bool _applyHolsterTime)
        {
            var currentItem = __instance.holdingItemItemValue;
            
            Logger.Info("SlotChangedEvent.Prefix.setHeldItemByIndex triggered by Inentory");

            ToolBeltEvent.DispatchSlotChangedEvent();
        }
    }
}