using System;
using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using NUnit.Framework;

namespace UnitTests.Harmony;

[TestFixture]
public class CloseToolBeltTests
{
    [Test]
    public void sets_the_delayer_time_having_backpack_close()
    {
        var factory = Factory.Create(new Dictionary<string, object> { ["BackpackOnClose"] = false });
        ToolBelt.DelayTimerSetAt = DateTime.MinValue;
        ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
        Assert.AreEqual(factory.now.Now(), ToolBelt.DelayTimerSetAt);
    }

    [Test]
    public void closes_the_tool_belt_having_hide_delayed_elapsed()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["BackpackOnClose"] = false,
            ["toolBeltIsVisible"] = true,
            ["hideDelayElapsed"] = true,
        });
        ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
        factory.toolBeltMock.VerifySet(h => h.ForceHide = true);
        factory.toolBeltMock.VerifySet(h => h.IsVisible = false);
        Assert.AreEqual(DateTime.MinValue, ToolBelt.DelayTimerSetAt);
    }
    
    [Test]
    public void does_not_close_the_tool_belt_having_delay_timer_not_elapse()
    {
        var factory = Factory.Create(new Dictionary<string, object>
        {
            ["BackpackOnClose"] = false,
            ["toolBeltIsVisible"] = true,
            ["hideDelayElapsed"] = false,
        });
        ToolBelt.Wrapper(factory.toolBeltMock.Object, factory.now);
        factory.toolBeltMock.VerifyNoOtherCalls();
    }
    
}