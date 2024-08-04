using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class XUiControllerSeam : IXUiController
    {
        private readonly XUi _xUi;

        public XUiControllerSeam(XUi xUi)
        {
            _xUi = xUi;
        }

        public IXUiControllerChild GetChildById(string id)
        {
            return new XUiControllerChildSeam(_xUi.GetChildById(id));
        }
    }
}