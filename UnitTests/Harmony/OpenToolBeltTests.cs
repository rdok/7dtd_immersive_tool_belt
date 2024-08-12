using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using NUnit.Framework;

namespace UnitTests.Harmony;

[TestFixture]
public class OpenToolBeltTests
{
    private static readonly object[] ToolBeltTestCases =
    [
        new TestCaseData(new Dictionary<string, object> { ["BackpackOnOpen"] = true })
            .SetName("having_backpack_open"),
        new TestCaseData(new Dictionary<string, object> { ["SlotChanged"] = true })
            .SetName("having_slot_changed")
    ];

    [Test, TestCaseSource(nameof(ToolBeltTestCases))]
    public void open_tool_belt(Dictionary<string, object> initialState)
    {
        var factory = ToolBeltFactory.Create(initialState);
        ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
        factory.toolBeltMock.VerifySet(h => h.ForceHide = false);
        factory.toolBeltMock.VerifySet(h => h.IsVisible = true);
    }

    [Test, TestCaseSource(nameof(ToolBeltTestCases))]
    public void resets_slots_changed(Dictionary<string, object> initialState)
    {
        var factory = ToolBeltFactory.Create(new Dictionary<string, object> { ["BackpackOnOpen"] = true });
        ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
        Assert.IsFalse(ToolBeltEvent.SlotChanged);
    }

    [Test, TestCaseSource(nameof(ToolBeltTestCases))]
    public void resets_delay_timer(Dictionary<string, object> initialState)
    {
        var factory = ToolBeltFactory.Create(new Dictionary<string, object> { ["BackpackOnOpen"] = true });
        ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
        Assert.AreEqual(factory.now.Now(), ToolBelt.DelayTimerSetAt);
    }
}