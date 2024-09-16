using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using NUnit.Framework;
using static UnitTests.Harmony.SlotChangedEventFactory;

namespace UnitTests.Harmony;

public class SlotChangedEventTests
{
    [Test]
    public void it_triggers_having_an_item_selected_through_numbered_keyboard_keymap()
    {
        var entityPlayerLocalMock = Create(new Dictionary<string, object>(Input)
        {
            ["InventorySlotWasPressed"] = true
        });
        SlotChangedEvent.Wrapper(entityPlayerLocalMock.Object);
        Assert.IsTrue(ToolBeltEvent.SlotChanged);
    }

    [Test]
    public void it_triggers_when_scrolling_to_the_left_using_mouse_scroll_or_controller()
    {
        var entityPlayerLocalMock = Create(new Dictionary<string, object>(Input)
        {
            ["InventorySlotLeftWasPressed"] = true
        });
        SlotChangedEvent.Wrapper(entityPlayerLocalMock.Object);
        Assert.IsTrue(ToolBeltEvent.SlotChanged);
    }

    [Test]
    public void it_triggers_when_scrolling_to_the_right_using_mouse_scroll_or_controller()
    {
        var entityPlayerLocalMock = Create(new Dictionary<string, object>(Input)
        {
            ["InventorySlotRightWasPressed"] = true
        });
        SlotChangedEvent.Wrapper(entityPlayerLocalMock.Object);
        Assert.IsTrue(ToolBeltEvent.SlotChanged);
    }

    [Test]
    public void it_does_not_trigger_having_no_player_input()
    {
        var entityPlayerLocalMock = Create(new Dictionary<string, object>(Input)
        {
            ["PlayerInputNull"] = true
        });
        SlotChangedEvent.Wrapper(entityPlayerLocalMock.Object);
        Assert.IsFalse(ToolBeltEvent.SlotChanged);
    }

    [Test]
    public void it_does_not_trigger_having_no_slot_changed()
    {
        var entityPlayerLocalMock = Create(new Dictionary<string, object>(Input));
        SlotChangedEvent.Wrapper(entityPlayerLocalMock.Object);
        Assert.IsFalse(ToolBeltEvent.SlotChanged);
    }
}