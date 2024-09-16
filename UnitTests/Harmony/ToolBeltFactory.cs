using System;
using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;
using Moq;

namespace UnitTests.Harmony;

public static class ToolBeltFactory
{
    public static ToolBeltContainer Create(Dictionary<string, object> parameters = null)
    {
        var xuiViewMock = new Mock<IXUiView>();
        var loggerMock = new Mock<ILogger>();
        var settingsMock = new Mock<ISettings>();

        var toolBelt = new ToolBelt(loggerMock.Object, settingsMock.Object);

        if (parameters == null) return new ToolBeltContainer { XUiViewMock = xuiViewMock };

        var backpackOnOpen = parameters.ContainsKey("BackpackOnOpen") && (bool)parameters["BackpackOnOpen"];
        ToolBeltEvent.BackpackOnOpen = backpackOnOpen;

        var slotChanged = parameters.ContainsKey("SlotChanged") && (bool)parameters["SlotChanged"];
        ToolBeltEvent.SlotChanged = slotChanged;

        var hideDelayElapsed = parameters.ContainsKey("hideDelayElapsed") && (bool)parameters["hideDelayElapsed"];
        var currentTime = new DateTime(2024, 8, 5, 10, 0, 0);
        var dateTimeSeam = new DateTimeSeam(() => currentTime);
        var subtractSeconds = hideDelayElapsed ? -4 : -3;
        toolBelt.DelayTimerSetAt = currentTime.AddSeconds(subtractSeconds);

        var toolBeltIsVisible =
            parameters.ContainsKey("toolBeltIsVisible") && (bool)parameters["toolBeltIsVisible"];
        xuiViewMock.Setup(p => p.IsVisible).Returns(toolBeltIsVisible);

        return new ToolBeltContainer
        {
            ToolBelt = toolBelt,
            DateTime = dateTimeSeam,
            XUiViewMock = xuiViewMock
        };
    }
}

public class ToolBeltContainer
{
    public Mock<IXUiView> XUiViewMock { get; set; }
    public IDateTime DateTime { get; set; }
    public ToolBelt ToolBelt { get; set; }
}