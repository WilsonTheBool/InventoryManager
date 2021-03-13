using System;
using System.Collections.Generic;
using UnityEngine;
public class Debug_DrawInventoryBound: MonoBehaviour
{
    [SerializeField]
    Inventory inventory;

    private void Update()
    {
        for (int x = 0; x <= inventory.inventorySize.x; x++)
        {

            Vector3 start = new Vector3(x * inventory.cellSize.x, 0 * inventory.cellSize.y);
            Vector3 end = new Vector3(x * inventory.cellSize.x, inventory.inventorySize.y * inventory.cellSize.y);
            Debug.DrawLine(start + transform.position, end + transform.position, Color.green);


        }

        for (int y = 0; y <= inventory.inventorySize.y; y++)
        {
            Vector3 start = new Vector3(0 * inventory.cellSize.x, y * inventory.cellSize.y);
            Vector3 end = new Vector3(inventory.inventorySize.x * inventory.cellSize.x, y * inventory.cellSize.y);
            Debug.DrawLine(start + transform.position, end + transform.position, Color.green);
        }
    }
}

