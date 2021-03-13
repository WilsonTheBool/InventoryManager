using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInputHandler: MonoBehaviour
{
    public abstract void HandleInput(InputArgs args);


    public class InputArgs : EventArgs
    {
        public InputType type;

        public Vector2Int direction;

        public enum InputType
        {
            movement,
            use,
            pickup,
            rotate,
            inventoryOpenClose,
            esc,
        }

        public InputArgs(InputType type)
        {
            this.type = type;
        }

        public InputArgs(Vector2Int direction)
        {
            this.type = InputType.movement;
            this.direction = direction;
        }
    }
}

