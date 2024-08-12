using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class PlayerActionsLocalSeam : IPlayerActionsLocal
    {
        private readonly PlayerActionsLocal _playerInput;

        public PlayerActionsLocalSeam(PlayerActionsLocal playerInput)
        {
            _playerInput = playerInput;
        }

        public int InventorySlotWasPressed => _playerInput.InventorySlotWasPressed;

        public IPlayerAction InventorySlotLeft => new PlayerActionSteam(_playerInput.InventorySlotLeft);
        public IPlayerAction InventorySlotRight => new PlayerActionSteam(_playerInput.InventorySlotRight);
    }
}