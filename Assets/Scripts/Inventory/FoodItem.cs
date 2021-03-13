using UnityEngine;
using System.Collections;

public class FoodItem : Item, IUsableItem
{
    [SerializeField]
    float hungerRestoreAmmount;

    [SerializeField]
    float hpRestoreAmmount;

    [SerializeField]
    string[] soundNames;

    public void UseItem(Player p)
    {
        p.RestoreHunger(hungerRestoreAmmount);
        p.RestoreHP(hpRestoreAmmount);

        if(soundManager != null)
        soundManager.PlaySoundByName(soundNames[Random.Range(0, soundNames.Length)]);
        else
        {
            soundManager = GameStateController.gameStateController.soundManager;
            soundManager.PlaySoundByName(soundNames[Random.Range(0, soundNames.Length)]);
        }
    }


}
