using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    /// <summary>
    /// Position of [0,0] coord in inventory (not local)
    /// </summary>
    Vector2Int OriginPos { get; set; }

    /// <summary>
    /// Item size (in ocupied inventry slots)
    /// </summary>
    Vector2Int Size { get; set; }

    /// <summary>
    /// Positions of all other item coord (in relation to [0,0], local)
    /// </summary>
    Vector2Int[,] ReletivePositions { get; set; }

    /// <summary>
    /// Reletive + origin
    /// </summary>
    /// <returns></returns>
    Vector2Int[,] GetGloablPositions();

    /// <summary>
    /// Rotate item clockwise (!!! rotate only by 90-deg !!!)
    /// </summary>
    void RotateItem(int deg);

    void Translate(Vector2Int pos);

    string GetInfo();

    float GetCost();

}

