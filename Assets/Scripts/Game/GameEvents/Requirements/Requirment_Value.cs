using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Requirment_Value : EventRequirement
{
    public enum Value_Type
    {
        Hp,
        Hunger,
        Gold,

    }

    public enum Equallator
    {
        More,
        Less,
        Equal,

    }

    public Value_Type ValueType;
    public Equallator ValueTest;
    public float Ammount;

    public override bool IsFulfilled(Player p)
    {
        return TestValue(GetAmmount(p));
    }

    private float GetAmmount(Player p)
    {
        switch (ValueType)
        {
            case Value_Type.Gold:
                {
                    return p.GetGold();
                }
            case Value_Type.Hp:
                {
                    return p.GetHp();
                }
            case Value_Type.Hunger:
                {
                    return p.GetHunger();
                } 
        }

        throw new System.Exception("Enum behaviour not found");
    }

    private bool TestValue(float value)
    {
        
        switch (ValueTest)
        {
            case Equallator.Equal:
                {
                    return value == Ammount;
                }
            case Equallator.Less:
                {
                    return value < Ammount;
                }
            case Equallator.More:
                {
                    return value > Ammount;
                }
        }

        throw new System.Exception("Enum behaviour not found");
    }
}
