using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using NUnit.Framework;

namespace UnitTests.Harmony;

[TestFixture]
public class ImmersiveToolBeltInitTests
{
    [Test]
    public void it_skips_update_having_no_player_instance()
    {
        var factory = Factory.Create();
        ToolBelt.UpdateVisibility(null);
        factory.entityPlayerLocalMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skips_update_having_changed_player_stats()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = true
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.playerUIMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skips_update_having_no_player_ui()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = false
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.xUIMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skips_update_having_no_xui()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = false
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.xUIMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skips_update_having_no_tool_belt_window_group()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = true,
            ["hasToolBeltWindowGroup"] = true
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.toolBeltWindowMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skips_update_having_no_tool_belt_window()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = true,
            ["hasToolBeltWindowGroup"] = true,
            ["hasWindowManager"] = false
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.windowManagerMock.VerifyNoOtherCalls();
    }

    [Test]
    public void it_checks_if_backpack_is_open()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = true,
            ["hasToolBeltWindowGroup"] = true,
            ["hasToolBeltWindow"] = true,
            ["hasWindowManager"] = true,
            ["hasViewComponent"] = true
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.windowManagerMock.Verify(h => h.IsWindowOpen("backpack"));
    }

    [Test]
    public void it_hides_the_tool_belt_window_having_backpack_open_and_tool_belt_closed()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = true,
            ["hasToolBeltWindowGroup"] = true,
            ["hasToolBeltWindow"] = true,
            ["hasWindowManager"] = true,
            ["hasViewComponent"] = true,
            ["isBackpackOpen"] = true,
            ["isToolBeltOpen"] = false
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.viewComponentMock.VerifySet(h => h.ForceHide = false);
        factory.viewComponentMock.VerifySet(h => h.IsVisible = true);
    }

    [Test]
    public void it_shows_the_tool_belt_window_having_backpack_closed_and_tool_belt_open()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = false,
            ["PlayerUI"] = true,
            ["hasXui"] = true,
            ["hasToolBeltWindowGroup"] = true,
            ["hasToolBeltWindow"] = true,
            ["hasWindowManager"] = true,
            ["hasViewComponent"] = true,
            ["isBackpackOpen"] = false,
            ["isToolBeltOpen"] = true
        });
        ToolBelt.UpdateVisibility(factory.entityPlayerLocalMock.Object);
        factory.viewComponentMock.VerifySet(h => h.ForceHide = true);
        factory.viewComponentMock.VerifySet(h => h.IsVisible = false);
    }
}