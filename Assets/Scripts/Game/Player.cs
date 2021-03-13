using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    
    public Inventory playerInventory;

    [SerializeField]
    float Gold;

    [SerializeField]
    float Hp;

    [SerializeField]
    float MaxHp;

    [SerializeField]
    float Hunger;

    [SerializeField]
    float MaxHunger;

    public bool HasTrait(string name)
    {
        throw new NotImplementedException();
    }

    public bool HasItem(string name)
    {
        throw new NotImplementedException();
    }

    public bool HasQuest(string name)
    {
        throw new NotImplementedException();
    }

    public float GetHp()
    {
        return Hp;
    }

    public float GetMaxHp()
    {
        return MaxHp;
    }

    public void RestoreHP(float value)
    {
        Hp += value;
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
    }

    public void RestoreHunger(float value)
    {
        Hunger += value;
        if(Hunger > MaxHunger)
        {
            Hunger = MaxHunger;
        }
    }

    public void RemoveHp(float value)
    {
        Hp -= value;
        if (Hp <= 0)
        {
            Hp = 0;
            Death();
        }
    }

    private void Death()
    {

    }

    public void SetHp(float value)
    {
        if(value > Hp)
        {
            RestoreHP(value - Hp);
        }
        else
        {
            RemoveHp(Hp - value);
        }
    }

    public float GetGold()
    {
        return Gold;
    }

    public void AddGold(float value)
    {
        Gold += value;
    }

    public void RemoveGold(float value)
    {
        Gold -= value;
    }
    public void SetGold(float value)
    {
        throw new NotImplementedException();
    }


    public float GetHunger()
    {
        return Hunger;
    }

    public float GetMaxHunger()
    {
        return MaxHunger;
    }

    public void SetHunger(float value)
    {
        Hunger = value;
    }

   
}
