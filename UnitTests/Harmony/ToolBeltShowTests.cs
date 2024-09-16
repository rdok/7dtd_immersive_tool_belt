using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using NUnit.Framework;

namespace UnitTests.Harmony;

[TestFixture]
public class ToolBeltShowTests
{
    private static readonly object[] ToolBeltTestCases =
    [
        new TestCaseData(new Dictionary<string, object> { ["ToolBeltEvent.IsAlive"] = true })
            .SetName("ToolBeltEvent.IsAlive"),
    ];

    [Test, TestCaseSource(nameof(ToolBeltTestCases))]
    public void show_tool_belt(Dictionary<string, object> initialState)
    {
        var factory = ToolBeltFactory.Create(initialState);
        ToolBeltPatch.Wrapper(factory.toolBeltMock.Object, factory.now);
        factory.toolBeltMock.VerifySet(h => h.ForceHide = false);
        factory.toolBeltMock.VerifySet(h => h.IsVisible = true);
    }

    // [Test, TestCaseSource(nameof(ToolBeltTestCases))]
    // public void does_not_hide_having_backpack_open(Dictionary<string, object> initialState)
    // {
    //     var factory = ToolBeltFactory.Create(new Dictionary<string, object> { ["BackpackOnOpen"] = true });
    //     ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
    //     Assert.IsFalse(ToolBeltEvent.SlotChanged);
    // }
}