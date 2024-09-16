using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ImmersiveToolBelt.Harmony
{
    public class Settings : ISettings
    {
        private readonly ILogger _logger;
        private XDocument _settingsXml;

        public Settings(Logger logger)
        {
            _logger = logger;
            _logger.Debug("Initialising settings.");

            HideDelayInSecondsSetting = GetInt("General", "ImmersiveToolBelt", "HideDelayInSeconds");
        }

        public int HideDelayInSecondsSetting { get; set; }

        public string GetString(string tab, string category, string settingName)
        {
            var value = GetSettingValue(tab, category, settingName);
            _logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }


        private void EnsureSettingsLoaded()
        {
            if (_settingsXml != null) return;

            var modPath = Assembly.GetExecutingAssembly().Location;
            var modDirectory = Path.GetDirectoryName(modPath);
            var xmlPath = Path.Combine(modDirectory, "ModSettings.xml");
            _settingsXml = XDocument.Load(xmlPath);

            _logger.Debug($"Settings loaded from: {xmlPath}");
        }

        private string GetSettingValue(string tab, string category, string settingName)
        {
            EnsureSettingsLoaded();
            var value = _settingsXml.Element("ModSettings")
                ?.Element("Global")
                ?.Elements("Tab")
                .FirstOrDefault(e => e.Attribute("name")?.Value == tab)
                ?.Elements("Category")
                .FirstOrDefault(e => e.Attribute("name")?.Value == category)
                ?.Elements()
                .FirstOrDefault(e => e.Attribute("name")?.Value == settingName)
                ?.Attribute("defaultValue")
                ?.Value;

            _logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public float GetFloat(string tab, string category, string settingName)
        {
            var value = float.Parse(GetSettingValue(tab, category, settingName));
            _logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        private int GetInt(string tab, string category, string settingName)
        {
            var value = int.Parse(GetSettingValue(tab, category, settingName));
            _logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public byte GetByte(string tab, string category, string settingName)
        {
            var value = byte.Parse(GetSettingValue(tab, category, settingName));
            _logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }

        public bool GetBool(string tab, string category, string settingName)
        {
            var value = bool.Parse(GetSettingValue(tab, category, settingName));
            _logger.Debug($"{tab}.{category}.{settingName}: {value}");
            return value;
        }
    }
}