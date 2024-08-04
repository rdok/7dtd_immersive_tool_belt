using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class XUiControllerChildSeam : IXUiControllerChild
    {
        private readonly XUiController _childWindow;

        public XUiControllerChildSeam(XUiController childWindow)
        {
            _childWindow = childWindow;
        }

        public IXUiView ViewComponent => new XUiViewSeam(_childWindow.ViewComponent);
    }
}