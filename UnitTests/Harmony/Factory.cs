using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
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

        if (parameters == null)
        {
            return new FactoryContainer
            {
                entityPlayerLocalMock = entityPlayerLocalMock,
                playerUIMock = playerUIMock,
                xUIMock = xuiMock
            };
        }
        
        var hasXui = parameters.ContainsKey("hasXui") && (bool)parameters["hasXui"];
        var xui = hasXui ? xuiMock.Object : null;
        playerUIMock.Setup(p => p.xui).Returns(xui);

        var hasToolBeltGroup = parameters.ContainsKey("hasToolBeltGroup") && (bool)parameters["hasToolBeltGroup"];
        var toolBeltGroup = hasToolBeltGroup ? toolBeltWindowGroupMock.Object : null;
        xuiMock.Setup(p => p.FindWindowGroupByName("toolbelt")).Returns(toolBeltGroup);

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
            entityPlayerLocalMock = entityPlayerLocalMock,
            playerUIMock = playerUIMock,
            xUIMock = xuiMock,
            toolBeltWindowGroupMock = toolBeltWindowGroupMock
        };
    }
}

public class FactoryContainer
{
    public Mock<IXUiController> toolBeltWindowGroupMock { get; set; }
    public Mock<IEntityPlayerLocal> entityPlayerLocalMock { get; set; }
    public Mock<ILocalPlayerUI> playerUIMock { get; set; }
    public Mock<IXUi> xUIMock { get; set; }
}