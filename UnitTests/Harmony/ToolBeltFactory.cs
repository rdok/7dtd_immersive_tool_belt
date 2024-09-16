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
        var toolBeltMock = new Mock<IXUiView>();
        var loggerMock = new Mock<ILogger>();

        ToolBeltPatch.SetLogger(loggerMock.Object);

        if (parameters == null) return new ToolBeltContainer { toolBeltMock = toolBeltMock };

        var toolBeltEventIsAlive = parameters.ContainsKey("ToolBeltEvent.IsAlive") &&
                             (bool)parameters["ToolBeltEvent.IsAlive"];
        if(toolBeltEventIsAlive) ToolBeltEvent.Trigger();

        var hideDelayElapsed = parameters.ContainsKey("hideDelayElapsed") && (bool)parameters["hideDelayElapsed"];
        var currentTime = new DateTime(2024, 8, 5, 10, 0, 0);
        var dateTimeSeam = new DateTimeSeam(() => currentTime);
        var subtractSeconds = hideDelayElapsed ? -4 : -3;
        // ToolBelt.DelayTimerSetAt = currentTime.AddSeconds(subtractSeconds);

        var toolBeltIsVisible =
            parameters.ContainsKey("toolBeltIsVisible") && (bool)parameters["toolBeltIsVisible"];
        toolBeltMock.Setup(p => p.IsVisible).Returns(toolBeltIsVisible);

        return new ToolBeltContainer()
        {
            now = dateTimeSeam,
            toolBeltMock = toolBeltMock,
        };
    }
}

public class ToolBeltContainer
{
    public Mock<IXUiView> toolBeltMock { get; set; }
    public IDateTime now { get; set; }
}