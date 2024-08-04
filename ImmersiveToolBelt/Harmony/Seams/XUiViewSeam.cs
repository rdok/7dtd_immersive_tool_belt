using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony.Seams
{
    public class XUiViewSeam : IXUiView
    {
        private readonly XUiView _xUiView;

        public XUiViewSeam(XUiView _xUiView)
        {
            this._xUiView = _xUiView;
        }

        public bool IsVisible
        {
            get => _xUiView.IsVisible;
            set => _xUiView.IsVisible = value;
        }

        public bool ForceHide
        {
            get => _xUiView.ForceHide;
            set => _xUiView.ForceHide = value;
        }
    }
}