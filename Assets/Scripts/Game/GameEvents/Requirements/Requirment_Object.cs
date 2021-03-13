using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Requirment_Object : EventRequirement
{
    public enum Object_Type
    {
       Trait,
       Item,
       Quest,

    }

    public Object_Type ObjectType;

    public string ObjectName;

    public override bool IsFulfilled(Player p)
    {
        switch (ObjectType)
        {
            case Object_Type.Item:
                {
                    return p.HasItem(ObjectName);
                }
            case Object_Type.Trait:
                {
                    return p.HasTrait(ObjectName);
                }
            case Object_Type.Quest:
                {
                    return p.HasQuest(ObjectName);
                }
        }

        throw new System.Exception("Enum behaviour not found");
    }
}
