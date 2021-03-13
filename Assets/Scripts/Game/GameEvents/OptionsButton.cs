using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsButton : MonoBehaviour
{

    [SerializeField]
    Text text;

    [SerializeField]
    Image image;

    [SerializeField]
    Image highlighter;

    [SerializeField]
    Color normalcolor;

    [SerializeField]
    Color darkColor;

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetHighlighter(bool isActive)
    {
        highlighter.gameObject.SetActive(isActive);
    }


    public void SetDark()
    {
        image.color = darkColor;
    }

    public void SetNormal()
    {
        image.color = normalcolor;
    }
}
