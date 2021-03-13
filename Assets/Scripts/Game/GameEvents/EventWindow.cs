using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EventWindow : MonoBehaviour
{
    [SerializeField]
    EventWindow_InputController window_inputController;

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    PlayerInputController inputController;

    [SerializeField]
    Text EventText;

    [SerializeField]
    float textDelay;

    [SerializeField]
    GameEvent test;

    [SerializeField]
    OptionsButton[] all_options_in_order;

    GameStateController gameStateController;

    [SerializeField]
    Player player;

    public GameEvent gameEvent;

    private bool isTextLoading;
    private void Start()
    {
        gameStateController = GameStateController.gameStateController;
        player = FindObjectOfType<Player>();

        inputController.OnPickUpKeyPressed += InputController_OnPickUpKeyPressed;
        inputController.OnMoveKeyPressed += InputController_OnMoveKeyPressed;
        inputController.OnUseKeyPressed += InputController_OnUseKeyPressed;
        SetUp(test);
    }

    private void InputController_OnUseKeyPressed()
    {
        if (isActiveAndEnabled)
        {
            EndLoadText(savedText);
        }
    }

    private void InputController_OnMoveKeyPressed(Vector2Int obj)
    {
        if (isActiveAndEnabled)
        {
            NavigateOptions(obj);
        }
    }

    private void NavigateOptions(Vector2Int vec)
    {
        if(vec.y < 0)
        {
            NavigateDown();
        }
        else
        {
            if(vec.y > 0)
            {
                NavigateUp();
            }
        }
    }

    private void NavigateDown()
    {
        int id = selectedId;

        if (id < all_options_in_order.Length - 1)
        {
            if(all_options_in_order[id + 1].isActiveAndEnabled)
            {
                SelectOption(id + 1);
                soundManager.PlaySoundByName("Menu_Move");
            }
        }
    }

    private void NavigateUp()
    {
        int id = selectedId;

        if (id > 0)
        {
            if (all_options_in_order[id - 1].isActiveAndEnabled)
            {
                SelectOption(id - 1);
                soundManager.PlaySoundByName("Menu_Move");
            }
        }

        if(id == -1)
        {
            SelectOption(0);
        }
    }

    private string savedText;

    private int selectedId;
    private EventOption selectedOption;
    private void SelectOption(int id)
    {
        selectedOption = gameEvent.EventOptions[id];
        selectedId = id;
        for (int i = 0; i < all_options_in_order.Length; i++)
        {
            if(i == id)
            {
                all_options_in_order[id].SetHighlighter(true);
                all_options_in_order[id].SetNormal();
            }
            else
            {
                if (all_options_in_order[i].isActiveAndEnabled)
                {
                    all_options_in_order[i].SetHighlighter(false);
                    all_options_in_order[i].SetDark();
                }
            }
        }
    }

    

    public void SetUp(GameEvent e)
    {
        List<EventOption> available = e.EventOptions.FindAll(isAvailable);
        if(isTextLoading)
        EndLoadText(savedText);

        StartLoadText(e.Text);
        gameEvent = e;
        for(int i = 0; i < available.Count; i++)
        {
            all_options_in_order[i].gameObject.SetActive(true);
            all_options_in_order[i].SetText(available[i].Text);
        }

        for (int i = available.Count; i < all_options_in_order.Length; i++)
        {
            all_options_in_order[i].gameObject.SetActive(false);
        }

        SelectOption(0);
        soundManager.PlaySoundByName("Map_Open");
    }

    private bool isAvailable(EventOption option)
    {
        return option.CanUse(player);
    }

    private void ActivateCurentOption()
    {
        if(selectedOption != null)
        {
            selectedOption.ActivateOption(player, this);
            soundManager.PlaySoundByName("Text");
        }
    }

    private void InputController_OnPickUpKeyPressed()
    {
        if (isActiveAndEnabled)
        {
            ActivateCurentOption();
        }
    }

    private IEnumerator curentCo;
    public void StartLoadText(string text)
    {
        isTextLoading = true;
        savedText = text;
        curentCo = LoadText(text);
        StartCoroutine(curentCo);
        
    }

    public void EndLoadText(string text)
    {
        if(curentCo != null && isTextLoading)
        StopCoroutine(curentCo);
        
        EventText.text = text;
    }   

    private IEnumerator LoadText(string text)
    {
        int curentLength = 1;

        while(curentLength <= text.Length)
        {
            EventText.text = text.Substring(0, curentLength);
            curentLength++;
            
            yield return new WaitForSeconds(textDelay);
            soundManager.PlaySoundByName("Text");
        }

        isTextLoading = false;
    }

    public void EndEvent()
    {
        GameStateController.gameStateController.EndEvent(gameEvent);
    }
}
