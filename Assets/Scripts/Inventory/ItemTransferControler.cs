using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ItemTransferControler : BaseInputHandler
{
    public Inventory inventory;
    public PlayerInputController PlayerInputController;
    public CurentItemSelectController ItemSelectController;
    public SoundManager SoundManager;
    public InventoriesNavigationController navigationController;

    [SerializeField]
    private ItemInfoController ItemInfoController;

    public Item holdItem;

    public Vector2Int curentPos;

    private Vector2Int originalItemPos;

    [SerializeField]
    private Canvas mainCanvas;

    [SerializeField]
    private Canvas pickUpCanvas;

    [SerializeField]
    private Canvas lowerCanvas;

    GameStateController gameStateController;
    public void PickUpItem(Item item)
    {
        if(holdItem == null)
        {
            holdItem = item;
            OnItemPickUp(item);
            inventory.PickUpItem(item);
            originalItemPos = item.OriginPos;

        }
        
    }

    private void Start()
    {
        gameStateController = GameStateController.gameStateController;

        gameStateController.OnInventoryOpened += GameStateController_OnInventoryOpened;
        gameStateController.OnInventoryClosed += GameStateController_OnInventoryClosed;

        inventory = GetComponent<Inventory>();
    }

    public override void HandleInput(InputArgs args)
    {
        switch (args.type)
        {
            case InputArgs.InputType.movement:
                {
                    PlayerInputController_OnMoveKeyPressed(args.direction);
                    return;
                }
            case InputArgs.InputType.pickup:
                {
                    PlayerInputController_OnPickUpKeyPressed();
                    return;
                }
            case InputArgs.InputType.rotate:
                {
                    PlayerInputController_OnRotationKeyPressed();
                    return;
                }
            case InputArgs.InputType.use:
                {
                    PlayerInputController_OnUseKeyPressed();
                    return;
                }

        }
    }

    private void UpdateItemInfo()
    {
        IInventoryItem item = inventory.GetCell(curentPos);
        if (holdItem == null && item != null)
        {
            ItemInfoController.ShowItemInfo(item.GetInfo(), item.GetCost(), inventory.CellToWorld(curentPos), item.Size);
        }
        else
        {
            ItemInfoController.HideItemInfo();
        }
        
    }

    private void PlayerInputController_OnUseKeyPressed()
    {
        if(holdItem == null)
        {
            UseCurentItem();
        }
        else
        {
            UseHoldItem();
        }
        
    }

    private void UseHoldItem()
    {
        if (holdItem is IUsableItem item)
        {
            item.UseItem(gameStateController.player);
            Item item1 = item as Item;
            Destroy(item1.gameObject);
        }
    }

    private void UseCurentItem()
    {
        if (inventory.GetCell(curentPos) is IUsableItem item)
        {

            item.UseItem(gameStateController.player);
            inventory.RemoveItem(item as IInventoryItem);
            Item item1 = item as Item;
            Destroy(item1.gameObject);

            ItemSelectController.UpdateSelect(inventory.CellCenter(curentPos));
            ItemInfoController.HideItemInfo();
            UpdareCurentSelect();
        }
    }

    public bool CanCloseInventories()
    {
        return holdItem == null;
    }

    private void GameStateController_OnInventoryOpened(List<Inventory> obj)
    {
        ItemSelectController.gameObject.SetActive(true);
        
    }

    private void GameStateController_OnInventoryClosed(List<Inventory> obj)
    {
        ItemSelectController.gameObject.SetActive(false);
        ItemInfoController.HideItemInfo();
    }

    private void PlayerInputController_OnRotationKeyPressed()
    {
        if(isActiveAndEnabled)
        RotateItem();
    }

    private void PlayerInputController_OnPickUpKeyPressed()
    {
        if (isActiveAndEnabled)
        {
            if (holdItem == null)
            {
                TryPickUpItem();
            }
            else
            {
                TryPlaceItem();
            }
        }
        
    }

    private void RotateItem()
    {
        if(holdItem != null && holdItem.Size.x != holdItem.Size.y)
        {
            holdItem.RotateItem(90);
            holdItem.transform.Rotate(new Vector3(0, 0, 1), 90);

            ItemSelectController.UpdateSelect(holdItem);

            SoundManager.PlaySoundByName("Item_Rotate");
        }
    }

    public void PlayerInputController_OnMoveKeyPressed(Vector2Int vec)
    {
        if (isActiveAndEnabled)
        {
            SoundManager.PlaySoundByName("Move");

            Move(vec);
        }
       
    }

    private void UpdareCurentSelect()
    {
        IInventoryItem item = inventory.GetCell(curentPos);
        if (item != null && item is Item item1)
        {
            ItemSelectController.UpdateSelect(item1);
        }
        else
        {
            ItemSelectController.ResetSize();
        }
    }

    private void TryPlaceItem()
    {
        PlaceItem();
    }

    private void PlaceItem()
    {
        IInventoryItem[] items = inventory.GetAllIntersectingItems(holdItem);

        if(items.Length == 0)
        {
            OnItemPlaced(holdItem);

            inventory.DropItem(holdItem);
            holdItem = null;
        }
        else
        {
            if(items.Length == 1)
            {
                Item saved = holdItem;
                holdItem = null;
                PickUpItem(items[0] as Item);

                OnItemPlaced(saved);
                inventory.DropItem(saved);
            }
        }
       
        
    }

    [SerializeField]
    public float pickUpDisplace = 0.2f;
    private void TryPickUpItem()
    {
        IInventoryItem item = inventory.GetCell(curentPos);

        if(item != null && item is Item item1)
        {
            PickUpItem((item1));
        }

        UpdateItemInfo();
    }

    private void OnItemPlaced(Item item)
    {
        item.transform.position -= new Vector3(0, pickUpDisplace, -pickUpDisplace);

        SoundManager.PlaySoundByName("Item_Drop");

        ItemSelectController.transform.SetParent(inventory.selectTileHolder.transform);
        item.transform.SetParent(inventory.itemsHolder.transform);

        UpdateItemInfo();
    }
    private void OnItemPickUp(Item item)
    {
        //ItemSelectController.UpdateSelect(item);
        item.transform.position += new Vector3(0, pickUpDisplace, -pickUpDisplace);

        SoundManager.PlaySoundByName("Item_PickUp");

        ItemSelectController.transform.SetParent(pickUpCanvas.transform);
        item.transform.SetParent(pickUpCanvas.transform);
        
    }

    private void Move(Vector2Int dir)
    {
        if(holdItem != null)
        {
            Move_Item(dir);
        }
        else
        {
            Move_Empty(dir);
            UpdateItemInfo();
        }

        OnMove();
    }

    private void Move_Item(Vector2Int dir)
    {
        holdItem.Translate(dir);
        if (inventory.IsInBounds(holdItem))
        {
            holdItem.Translate(-dir);
            Move_Item_Simple(dir);
        }
        else
        {
            holdItem.Translate(-dir);
            Move_Item_OutOfBounds(dir);
            
        }
    }

    private void Move_Item_OutOfBounds(Vector2Int dir)
    {
        Move_Item_FindNewInv(dir);
    }

    private void Move_Item_Simple(Vector2Int dir)
    {
        holdItem.Translate(dir);
        Vector3 vec = inventory.cellSize* dir;
        holdItem.transform.position += vec;
    }

    private void Move_Item_FindNewInv(Vector2Int dir)
    {
        Inventory newInv = navigationController.FindInventory_Closest(inventory.CellCenter(curentPos), dir, inventory, out Vector2Int closest);

        
        if(newInv != null)
        {
            Inventory old = inventory;
            inventory = newInv;
            holdItem.inventory = newInv;

            Vector2Int newPos = Move_Item_FindNewPosForItem_NewInventory(closest, dir, holdItem);

            if (inventory.IsInBounds(newPos))
            {
                curentPos = newPos;
                holdItem.OriginPos = newPos;
                holdItem.SetGlobalPos(newPos, pickUpDisplace);
            }
            else
            {
                inventory = old;
                holdItem.inventory = old;
            }
            
        }
    }

    private Vector2Int Move_Item_FindNewPosForItem_NewInventory(Vector2Int searchStart, Vector2Int dir, IInventoryItem item)
    {
        Vector2Int oldPos = item.OriginPos;

        item.OriginPos = searchStart;
        while (!inventory.IsInBounds(item) && inventory.IsInBounds(item.OriginPos))
        {
            item.Translate(dir);
        }

        Vector2Int newPos = item.OriginPos;
        item.OriginPos = oldPos;

        return newPos;
    }

    private void Move_Empty(Vector2Int dir)
    {
        IInventoryItem item = inventory.GetCell(curentPos);

        if(item != null)
        {
            Move_Empty_HasItem(dir, item);
        }
        else
        {
            Move_Empty_NoItem(dir);
        }
        
        
    }

    private void Move_Empty_NoItem(Vector2Int dir)
    {
        if (inventory.IsInBounds(curentPos + dir))
        {

            if (Move_Empty_TryFindNextItem(dir, out Vector2Int foundPos))
            {
                curentPos = foundPos;
            }
        }
        else
        {
            Move_Empty_FindNewInventory(dir);
            ItemSelectController.transform.SetParent(inventory.selectTileHolder.transform);
        }
    }

    private void Move_Empty_HasItem(Vector2Int dir, IInventoryItem item)
    {
        item.OriginPos += dir;
        if (inventory.IsInBounds(curentPos + dir) && inventory.IsInBounds(item))
        {
            item.OriginPos -= dir;
            if (Move_Empty_TryFindNextItem(dir, out Vector2Int foundPos))
            {
                curentPos = foundPos;
            }

        }
        else
        {
            item.OriginPos -= dir;
            
            Move_Empty_FindNewInventory(dir);
            ItemSelectController.transform.SetParent(inventory.selectTileHolder.transform);
        }
    }

    private void Move_Empty_FindNewInventory(Vector2Int dir)
    {
        Inventory newInv = navigationController.FindInventory_Closest(inventory.CellCenter(curentPos), dir, inventory, out Vector2Int closest);


        if (newInv != null)
        {
            inventory = newInv;
            curentPos = closest;
           
        }
    }

    private bool Move_Empty_TryFindNextItem(Vector2Int dir, out Vector2Int foundPos)
    {
        Vector2Int curent = curentPos + dir;
        IInventoryItem start = inventory.GetCell(curentPos);

        while (inventory.IsInBounds(curent))
        {
            IInventoryItem found = inventory.GetCell(curent);
            if (found != start || found == null)
            {
                if(found == null)
                {
                    foundPos = curent;
                    
                }
                else
                {
                    foundPos = found.OriginPos;
                }
               
                return true;
            }
            curent += dir;
        }

        foundPos = curentPos;
        return false;
    }

    private void OnMove()
    {
        if(holdItem != null)
        {
            ItemSelectController.UpdateSelect(holdItem);
            
            curentPos = holdItem.OriginPos;

            
        }
        else
        {
            ItemSelectController.UpdateSelect(inventory.CellCenter(curentPos));
            UpdareCurentSelect();
        }
        
       
    }

    public void DeleteItem(Item item)
    {
        item.inventory.RemoveItem(item);
        ItemInfoController.HideItemInfo();
        Destroy(item.gameObject);

    }

    public void TrySellItem()
    {
        if(holdItem == null)
        {
            IInventoryItem item = inventory.GetCell(curentPos);
            if (item != null && item is ISellableItem sellableItem)
            {
                SellItem(sellableItem);
            }
        }
    }

    private void SellItem(ISellableItem sellableItem)
    {
        sellableItem.OnItemSell(gameStateController.player);

        DeleteItem(sellableItem as Item);

        SoundManager.PlaySoundByName("Item_Sold");
    }
}

