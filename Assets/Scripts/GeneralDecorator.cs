using UnityEngine;
using System.Collections;

public class GeneralDecorator : MonoBehaviour
{

    [SerializeField]
    GameObject inventoryBackground;

    GameStateController gameStateController;

    private void Start()
    {
        gameStateController = GameStateController.gameStateController;
        gameStateController.OnInventoryOpened += GameStateController_OnInventoryOpened;
        gameStateController.OnInventoryClosed += GameStateController_OnInventoryClosed;
    }

    private void GameStateController_OnInventoryClosed(System.Collections.Generic.List<Inventory> obj)
    {
        inventoryBackground.SetActive(false);
    }

    private void GameStateController_OnInventoryOpened(System.Collections.Generic.List<Inventory> obj)
    {
        inventoryBackground.SetActive(true);
    }
}
