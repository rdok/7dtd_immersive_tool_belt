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
        var toolBeltWindowGroupMock = new Mock<IXUiController>();
        var xuiMock = new Mock<IXUi>();
        var playerUIMock = new Mock<ILocalPlayerUI>();
        var entityPlayerLocalMock = new Mock<IEntityPlayerLocal>();
        var windowManagerMock = new Mock<IGUIWindowManager>();
        var toolBeltWindowMock = new Mock<IXUiControllerChild>();
        var viewComponentMock = new Mock<IXUiView>();

        if (parameters == null)
        {
            return new FactoryContainer
            {
                entityPlayerLocalMock = entityPlayerLocalMock,
                playerUIMock = playerUIMock,
                xUIMock = xuiMock
            };
        }

        var hideDelayElapsed = parameters.ContainsKey("hideDelayElapsed") && (bool)parameters["hideDelayElapsed"];
        var currentTime = new DateTime(2024, 8, 5, 10, 0, 0);
        var dateTimeSeam = new DateTimeSeam(() => currentTime);
        if (hideDelayElapsed)
        {
            ToolBelt.BackpackHiddenAt = currentTime.AddSeconds(-4);
        }

        var hasXui = parameters.ContainsKey("hasXui") && (bool)parameters["hasXui"];
        var xui = hasXui ? xuiMock.Object : null;
        playerUIMock.Setup(p => p.xui).Returns(xui);

        var hasWindowManager = parameters.ContainsKey("hasWindowManager") && (bool)parameters["hasWindowManager"];
        var windowManager = hasWindowManager ? windowManagerMock.Object : null;
        playerUIMock.Setup(p => p.windowManager).Returns(windowManager);

        var isBackpackVisible = parameters.ContainsKey("isBackpackVisible") && (bool)parameters["isBackpackVisible"];
        windowManagerMock.Setup(p => p.IsWindowOpen("backpack")).Returns(isBackpackVisible);

        var isVisible =
            parameters.ContainsKey("isVisible") && (bool)parameters["isVisible"];
        viewComponentMock.Setup(p => p.IsVisible).Returns(isVisible);

        var hasViewComponent =
            parameters.ContainsKey("hasViewComponent") && (bool)parameters["hasViewComponent"];
        var viewComponent = hasViewComponent ? viewComponentMock.Object : null;
        toolBeltWindowMock.Setup(p => p.ViewComponent).Returns(viewComponent);

        var isToolBeltVisible =
            parameters.ContainsKey("isToolBeltVisible") && (bool)parameters["isToolBeltVisible"];
        viewComponentMock.Setup(p => p.IsVisible).Returns(isToolBeltVisible);

        var hasToolBeltWindow =
            parameters.ContainsKey("hasToolBeltWindow") && (bool)parameters["hasToolBeltWindow"];
        var toolBeltWindow = hasToolBeltWindow ? toolBeltWindowMock.Object : null;
        toolBeltWindowGroupMock.Setup(p => p.GetChildById("windowToolbelt")).Returns(toolBeltWindow);

        var hasToolBeltWindowGroup =
            parameters.ContainsKey("hasToolBeltWindowGroup") && (bool)parameters["hasToolBeltWindowGroup"];
        var toolBeltWindowGroup = hasToolBeltWindowGroup ? toolBeltWindowGroupMock.Object : null;
        xuiMock.Setup(p => p.FindWindowGroupByName("toolbelt")).Returns(toolBeltWindowGroup);

        var hasPlayerUI = parameters.ContainsKey("PlayerUI") && (bool)parameters["PlayerUI"];
        var playerUIInstance = hasPlayerUI ? playerUIMock.Object : null;
        entityPlayerLocalMock.Setup(p => p.PlayerUI).Returns(playerUIInstance);

        var bPlayerStatsChanged =
            parameters.ContainsKey("bPlayerStatsChanged") && (bool)parameters["bPlayerStatsChanged"];
        entityPlayerLocalMock.Setup(p => p.bPlayerStatsChanged).Returns(bPlayerStatsChanged);

        var logger = new Mock<ILogger>();

        ToolBelt.SetLogger(logger.Object);

        return new FactoryContainer
        {
            now = dateTimeSeam,
            entityPlayerLocalMock = entityPlayerLocalMock,
            playerUIMock = playerUIMock,
            xUIMock = xuiMock,
            toolBeltWindowGroupMock = toolBeltWindowGroupMock,
            toolBeltWindowMock = toolBeltWindowMock,
            windowManagerMock = windowManagerMock,
            viewComponentMock = viewComponentMock
        };
    }
}

public class FactoryContainer
{
    public Mock<IXUiControllerChild> toolBeltWindowMock { get; set; }
    public Mock<IXUiController> toolBeltWindowGroupMock { get; set; }
    public Mock<IEntityPlayerLocal> entityPlayerLocalMock { get; set; }
    public Mock<ILocalPlayerUI> playerUIMock { get; set; }
    public Mock<IXUi> xUIMock { get; set; }
    public Mock<IGUIWindowManager> windowManagerMock { get; set; }
    public Mock<IXUiView> viewComponentMock { get; set; }
    public IDateTime now { get; set; }
}