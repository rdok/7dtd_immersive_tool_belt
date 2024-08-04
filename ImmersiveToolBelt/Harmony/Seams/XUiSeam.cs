using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class XUiSeam : IXUi
    {
        private readonly XUi _xUi;

        public XUiSeam(XUi xUi)
        {
            _xUi = xUi;
        }

        public ILocalPlayerUI playerUI { get; }

        public IXUiController FindWindowGroupByName(string name)
        {
            return new XUiControllerSeam(_xUi);
        }

        // public IGUIWindowManager windowManager => new IWindowManger(_xUi.windowMa);
    }
}