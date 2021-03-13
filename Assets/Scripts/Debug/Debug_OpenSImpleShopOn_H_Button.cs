using UnityEngine;
using System.Collections;

public class Debug_OpenSImpleShopOn_H_Button : MonoBehaviour
{
    GameStateController gameStateController;
    // Use this for initialization
    void Start()
    {
        gameStateController = GameStateController.gameStateController;
    }

    // Update is called once per frame
    private bool isOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isOpen)
            {
                gameStateController.OpenShop();
                isOpen = true;
            }
            else
            {
                gameStateController.CloseShop();
                isOpen = false;
            }
            
        }

        
    }
}
