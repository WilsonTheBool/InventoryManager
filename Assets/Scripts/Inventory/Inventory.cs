using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public Vector2Int inventorySize;

    public IInventoryItem[,] allCells;

    public Vector2 cellSize;
    public Vector2 cellOffset;

    public  event EventHandler<ItemEvent> OnItemPlaced;

    public Transform itemsHolder;
    public Transform selectTileHolder;

    public void PickUpItem(IInventoryItem item)
    {
        foreach (Vector2Int pos in item.GetGloablPositions())
        {
            SetCell(pos, null);
        }
    }

    public void DropItem(IInventoryItem item)
    {
        if (CanPlaceItem(item))
        {
            foreach (Vector2Int pos in item.GetGloablPositions())
            {
                SetCell(pos, item);
            }

            OnItemPlaced?.Invoke(this, new ItemEvent(item));
        }


    }

    public void RemoveItem(IInventoryItem item)
    {
        foreach(Vector2Int pos in item.GetGloablPositions())
        {
            SetCell(pos, null);
        }
    }

    public void SetCell(Vector2Int pos, IInventoryItem value)
    {
        if(IsInBounds(pos))
        allCells[pos.x, pos.y] = value;
    }

    public IInventoryItem GetCell(Vector2Int pos)
    {
        if (IsInBounds(pos))
            return allCells[pos.x, pos.y];
        return null;
    }

    public bool CanPlaceItem(IInventoryItem item)
    {
        foreach(Vector2Int pos in item.GetGloablPositions())
        {
            if (!IsCellFree(pos))
            {
                return false;
            }
        }

        return true;
    }

    public IInventoryItem[] GetAllIntersectingItems(IInventoryItem item)
    {
        List<IInventoryItem> list = new List<IInventoryItem>();

        foreach(Vector2Int pos in item.GetGloablPositions())
        {
            IInventoryItem inter = GetCell(pos);

            if(inter != null && !list.Contains(inter))
            {
                list.Add(inter);
            }
        }

        return list.ToArray();
    }

    public bool TryFindNextPosition(Vector2Int searchStart, Vector2Int direction, bool overflow, out Vector2Int resultPos)
    {
        IInventoryItem item = GetCell(searchStart);

        if(item != null)
        {
            Vector2Int savedPos = item.OriginPos;

            Vector2Int curentPos = searchStart + direction;
            item.OriginPos = curentPos;
            while (IsInBounds(item))
            {
                if (GetCell(curentPos) != item || GetCell(curentPos) == null)
                {
                    resultPos = curentPos;
                    return true;
                }

                curentPos += direction;
                item.OriginPos = curentPos;
            }

            item.OriginPos = savedPos;

            if (overflow)
            {
                curentPos -= direction * (inventorySize);

                if (GetCell(curentPos) != item || GetCell(curentPos) == null)
                {
                    resultPos = curentPos;
                    return true;
                }
            }

            resultPos = curentPos;
            return false;
        }
        else
        {
            

            Vector2Int curentPos = searchStart + direction;
           
            while (IsInBounds(curentPos))
            {
                if (GetCell(curentPos) != item || GetCell(curentPos) == null)
                {
                    resultPos = curentPos;
                    return true;
                }

                curentPos += direction;
                
            }



            if (overflow)
            {
                curentPos -= direction * (inventorySize);

                if (GetCell(curentPos) != item || GetCell(curentPos) == null)
                {
                    resultPos = curentPos;
                    return true;
                }
            }

            resultPos = curentPos;
            return false;
        }
        

        

    }





    public bool IsCellFree(Vector2Int cellPos)
    {
        return allCells[cellPos.x, cellPos.y] == null;
    }

   public Vector3 CellToWorld(Vector2Int pos)
    {
        return new Vector3(pos.x * cellSize.x + transform.position.x, (pos.y * cellSize.y + transform.position.y));
    }

    public Vector2Int WorldToCell(Vector3 pos)
    {
        pos -= transform.position;

        return new Vector2Int(Mathf.FloorToInt(pos.x / cellSize.x), Mathf.FloorToInt(pos.y / cellSize.y));

        
    }

    public Vector3 CellCenter(Vector2Int pos)
    {
        return new Vector3((pos.x + 0.5f) * cellSize.x + transform.position.x , (pos.y + 0.5f) * cellSize.y + transform.position.y);
    }

    public enum SearchDirection
    {
        up,
        down,
        left,
        right,
    }

    private void Start()
    {
        allCells = new IInventoryItem[inventorySize.x, inventorySize.y];
        LoadItems();
    }

    private void LoadItems()
    {
        Item[] items = FindObjectsOfType<Item>();

        foreach (Item item in items)
        {
            if(item.inventory == this)
            LoadItem(item);
        }
    }

    private void LoadItem(IInventoryItem item)
    {
        Vector2Int[,] positions = item.GetGloablPositions();

        foreach (Vector2Int pos in positions)
        {
            SetCell(pos, item);
        }
    }

    public bool IsInBounds(Vector2Int pos)
    {
       return(pos.x >= 0 && pos.x < inventorySize.x && pos.y >= 0 && pos.y < inventorySize.y) ;
    }

    public bool IsInBounds(IInventoryItem item)
    {
        foreach(Vector2Int pos in item.GetGloablPositions())
        {
            if (!(pos.x >= 0 && pos.x < inventorySize.x && pos.y >= 0 && pos.y < inventorySize.y))
            {
                return false;
            }
        }

        return true;
    }

    public class ItemEvent : EventArgs
    {
        public IInventoryItem item;

        public ItemEvent(IInventoryItem item)
        {
            this.item = item;
        }
    }
}


