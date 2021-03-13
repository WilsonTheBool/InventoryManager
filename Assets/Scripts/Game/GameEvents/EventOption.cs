using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOption : MonoBehaviour
{
    public List<EventRequirement> requirements;
    public List<EventAction> eventActions;

    [TextArea]
    public string Text;

    public bool CanUse(Player p)
    {
        foreach(EventRequirement e in requirements)
        {
            if (!e.IsFulfilled(p))
            {
                return false;
            }
        }

        return true;
    }

    public void ActivateOption(Player p, EventWindow e)
    {
        foreach(EventAction eventAction in eventActions)
        {
            eventAction.DoAction(p, e);
        }
    }
}
