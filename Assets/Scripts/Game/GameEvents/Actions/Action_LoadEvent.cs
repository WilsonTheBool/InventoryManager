using UnityEngine;
using System.Collections;

public class Action_LoadEvent : EventAction
{
    [SerializeField]
    GameEvent eventToLoad;
    public override void DoAction(Player p, EventWindow e)
    {
        e.SetUp(eventToLoad);
    }
}
