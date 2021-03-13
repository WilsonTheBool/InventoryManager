using System;
using System.Collections.Generic;
using UnityEngine;
public class Action_LoadNewInventory : EventAction
{
    [SerializeField]
    Inventory inventoryPrefab;

    private Inventory savedInstanse;
    public override void DoAction(Player p, EventWindow e)
    {
        GameStateController gameStateController = GameStateController.gameStateController;

        savedInstanse = Instantiate<Inventory>(inventoryPrefab, gameStateController.inventoryHolders[0]);
        gameStateController.AddInventory(savedInstanse);
        gameStateController.OnInventoryClosed += GameStateController_OnInventoryClosed;
        gameStateController.OpenInventories();
    }

    private void GameStateController_OnInventoryClosed(List<Inventory> obj)
    {
        GameStateController gameStateController = GameStateController.gameStateController;
        gameStateController.OnInventoryClosed -= GameStateController_OnInventoryClosed;
        gameStateController.RemoveInventory(savedInstanse);
    }
}

