using System;
using ImmersiveToolBelt.Harmony.Interfaces;

namespace ImmersiveToolBelt.Harmony
{
    public class ToolBelt
    {
        private readonly ILogger _logger;
        private readonly ISettings _settings;
        public DateTime DelayTimerSetAt;

        public ToolBelt()
        {
            _logger = Services.Get<ILogger>();
            _settings = Services.Get<ISettings>();
        }

        public ToolBelt(ILogger logger, ISettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public void Update(IXUiView toolBelt, IDateTime dateTime)
        {
            var now = dateTime.Now();

            if (ToolBeltEvent.BackpackOnOpen)
            {
                _logger.Debug($"ToolBeltEvent.BackpackOnOpen: {ToolBeltEvent.BackpackOnOpen}");
                toolBelt.ForceHide = false;
                toolBelt.IsVisible = true;
                DelayTimerSetAt = DateTime.MinValue;
                return;
            }

            if (ToolBeltEvent.SlotChanged)
            {
                _logger.Debug($"ToolBeltEvent.SlotChanged: {ToolBeltEvent.SlotChanged}");
                _logger.Debug($"ToolBeltEvent.BackpackOnClose: {ToolBeltEvent.BackpackOnClose}");
                toolBelt.ForceHide = false;
                toolBelt.IsVisible = true;
                _logger.Debug("Showing tool belt.");

                if (DelayTimerSetAt == DateTime.MinValue) DelayTimerSetAt = now;
            }

            var delayInSeconds = _settings.HideDelayInSecondsSetting;
            var delayTimerInSeconds = (now - DelayTimerSetAt).TotalSeconds;
            var hideDelayElapsed = delayTimerInSeconds > delayInSeconds;

            if (!hideDelayElapsed) return;

            _logger.Debug($"{delayTimerInSeconds} seconds passed since Backpack closed, closing windowToolbelt.");

            DelayTimerSetAt = DateTime.MinValue;
            ToolBeltEvent.SlotChanged = false; 
            toolBelt.ForceHide = true;
            toolBelt.IsVisible = false;
        }
    }
}