using System;
using System.Collections.Generic;
using UnityEngine;
public class MovementController : MonoBehaviour
{
    public static MovementController movementController;

    [SerializeField]
    List<GameObject> testMovingObjects;

    [SerializeField]
    PlayerBehaviour playerBehaviour;

    private bool isMoving;

    private void Awake()
    {
        if(movementController == null)
        {
            movementController = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        isMoving = playerBehaviour.isMoving;
        speed = playerBehaviour.moveSpeed;
        playerBehaviour.PlayerMoveStart += PlayerBehaviour_PlayerMoveStart;
        playerBehaviour.PlayerMoveEnd += PlayerBehaviour_PlayerMoveEnd;
    }

    float speed;
    private void LateUpdate()
    {
        if (isMoving)
        {
            Vector3 dif = new Vector3(speed * Time.deltaTime, 0);
            foreach (GameObject obj in testMovingObjects)
            {
                obj.transform.Translate(-dif);
            }
        }
       
    }

    public void RemoveObject(GameObject obj)
    {
        testMovingObjects.Remove(obj);
    }

    public void AddObject(GameObject obj)
    {
        testMovingObjects.Add(obj);
    }

    private void PlayerBehaviour_PlayerMoveEnd()
    {
        isMoving = false;
    }

    private void PlayerBehaviour_PlayerMoveStart()
    {
        isMoving = true;
    }
}

