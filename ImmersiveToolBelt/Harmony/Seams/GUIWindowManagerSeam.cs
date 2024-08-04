using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class GUIWindowManagerSeam : IGUIWindowManager
    {
        private readonly GUIWindowManager _windowManager;

        public GUIWindowManagerSeam(GUIWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public bool IsWindowOpen(string windowId)
        {
            return _windowManager.IsWindowOpen(windowId);
        }
    }
}