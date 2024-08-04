namespace ImmersiveToolBelt.Harmony.Interfaces
{
    public interface IXUi
    {
        ILocalPlayerUI playerUI { get; }
        IXUiController FindWindowGroupByName(string name);
        // IGUIWindowManager windowManager { get; }
    }
}