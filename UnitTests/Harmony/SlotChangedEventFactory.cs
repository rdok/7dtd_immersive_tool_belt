using System.Collections.Generic;
using ImmersiveToolBelt.Harmony.Interfaces;
using Moq;

namespace UnitTests.Harmony;

public static class SlotChangedEventFactory
{
    public static Dictionary<string, object> Input => new()
    {
        { "HasPlayerInput", true },
        { "InventorySlotWasPressed", false },
        { "InventorySlotLeftWasPressed", false },
        { "InventorySlotRightWasPressed", false }
    };

    public static Mock<IEntityPlayerLocal> Create(Dictionary<string, object> parameters)
    {
        var entityPlayerLocalMock = new Mock<IEntityPlayerLocal>();
        var playerInputMock = new Mock<IPlayerActionsLocal>();

        entityPlayerLocalMock.Setup(p => p.playerInput).Returns(playerInputMock.Object);

        if (parameters == null) return entityPlayerLocalMock;

        var hasPlayerInput =
            parameters.ContainsKey("HasPlayerInput") &&
            (bool)parameters["HasPlayerInput"];
        entityPlayerLocalMock.Setup(p => p.playerInput).Returns(hasPlayerInput ? playerInputMock.Object : null);

        var inventorySlotWasPressed =
            parameters.ContainsKey("InventorySlotWasPressed") &&
            (bool)parameters["InventorySlotWasPressed"];
        playerInputMock.Setup(p => p.InventorySlotWasPressed).Returns(inventorySlotWasPressed ? 1 : -1);

        var inventorySlotLeftWasPressed =
            parameters.ContainsKey("InventorySlotLeftWasPressed") &&
            (bool)parameters["InventorySlotLeftWasPressed"];
        playerInputMock.Setup(p => p.InventorySlotLeft.WasPressed).Returns(inventorySlotLeftWasPressed);

        var inventorySlotRightWasPressed = parameters.ContainsKey("InventorySlotRightWasPressed") &&
                                           (bool)parameters["InventorySlotRightWasPressed"];
        playerInputMock.Setup(p => p.InventorySlotRight.WasPressed).Returns(inventorySlotRightWasPressed);

        return entityPlayerLocalMock;
    }
}