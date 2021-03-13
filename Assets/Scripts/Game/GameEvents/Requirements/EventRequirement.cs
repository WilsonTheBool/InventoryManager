using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventRequirement : MonoBehaviour
{
    public virtual bool IsFulfilled(Player p)
    {
        return false;
    }
}
