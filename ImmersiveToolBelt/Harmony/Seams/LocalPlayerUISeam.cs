using System;
using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class LocalPlayerUISeam : ILocalPlayerUI
    {
        private readonly LocalPlayerUI _localPlayerUI;
        private readonly ILogger _logger = new Logger();

        public LocalPlayerUISeam(LocalPlayerUI playerUI)
        {
            _localPlayerUI =
                playerUI ?? throw new ArgumentNullException(nameof(playerUI));
        }

        public IXUi xui => new XUiSeam(_localPlayerUI.xui);
        public IGUIWindowManager windowManager => new GUIWindowManagerSeam(_localPlayerUI.windowManager);
    }
}