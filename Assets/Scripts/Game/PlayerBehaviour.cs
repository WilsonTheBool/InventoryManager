using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    float hungerTickTime;

    [SerializeField]
    float starveAmmount;

    Player owner;
    GameStateController GameState;


    public bool isMoving = false;

    Animator animator;

    public event Action PlayerMoveStart;
    public event Action PlayerMoveEnd;

    // Use this for initialization
    void Start()
    {
        owner = GetComponent<Player>();
        animator = GetComponent<Animator>();
        GameState = GameStateController.gameStateController;
        GameState.OnEventStarted += GameState_OnEventStarted;
        GameState.OnEventEnded += GameState_OnEventEnded;

        GameState.OnInventoryOpened += GameState_OnInventoryOpened;
        GameState.OnInventoryClosed += GameState_OnInventoryClosed;

        StartMove();
    }

    private float curentTickTime;
    public void Update()
    {
        if (isMoving)
        {
            if(curentTickTime >= hungerTickTime)
            {
                StarvePlayer();
                curentTickTime = 0;
            }
            curentTickTime += Time.deltaTime;
        }
    }

    private void StarvePlayer()
    {
        owner.SetHunger(owner.GetHunger() - starveAmmount);
    }

    private void StartMove()
    {
        animator.SetBool("Run", true);
        isMoving = true;
        PlayerMoveStart?.Invoke();
    }

    private void EndMove()
    {
        animator.SetBool("Run", false);
        isMoving = false;
        PlayerMoveEnd?.Invoke();
    }

    private void GameState_OnInventoryClosed(System.Collections.Generic.List<Inventory> obj)
    {
        StartMove();
    }

    private void GameState_OnInventoryOpened(System.Collections.Generic.List<Inventory> obj)
    {
        EndMove();
    }

    private void GameState_OnEventEnded(GameEvent obj)
    {
        StartMove();
    }

    private void GameState_OnEventStarted(GameEvent obj)
    {
        EndMove();
    }

}
