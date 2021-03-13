using UnityEngine;
using System.Collections;

public class Action_EndCurentEvent : EventAction
{
    public override void DoAction(Player p, EventWindow e)
    {
        e.EndEvent();
    }

}
