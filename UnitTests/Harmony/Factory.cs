using System;
using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using ImmersiveToolBelt.Harmony.Interfaces;
using ImmersiveToolBelt.Harmony.Seams;
using Moq;

namespace UnitTests.Harmony;

public static class Factory
{
    public static FactoryContainer Create(Dictionary<string, object> parameters = null)
    {
        var toolBeltMock = new Mock<IXUiView>();
        var loggerMock = new Mock<ILogger>();

        ToolBelt.SetLogger(loggerMock.Object);

        if (parameters == null) return new FactoryContainer { toolBeltMock = toolBeltMock };

        var backpackOnOpen = parameters.ContainsKey("BackpackOnOpen") && (bool)parameters["BackpackOnOpen"];
        ToolBeltEvent.BackpackOnOpen = backpackOnOpen;

        var slotChanged = parameters.ContainsKey("SlotChanged") && (bool)parameters["SlotChanged"];
        ToolBeltEvent.SlotChanged = slotChanged;

        var hideDelayElapsed = parameters.ContainsKey("hideDelayElapsed") && (bool)parameters["hideDelayElapsed"];
        var currentTime = new DateTime(2024, 8, 5, 10, 0, 0);
        var dateTimeSeam = new DateTimeSeam(() => currentTime);
        var subtractSeconds = hideDelayElapsed ? -4 : -3;
        ToolBelt.DelayTimerSetAt = currentTime.AddSeconds(subtractSeconds);

        var toolBeltIsVisible =
            parameters.ContainsKey("toolBeltIsVisible") && (bool)parameters["toolBeltIsVisible"];
        toolBeltMock.Setup(p => p.IsVisible).Returns(toolBeltIsVisible);

        return new FactoryContainer
        {
            now = dateTimeSeam,
            toolBeltMock = toolBeltMock,
        };
    }
}

public class FactoryContainer
{
    public Mock<IXUiView> toolBeltMock { get; set; }
    public IDateTime now { get; set; }
}