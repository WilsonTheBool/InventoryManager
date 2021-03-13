using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoText : MonoBehaviour
{
    [SerializeField]
    private Text infoText;

    [SerializeField]
    private Text costText;
    public void SetUp(string info, float cost)
    {
        infoText.text = info;
        costText.text = cost.ToString();
    }
}
