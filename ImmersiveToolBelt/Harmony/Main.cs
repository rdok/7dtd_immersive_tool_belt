using GearsAPI.Settings;
using GearsAPI.Settings.Global;
using GearsAPI.Settings.World;

namespace ImmersiveToolBelt.Harmony
{
    public class Main : IModApi, IGearsModApi
    {
        public static IGearsMod gearsMod;
        private static ILogger _logger;
        private static ISettings _settings;

        public Main()
        {
            _settings = Services.Get<ISettings>();
            _logger = Services.Get<ILogger>();
        }

        public void InitMod(IGearsMod modInstance)
        {
            gearsMod = modInstance;
        }

        public void OnGlobalSettingsLoaded(IModGlobalSettings modGlobalSettings)
        {
            var generalTab = modGlobalSettings.GetTab("General");
            var category = generalTab.GetCategory("ImmersiveToolBelt");

            var hideDelayInSecondsSetting = category.GetSetting("HideDelayInSeconds");
            hideDelayInSecondsSetting.OnSettingChanged += HideDelayInSecondsSetting;
            HideDelayInSecondsSetting(
                hideDelayInSecondsSetting,
                (hideDelayInSecondsSetting as IGlobalValueSetting)?.CurrentValue
            );
        }

        public void OnWorldSettingsLoaded(IModWorldSettings worldSettings)
        {
        }

        public void InitMod(Mod modInstance)
        {
            const string id = "uk.co.rdok.7daystodie.mods.immersive_tool_belt";
            _logger.Debug("Loading Patch: " + id);
            var harmony = new HarmonyLib.Harmony(id);
            harmony.PatchAll();
        }

        private static void HideDelayInSecondsSetting(IGlobalModSetting globalModSetting, string value)
        {
            _logger.Debug($"setting.Name: {globalModSetting.Name}. New Value: {value}");
            int.TryParse(value, out var hideDelayInSecondsSetting);
            _settings.HideDelayInSecondsSetting = hideDelayInSecondsSetting;
        }
    }
}