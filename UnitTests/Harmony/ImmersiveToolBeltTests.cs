using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace UnitTests.Harmony;

[TestFixture]
public class ImmersiveToolBeltInitTests
{
    [Test]
    public void it_skis_update_having_a_nullable_player_instance()
    {
        var factory = Factory.Create();
        ImmersiveToolBelt.Harmony.ToolBelt.UpdateVisibility(null);
        factory.entityPlayerLocalMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skis_update_having_changed_player_stats()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = true
        });
        ImmersiveToolBelt.Harmony.ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.playerUIMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skis_update_having_a_nullable_player_ui()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = false
        });
        ImmersiveToolBelt.Harmony.ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.xUIMock.VerifyNoOtherCalls();
    }
    
    [Test]
    public void it_skis_update_having_a_nullable_xui()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = false
        });
        ImmersiveToolBelt.Harmony.ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.toolBeltWindowGroupMock.VerifyNoOtherCalls();
    }
}