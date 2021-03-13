using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoriesNavigationController : MonoBehaviour
{
    [SerializeField]
    private List<Inventory> inventories;

    [SerializeField]
    private float maxLength = 100;

    public void Start()
    {

        //inventories = GameStateController.gameStateController.inventories;
    }

    public void UpdateInventories(List<Inventory> inventories)
    {
        this.inventories = inventories;
    }

    public Inventory FindInventory_Closest(Vector3 globalStartPos, Vector2Int dir,  Inventory curent, out Vector2Int outClosestPos)
    {
        float minValue = float.MaxValue;
        Inventory minInv = null;
        outClosestPos = new Vector2Int(-1, -1);
        foreach (Inventory inventory in inventories)
        {
            if(inventory != curent)
            {
                Vector2Int nearestCell = CLampInventoryVec(inventory.WorldToCell(globalStartPos), inventory);
                Vector3 nearestCellPosInWorld = inventory.CellToWorld(nearestCell);
                float value = Vector3.Distance(nearestCellPosInWorld, globalStartPos);

                if(IsPositionInDirection(globalStartPos, nearestCellPosInWorld, dir))
                if(value <= minValue)
                {
                    outClosestPos = nearestCell;
                    minValue = value;
                    minInv = inventory;
                }
            }
        }
        
        return minInv;
    }

    public bool IsPositionInDirection(Vector3 posStart, Vector3 posEnd, Vector2Int dir)
    {
        Vector3 pos = posEnd - posStart;

        if(dir.x != 0)
        {
            Vector3 nPos = new Vector3(pos.x, 0);
            Vector3 norm = nPos.normalized;
            if(norm.x == dir.x && Mathf.Abs(pos.y) <= maxLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Vector3 nPos = new Vector3(0, pos.y);
            Vector3 norm = nPos.normalized;
            if (norm.x == dir.x && Mathf.Abs(pos.y) <= maxLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    public Inventory GetInventoryForPosition(Vector3 pos)
    {
        foreach(Inventory inventory in inventories)
        {
            if (inventory.IsInBounds(inventory.WorldToCell(pos)))
            {
                return inventory;
            }
        }

        return null;
    }

    //public Inventory FindInventory_Far(Vector3 globalStartPos, Inventory curent, out Vector2Int outFarPos)
    //{
    //    float maxValue = 0;
    //    Inventory maxInv = null;
    //    outFarPos = new Vector2Int(-1, -1);
    //    foreach (Inventory inventory in inventories)
    //    {

    //        Vector2Int nearestCell = CLampInventoryVec(inventory.WorldToCell(globalStartPos), inventory);
    //        Vector3 nearestCellPosInWorld = inventory.CellToWorld(nearestCell);
    //        float value = Vector3.Distance(nearestCellPosInWorld, globalStartPos);

    //        if (value <= minValue)
    //        {
    //            outClosestPos = nearestCell;
    //            minValue = value;
    //            minInv = inventory;
    //        }

    //    }

    //    return minInv;
    //}

    private Vector2Int CLampInventoryVec(Vector2Int vec, Inventory inventory)
    {
        return new Vector2Int(Mathf.Clamp(vec.x, 0, inventory.inventorySize.x - 1), Mathf.Clamp(vec.y, 0, inventory.inventorySize.y - 1));
    }



    
}
