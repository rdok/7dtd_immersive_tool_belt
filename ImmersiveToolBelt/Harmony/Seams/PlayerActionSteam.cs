using ImmersiveToolBelt.Harmony.Interfaces;
using InControl;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class PlayerActionSteam : IPlayerAction
    {
        private readonly PlayerAction _playerAction;

        public PlayerActionSteam(PlayerAction playerAction)
        {
            _playerAction = playerAction;
        }

        public bool WasPressed => _playerAction.WasPressed;
    }
}