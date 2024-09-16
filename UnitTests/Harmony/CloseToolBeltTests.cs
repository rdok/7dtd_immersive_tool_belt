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
        var factory = ToolBeltFactory.Create(new Dictionary<string, object> { ["BackpackOnClose"] = true });
        factory.ToolBelt.DelayTimerSetAt = DateTime.MinValue;
        factory.ToolBelt.Update(factory.XUiViewMock.Object, factory.DateTime);
        Assert.AreEqual(factory.DateTime.Now(), factory.ToolBelt.DelayTimerSetAt);
    }

    // [Test]
    // public void closes_the_tool_belt_having_hide_delayed_elapsed()
    // {
    //     var factory = ToolBeltFactory.Create(new Dictionary<string, object>
    //     {
    //         ["BackpackOnClose"] = false,
    //         ["toolBeltIsVisible"] = true,
    //         ["hideDelayElapsed"] = true
    //     });
    //     ToolBelt.Update(factory.XUiViewMock.Object, factory.Now);
    //     factory.XUiViewMock.VerifySet(h => h.ForceHide = true);
    //     factory.XUiViewMock.VerifySet(h => h.IsVisible = false);
    //     Assert.AreEqual(DateTime.MinValue, ToolBelt.DelayTimerSetAt);
    // }
    //
    // [Test]
    // public void does_not_close_the_tool_belt_having_delay_timer_not_elapse()
    // {
    //     var factory = ToolBeltFactory.Create(new Dictionary<string, object>
    //     {
    //         ["BackpackOnClose"] = false,
    //         ["toolBeltIsVisible"] = true,
    //         ["hideDelayElapsed"] = false
    //     });
    //     ToolBelt.Update(factory.XUiViewMock.Object, factory.Now);
    //     factory.XUiViewMock.VerifyNoOtherCalls();
    // }
}