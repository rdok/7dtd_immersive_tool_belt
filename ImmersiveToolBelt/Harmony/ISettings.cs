namespace ImmersiveToolBelt.Harmony
{
    public interface ISettings
    {
        int HideDelayInSecondsSetting { get; set; }
        string GetString(string tab, string category, string settingName);
    }
}