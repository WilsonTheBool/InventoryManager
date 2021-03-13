using UnityEngine;
using System.Collections;
using System;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    KeyCode upKey;

    [SerializeField]
    KeyCode downKey;

    [SerializeField]
    KeyCode rightKey;

    [SerializeField]
    KeyCode leftKey;

    [SerializeField]
    KeyCode pickUp_Drop_Key;

    [SerializeField]
    KeyCode rotate_Key;

    [SerializeField]
    KeyCode use_Key;

    [SerializeField]
    KeyCode inventory_Key;

    [SerializeField]
    KeyCode mainMenu_Key;

    [SerializeField]
    KeyCode Joystick_pickUp_Drop_Key;

    [SerializeField]
    KeyCode Joystick_rotate_Key;

    [SerializeField]
    KeyCode Joystick_use_Key;

    [SerializeField]
    KeyCode Joystick_inventory_Key;

    [SerializeField]
    KeyCode Joystick_mainMenu_Key;

    public event Action<Vector2Int> OnMoveKeyPressed; 

    public event Action OnRotationKeyPressed; 
    public event Action OnPickUpKeyPressed;
    public event Action OnUseKeyPressed;
    public event Action OnInventoryKeyPressed;
    public event Action OnEscKeyPressed;

    private MoveDirection keyHold = MoveDirection.none;

    [SerializeField]
    float holdTimer;
    [SerializeField]
    float repeatTimer;

    float curentTimer;
    public void Update()
    {


        //if(keyHold != MoveDirection.none)
        //{
        //    if(curentTimer > 0)
        //    {
        //        curentTimer -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        curentTimer = repeatTimer;
        //        OnMoveKeyPressed(GetVecDirection(keyHold));
        //    }
        //}
        //float Vertical = Input.GetAxis("Vertical");
        //if(Vertical > 0)
        //{
        //    //OnMoveKeyPressed?.Invoke(GetVecDirection(MoveDirection.up));
        //    SetKeyHold(MoveDirection.up);
        //}
        //if (Vertical < 0)
        //{
        //   // OnMoveKeyPressed?.Invoke(new Vector2Int(0, -1));
        //    SetKeyHold(MoveDirection.down);
        //}
        //if(Vertical == 0)
        //{
        //    SetKeyReleased(MoveDirection.up);
        //    SetKeyReleased(MoveDirection.down);
        //}

        //float Horisontal = Input.GetAxis("Horizontal");
        //if (Horisontal > 0)
        //{
        //    //OnMoveKeyPressed?.Invoke(GetVecDirection(MoveDirection.up));
        //    SetKeyHold(MoveDirection.right);
        //}
        //if (Horisontal < 0)
        //{
        //    // OnMoveKeyPressed?.Invoke(new Vector2Int(0, -1));
        //    SetKeyHold(MoveDirection.left);
        //}
        //if (Horisontal == 0)
        //{
        //    SetKeyReleased(MoveDirection.left);
        //    SetKeyReleased(MoveDirection.right);
        //}
        if (Input.GetKeyDown(upKey))
        {
            OnMoveKeyPressed?.Invoke(GetVecDirection(MoveDirection.up));
            SetKeyHold(MoveDirection.up);
        }
        if (Input.GetKeyUp(upKey))
        {
            SetKeyReleased(MoveDirection.up);
        }

        if (Input.GetKeyDown(downKey))
        {
            OnMoveKeyPressed?.Invoke(new Vector2Int(0, -1));
            SetKeyHold(MoveDirection.down);
        }
        if (Input.GetKeyUp(downKey))
        {
            SetKeyReleased(MoveDirection.down);
        }

        if (Input.GetKeyDown(rightKey))
        {
            OnMoveKeyPressed?.Invoke(new Vector2Int(1, 0));
            SetKeyHold(MoveDirection.right);
        }
        if (Input.GetKeyUp(rightKey))
        {
            SetKeyReleased(MoveDirection.right);
        }

        if (Input.GetKeyDown(leftKey))
        {
            OnMoveKeyPressed?.Invoke(new Vector2Int(-1, 0));
            SetKeyHold(MoveDirection.left);
        }
        if (Input.GetKeyUp(leftKey))
        {
            SetKeyReleased(MoveDirection.left);
        }



        if (Input.GetKeyDown(pickUp_Drop_Key) || Input.GetKeyDown(Joystick_pickUp_Drop_Key))
        {
            OnPickUpKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(rotate_Key) || Input.GetKeyDown(Joystick_rotate_Key))
        {
            OnRotationKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(use_Key) || Input.GetKeyDown(Joystick_use_Key))
        {
            OnUseKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(inventory_Key) || Input.GetKeyDown(Joystick_inventory_Key))
        {
            OnInventoryKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(mainMenu_Key) || Input.GetKeyDown(Joystick_mainMenu_Key))
        {
            OnEscKeyPressed?.Invoke();
        }

    }

    private void SetKeyHold(MoveDirection key)
    {
        if(keyHold != key)
        {
            keyHold = key;
            curentTimer = 0;
        }
        
    }

    private void SetKeyReleased(MoveDirection key)
    {
        if(keyHold == key)
        {
            keyHold = MoveDirection.none;
        }
    }

    private void SetKeyReleased()
    {

        keyHold = MoveDirection.none;
    }

    private Vector2Int GetVecDirection(MoveDirection key)
    {
        switch (key)
        {
            case MoveDirection.up:
                {
                    return new Vector2Int(0, 1);
                }
            case MoveDirection.down:
                {
                    return new Vector2Int(0, -1);
                }
            case MoveDirection.right:
                {
                    return new Vector2Int(1, 0);
                }
            case MoveDirection.left:
                {
                    return new Vector2Int(-1, 0);
                }
            default:
                {
                    return new Vector2Int(0, 0);
                }
        }
    }

    private enum MoveDirection
    {
        up, down, left, right, none
    }
}
