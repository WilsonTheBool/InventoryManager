using System;
using System.Collections.Generic;
using UnityEngine;

public class EventCharacter : MonoBehaviour
{
    [SerializeField]
    string EventName;

    [SerializeField]
    GameEvent gameEvent;

    public void StartEvent()
    {
        gameEvent.owner = this;
        GameStateController.gameStateController.OnEventEnded += OnEventEnd;
        GameStateController.gameStateController.StartEvent(gameEvent);
    }

    [SerializeField]
    GameObject smokePrefab;


    public virtual void OnEventEnd(GameEvent gameEvent)
    {
        GameStateController.gameStateController.OnEventEnded -= OnEventEnd;
        MovementController.movementController.AddObject(Instantiate(smokePrefab, transform.position, Quaternion.Euler(0,0,0)));
        MovementController.movementController.RemoveObject(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger");
        StartEvent();
    }
}

