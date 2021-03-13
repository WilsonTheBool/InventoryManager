using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameEvent : MonoBehaviour
{
    
    public List<EventOption> EventOptions;

    public EventCharacter owner;

    public void SetOpwner(EventCharacter ch)
    {
        owner = ch;
    }

    [TextArea]
    public string Text;
}
