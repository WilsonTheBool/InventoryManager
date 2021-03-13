using UnityEngine;
using System.Collections;

public class CurentItemSelectController : MonoBehaviour
{
    public Vector2 defaultSize;
    public RectTransform rectTransform;

    [SerializeField]
    private ItemTransferControler ItemTransferControler;

    public Transform parent;
    public void UpdateSelect(Item item)
    {
        if(item != null)
        {
            rectTransform.pivot = (item.transform as RectTransform).pivot;
            rectTransform.rotation = item.transform.rotation;
            SetPosition(item.transform.position/*(item.transform as RectTransform).anchoredPosition3D*/);
            SetSize((item.transform as RectTransform).rect.size);

            if(ItemTransferControler.holdItem == item)
            {
                rectTransform.position -= new Vector3(0,ItemTransferControler.pickUpDisplace);
            }
        }
    }

    public void UpdateSelect(Vector3 pos)
    {
        SetPosition(pos);
       
    }

    public void ResetSize()
    {
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        SetSize(defaultSize);
    }

    private void SetSize(Vector2 size)
    {
        rectTransform.sizeDelta = size;
    }

    public void SetPosition(Vector3 pos)
    {
        rectTransform.position = pos;
    }
}
