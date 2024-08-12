namespace ImmersiveToolBelt.Harmony.Interfaces
{
    public interface IPlayerActionsLocal
    {
        int InventorySlotWasPressed { get; }
        IPlayerAction InventorySlotLeft { get; }
        IPlayerAction InventorySlotRight { get; }
    }
}