using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoController : MonoBehaviour
{
    [SerializeField]
    private ItemInfoText itemInfoTextObject;

    [SerializeField]
    private Vector3 textOffset;

    [SerializeField]
    private Vector3 defaultPos;
    public void ShowItemInfo(string info, float cost, Vector3 itemPos, Vector2Int itemSize)
    {
        Vector3 pos = new Vector3(itemPos.x + itemSize.x * textOffset.x, itemPos.y + itemSize.y * textOffset.y);
        itemInfoTextObject.transform.position = pos;
        itemInfoTextObject.SetUp(info, cost);
    }

    public void HideItemInfo()
    {
        itemInfoTextObject.transform.position = defaultPos;
    }
}
