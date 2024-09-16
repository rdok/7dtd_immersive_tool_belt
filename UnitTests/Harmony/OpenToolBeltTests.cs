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
    
    [Test]
    [TestCaseSource(nameof(ToolBeltTestCases))]
    public void open_tool_belt(Dictionary<string, object> initialState)
    {
        var factory = ToolBeltFactory.Create(initialState);
        factory.ToolBelt.Update(factory.XUiViewMock.Object, factory.DateTime);
        factory.XUiViewMock.VerifySet(h => h.ForceHide = false);
        factory.XUiViewMock.VerifySet(h => h.IsVisible = true);
    }
    
    [Test]
    [TestCaseSource(nameof(ToolBeltTestCases))]
    public void resets_slots_changed(Dictionary<string, object> initialState)
    {
        var factory = ToolBeltFactory.Create(new Dictionary<string, object> { ["BackpackOnOpen"] = true });
        factory.ToolBelt.Update(factory.XUiViewMock.Object, factory.DateTime);
        Assert.IsFalse(ToolBeltEvent.SlotChanged);
    }
}