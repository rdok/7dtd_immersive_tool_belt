// using System.Collections.Generic;
// using ImmersiveToolBelt.Harmony;
// using Moq;
// using NUnit.Framework;
//
// namespace UnitTests.Harmony;
//
// [TestFixture]
// public class ImmersiveToolBeltInitTests
// {
//
//     [Test]
//     public void it_checks_if_backpack_is_visible()
//     {
//         var factory = Factory.Create(new Dictionary<string, object>
//         {
//             ["bPlayerStatsChanged"] = false,
//             ["PlayerUI"] = true,
//             ["hasXui"] = true,
//             ["hasToolBeltWindowGroup"] = true,
//             ["hasToolBeltWindow"] = true,
//             ["hasWindowManager"] = true,
//             ["hasViewComponent"] = true
//         });
//         ToolBelt.Wrapper(factory.entityPlayerLocalMock.Object, factory.now);
//         factory.windowManagerMock.Verify(h => h.IsWindowOpen("backpack"));
//     }
//
//     [Test]
//     public void it_hides_the_tool_belt_having_backpack_hidden_and_tool_belt_visible()
//     {
//         var factory = Factory.Create(new Dictionary<string, object>
//         {
//             ["bPlayerStatsChanged"] = false,
//             ["PlayerUI"] = true,
//             ["hasXui"] = true,
//             ["hasToolBeltWindowGroup"] = true,
//             ["hasToolBeltWindow"] = true,
//             ["hasWindowManager"] = true,
//             ["hasViewComponent"] = true,
//             ["isBackpackVisible"] = false,
//             ["isToolBeltVisible"] = true,
//             ["hideDelayElapsed"] = true
//         });
//         ToolBelt.Wrapper(factory.entityPlayerLocalMock.Object, factory.now);
//         factory.viewComponentMock.VerifySet(h => h.ForceHide = true);
//         factory.viewComponentMock.VerifySet(h => h.IsVisible = false);
//     }
//
//     [Test]
//     public void it_shows_the_tool_belt_window_having_backpack_visible_and_tool_belt_hidden()
//     {
//         var factory = Factory.Create(new Dictionary<string, object>
//         {
//             ["bPlayerStatsChanged"] = false,
//             ["PlayerUI"] = true,
//             ["hasXui"] = true,
//             ["hasToolBeltWindowGroup"] = true,
//             ["hasToolBeltWindow"] = true,
//             ["hasWindowManager"] = true,
//             ["hasViewComponent"] = true,
//             ["isBackpackVisible"] = true,
//             ["isToolBeltVisible"] = false,
//             ["hideDelayElapsed"] = true
//         });
//         ToolBelt.Wrapper(factory.entityPlayerLocalMock.Object, factory.now);
//         factory.viewComponentMock.VerifySet(h => h.ForceHide = false);
//         factory.viewComponentMock.VerifySet(h => h.IsVisible = true);
//     }
//     
//     [Test]
//     public void it_does_not_show_the_tool_belt_having_backpack_hidden_less_than_the_delay_threshold()
//     {
//         var factory = Factory.Create(new Dictionary<string, object>
//         {
//             ["bPlayerStatsChanged"] = false,
//             ["PlayerUI"] = true,
//             ["hasXui"] = true,
//             ["hasToolBeltWindowGroup"] = true,
//             ["hasToolBeltWindow"] = true,
//             ["hasWindowManager"] = true,
//             ["hasViewComponent"] = true,
//             ["isBackpackVisible"] = false,
//             ["isToolBeltVisible"] = true,
//             ["hideDelayElapsed"] = false
//         });
//         ToolBelt.Wrapper(factory.entityPlayerLocalMock.Object, factory.now);
//         factory.viewComponentMock.VerifySet(v => v.IsVisible = It.IsAny<bool>(), Times.Never());
//     }
//
//     [Test]
//     public void it_keeps_concealing_the_tool_belt_after_the_first_one()
//     {
//         var factory = Factory.Create(new Dictionary<string, object>
//         {
//             ["bPlayerStatsChanged"] = false,
//             ["PlayerUI"] = true,
//             ["hasXui"] = true,
//             ["hasToolBeltWindowGroup"] = true,
//             ["hasToolBeltWindow"] = true,
//             ["hasWindowManager"] = true,
//             ["hasViewComponent"] = true,
//             ["isBackpackVisible"] = false,
//             ["isToolBeltVisible"] = true,
//             ["hideDelayElapsed"] = true
//         });
//         ToolBelt.Wrapper(factory.entityPlayerLocalMock.Object, factory.now);
//         ToolBelt.Wrapper(factory.entityPlayerLocalMock.Object, factory.now);
//         factory.viewComponentMock.VerifySet(v => v.IsVisible = false, Times.Exactly(1));
//         factory.viewComponentMock.VerifySet(v => v.ForceHide = true, Times.Exactly(1));
//     }
// }