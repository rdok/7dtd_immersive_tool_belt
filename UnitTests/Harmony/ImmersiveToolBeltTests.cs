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
        var (player, _) = Factory.CreatePostfixFactory();
        ImmersiveToolBelt.Harmony.ToolBelt.UpdateVisibility(null);
        player.VerifyNoOtherCalls();
    }

    [Test]
    public void it_skis_update_having_changed_player_stats()
    {
        var (player, playerUI) = Factory.CreatePostfixFactory(new Dictionary<string, object>
        {
            ["bPlayerStatsChanged"] = true
        });
        ImmersiveToolBelt.Harmony.ToolBelt.UpdateVisibility(player.Object);
        playerUI.VerifyNoOtherCalls();
    }

//     [Test]
//     public void it_disables_crosshair_holding_a_ranged_weapon()
//     {
//         var (playerLocalMock, hudMock) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//             {
//                 { "HasHud", true },
//                 { "HitInfo", true },
//                 { "bHitValid", true },
//                 { "holdingRanged", true },
//                 { "holdingHarvest", false },
//                 { "holdingRepair", false }
//             }
//         );
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocalMock.Object);
//
//         hudMock.VerifySet(h => h.showCrosshair = false, Times.Once);
//     }
//
//     [Test]
//     public void it_does_not_change_crosshair_having_no_hit_info()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "holdingRanged", false },
//             { "HitInfo", false },
//             { "holdingHarvest", true },
//             { "holdingRepair", false }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifyNoOtherCalls();
//     }
//
//     [Test]
//     public void it_enables_crosshair_having_an_interactable_in_distance_while_holding_repair_tool()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "holdingRanged", false },
//             { "holdingRepair", true },
//             { "holdingHarvest", false },
//             { "HitInfo", true },
//             { "bHitValid", true },
//             { "hit.distanceSq", MinimumInteractableDistance }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifySet(h => h.showCrosshair = true, Times.Once);
//     }
//
//     [Test]
//     public void it_enables_crosshair_having_an_interactable_in_distance_while_holding_harvest_tool()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "holdingRanged", false },
//             { "holdingHarvest", true },
//             { "HitInfo", true },
//             { "bHitValid", true },
//             { "hit.distanceSq", MinimumInteractableDistance }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifySet(h => h.showCrosshair = true, Times.Once);
//     }
//
//     [Test]
//     public void it_disables_crosshair_having_no_interactable_in_distance()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "HitInfo", true },
//             { "bHitValid", true },
//             { "holdingRanged", false },
//             { "holdingRepair", true },
//             { "hit.distanceSq", MinimumInteractableDistance + .1f }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifySet(h => h.showCrosshair = false, Times.Once);
//     }
//
//     [Test]
//     public void it_hides_the_crosshair_holding_a_non_interactable_item()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "HitInfo", true },
//             { "bHitValid", true },
//             { "holdingRanged", false },
//             { "holdingHarvest", false },
//             { "holdingRepair", false },
//             { "holdingSalvage", false }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifySet(h => h.showCrosshair = false, Times.Once);
//     }
//
//     [Test]
//     public void it_enables_crosshair_having_an_interactable_in_distance_while_holding_salvage_tool()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "holdingRanged", false },
//             { "holdingSalvage", true },
//             { "HitInfo", true },
//             { "bHitValid", true },
//             { "hit.distanceSq", MinimumInteractableDistance }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifySet(h => h.showCrosshair = true, Times.Once);
//     }
//
//     [Test]
//     public void it_enables_crosshair_having_an_interactable_in_distance_while_bare_hands_tool()
//     {
//         var (playerLocal, hud) = Factory.CreatePostfixFactory(new Dictionary<string, object>
//         {
//             { "HasHud", true },
//             { "holdingRanged", false },
//             { "holdingSalvage", false },
//             { "holdingBareHands", true },
//             { "HitInfo", true },
//             { "bHitValid", true },
//             { "hit.distanceSq", MinimumInteractableDistance }
//         });
//
//         ImmersiveToolBelt.Harmony.Main.ApplyPatch(playerLocal.Object);
//
//         hud.VerifySet(h => h.showCrosshair = true, Times.Once);
//     }
}