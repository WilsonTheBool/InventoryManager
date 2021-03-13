using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    [SerializeField]
    Image healthBar;

    [SerializeField]
    Image hungerBar;

    [SerializeField]
    Text Hptext;

    [SerializeField]
    Text HungerText;

    [SerializeField]
    Text GoldText;

    Player p;
    void Start()
    {
        p = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        healthBar.fillAmount = p.GetHp() / p.GetMaxHp();
        Hptext.text = p.GetHp().ToString() + " / " + p.GetMaxHp().ToString();

        hungerBar.fillAmount = p.GetHunger() / p.GetMaxHunger();
        HungerText.text = p.GetHunger().ToString() + " / " + p.GetMaxHunger().ToString();

        GoldText.text = p.GetGold().ToString();
    }
}
