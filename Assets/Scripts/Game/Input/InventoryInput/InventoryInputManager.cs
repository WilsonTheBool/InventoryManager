using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ItemTransferControler))]
public class InventoryInputManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerInputController PlayerInputController;

    [HideInInspector]
    public GameStateController gameStateController;

    [HideInInspector]
    public BaseInputHandler itemTransferController;
    [HideInInspector]
    public BaseInputHandler itemSellInputController;

    private BaseInputHandler curentInputHandler;
    private void Start()
    {
        gameStateController = GameStateController.gameStateController;
        PlayerInputController = gameStateController.PlayerInputController;
        itemTransferController = GetComponent<ItemTransferControler>();
        itemSellInputController = GetComponent<ItemSellInputController>();

        PlayerInputController.OnMoveKeyPressed += PlayerInputController_OnMoveKeyPressed;
        PlayerInputController.OnPickUpKeyPressed += PlayerInputController_OnPickUpKeyPressed;
        PlayerInputController.OnRotationKeyPressed += PlayerInputController_OnRotationKeyPressed;
        PlayerInputController.OnUseKeyPressed += PlayerInputController_OnUseKeyPressed;

        

        gameStateController.OnShopOpened += GameStateController_OnShopOpened;
        gameStateController.OnShopClosed += GameStateController_OnShopClosed;

        curentInputHandler = itemTransferController;
    }

    private void GameStateController_OnShopClosed()
    {
        curentInputHandler = itemTransferController;
    }

    private void GameStateController_OnShopOpened()
    {
        curentInputHandler = itemSellInputController;
    }

    private void PlayerInputController_OnMoveKeyPressed(Vector2Int obj)
    {
        curentInputHandler.HandleInput(new BaseInputHandler.InputArgs(obj));
    }

    private void PlayerInputController_OnPickUpKeyPressed()
    {
        curentInputHandler.HandleInput(new BaseInputHandler.InputArgs(BaseInputHandler.InputArgs.InputType.pickup));
    }

    private void PlayerInputController_OnRotationKeyPressed()
    {
        curentInputHandler.HandleInput(new BaseInputHandler.InputArgs(BaseInputHandler.InputArgs.InputType.rotate));
    }

    private void PlayerInputController_OnUseKeyPressed()
    {
        curentInputHandler.HandleInput(new BaseInputHandler.InputArgs(BaseInputHandler.InputArgs.InputType.use));
    }
}
