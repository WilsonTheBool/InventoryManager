using UnityEngine;
using System.Collections;

public class Action_Value : EventAction
{

    public enum Value_Type
    {
        Hp,
        Hunger,
        Gold,

    }

    public enum Action_Type
    {
        Gain,
        Loose,
    }

    public Action_Type ActionType;
    public Value_Type ValueType;
    public float Ammount;

    public override void DoAction(Player p, EventWindow e)
    {
        switch (ActionType)
        {
            case Action_Type.Gain:
                {
                    ChangeValue(p, true);
                    return;
                }
            case Action_Type.Loose:
                {
                    ChangeValue(p, false);
                    return;
                }

        }
    }

    private void ChangeValue(Player p, bool isGain)
    {
        switch (ValueType)
        {
            case Value_Type.Gold:
                {
                    if (isGain)
                        p.AddGold(Ammount);
                    else
                        p.RemoveGold(Ammount);
                    return;
                }
            case Value_Type.Hp:
                {
                    if (isGain)
                        p.RestoreHP(Ammount);
                    else
                        p.RemoveHp(Ammount);
                    return;
                }
            case Value_Type.Hunger:
                {
                    if (isGain)
                        p.SetHunger(p.GetHunger() + Ammount);
                    else
                        p.SetHunger(p.GetHunger() - Ammount);
                    return;

                }
        }

        throw new System.Exception("Enum behaviour not found");
    }

}
