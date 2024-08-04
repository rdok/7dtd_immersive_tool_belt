using System.Collections.Generic;
using ImmersiveToolBelt.Harmony;
using ImmersiveToolBelt.Harmony.Seams;
using Moq;

namespace UnitTests.Harmony;

public static class Factory
{
    public static (Mock<IEntityPlayerLocal> playerLocal, Mock<ILocalPlayerUI> playerUI) CreatePostfixFactory(
        Dictionary<string, object> parameters = null)
    {
//         var hitInfo = new Mock<IWorldRayHitInfo>();
//         var inventory = new Mock<ImmersiveToolBelt.Harmony.IInventory>();
//         var itemClassMock = new Mock<IItemClass>();
//         var hit = new Mock<IHitInfoDetails>();
//         var holdingItemItemValue = new Mock<IItemValue>();
//         var logger = new Mock<ILogger>();
//
//         var hasHud = parameters.ContainsKey("HasHud") && (bool)parameters["HasHud"];
//         var hasHitInfo = parameters.ContainsKey("HitInfo") && (bool)parameters["HitInfo"];
//         var hasBHitValid = parameters.ContainsKey("bHitValid") && (bool)parameters["bHitValid"];
//         var distanceSq = parameters.ContainsKey("hit.distanceSq") ? (float)parameters["hit.distanceSq"] : 1;
//
//         holdingItemItemValue.Setup(p => p.ItemClass).Returns(itemClassMock.Object);
//         inventory.Setup(p => p.holdingItemItemValue)
//             .Returns(holdingItemItemValue.Object);
//         playerUI.Setup(p => p.GetComponentInChildren<IGuiWdwInGameHUD>())
//             .Returns(hasHud ? hudMock.Object : null);
//         playerLocal.Setup(p => p.HitInfo)
//             .Returns(hasHitInfo ? hitInfo.Object : null);
//         hitInfo.Setup(p => p.bHitValid).Returns(hasBHitValid);
//         hitInfo.Setup(p => p.hit).Returns(hit.Object);
//         hit.Setup(p => p.distanceSq).Returns(distanceSq);
//
//         var actions = new List<IItemAction>();
//
//         var itemActionRanged = new Mock<IItemActionRanged>();
//         var holdingRanged = parameters.ContainsKey("holdingRanged") && (bool)parameters["holdingRanged"];
//         itemActionRanged.Setup(p => p.IsRanged).Returns(holdingRanged);
//         if (holdingRanged) actions.Add(itemActionRanged.Object);
//
//         var itemActionRepair = new Mock<IItemActionRepair>();
//         var holdingRepair = parameters.ContainsKey("holdingRepair") && (bool)parameters["holdingRepair"];
//         itemActionRepair.Setup(p => p.IsRepair).Returns(holdingRepair);
//         if (holdingRepair) actions.Add(itemActionRepair.Object);
//
//         var itemActionHarvest = new Mock<IItemActionHarvest>();
//         var holdingHarvest =
//             parameters.ContainsKey("holdingHarvest") && (bool)parameters["holdingHarvest"];
//         itemActionHarvest.Setup(p => p.IsHarvest).Returns(holdingHarvest);
//         if (holdingHarvest) actions.Add(itemActionHarvest.Object);
//
//         var itemActionSalvage = new Mock<IItemActionSalvage>();
//         var holdingSalvage =
//             parameters.ContainsKey("holdingSalvage") && (bool)parameters["holdingSalvage"];
//         itemActionSalvage.Setup(p => p.IsSalvage).Returns(holdingSalvage);
//         if (holdingSalvage) actions.Add(itemActionSalvage.Object);
//
//         var itemActionBareHands = new Mock<IItemActionBareHands>();
//         itemActionBareHands.Setup(p => p.IsBareHands).Returns(holdingBareHands);
//         if (holdingBareHands) actions.Add(itemActionBareHands.Object);
//
//         itemClassMock.Setup(p => p.Actions).Returns(actions.ToArray());
//
        var playerLocal = new Mock<IEntityPlayerLocal>();
        var playerUI = new Mock<ILocalPlayerUI>();
        playerLocal.Setup(p => p.PlayerUI).Returns(playerUI.Object);

        if (parameters == null) return (playerLocal, playerUI);

        var bPlayerStatsChanged =
            parameters.ContainsKey("bPlayerStatsChanged") && (bool)parameters["bPlayerStatsChanged"];
        playerLocal.Setup(p => p.bPlayerStatsChanged).Returns(bPlayerStatsChanged);
        
        var logger = new Mock<ILogger>();

        ImmersiveToolBelt.Harmony.ToolBelt.SetLogger(logger.Object);
//
        return (playerLocal, playerUI);
//     }
    }
}