using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameStateController: MonoBehaviour
{
    public static GameStateController gameStateController;

    public event Action<GameEvent> OnEventStarted;
    public event Action<GameEvent> OnEventEnded;

    public event Action<List<Inventory>> OnInventoryClosed;
    public event Action<List<Inventory>> OnInventoryOpened;

    public event Action OnShopClosed;
    public event Action OnShopOpened;


    public PlayerInputController PlayerInputController;

    ItemTransferControler ItemTransferControler;

    public InventoriesNavigationController InventoriesNavigationController;

    public SoundManager soundManager;

    public List<Inventory> inventories;
    public EventWindow EventWindow;
    public Canvas itemCanvas;

    public Player player;

    public GameState state;

    public Transform[] inventoryHolders;

    public enum GameState
    {
        none,
        inventory,
        game_event,

    }

    private void Awake()
    {

        if (gameStateController == null)
        {
            gameStateController = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OpenShop()
    {
        OnInventoryOpened?.Invoke(inventories);
        OnShopOpened?.Invoke();

        OpenInventories();
    }

    public void CloseShop()
    {
        OnInventoryClosed?.Invoke(inventories);
        OnShopClosed?.Invoke();

        CloseInventories();
    }

    private void Start()
    {
        inventories = new List<Inventory>(FindObjectsOfType<Inventory>());
        EventWindow = FindObjectOfType<EventWindow>();
        player = FindObjectOfType<Player>();
        ItemTransferControler = FindObjectOfType<ItemTransferControler>();
        PlayerInputController.OnInventoryKeyPressed += PlayerInputController_OnInventoryKeyPressed;
        InventoriesNavigationController = FindObjectOfType<InventoriesNavigationController>();
        soundManager = FindObjectOfType<SoundManager>();
        InventoriesNavigationController.UpdateInventories(inventories);
        EventWindow.gameObject.SetActive(false);
        CloseInventories();
        
    }

    public void CloseEventWindow()
    {
        if(state == GameState.game_event)
        {
            EventWindow.gameObject.SetActive(false);
            state = GameState.none;
        }

    }

    public void AddInventory(Inventory inventory)
    {
        inventories.Add(inventory);
        InventoriesNavigationController.UpdateInventories(inventories);
    }

    public void RemoveInventory(Inventory inventory)
    {
        inventories.Remove(inventory);
        InventoriesNavigationController.UpdateInventories(inventories);
        Destroy(inventory.gameObject);
    }

    private void PlayerInputController_OnInventoryKeyPressed()
    {
        if(state == GameState.inventory && ItemTransferControler.CanCloseInventories())
        {
            CloseInventories();
            
        }
        else
        {
            if(state == GameState.none)
            {
                OpenInventories();
                
            }
            
        }
    }

    public void OpenInventories()
    {
        state = GameState.inventory;
        OnInventoryOpened?.Invoke(inventories);

        foreach (Inventory inventory in inventories)
        {
            OpenInventory(inventory);
        }
    }
    private void OpenInventory(Inventory inventory)
    {
        inventory.gameObject.SetActive(true);
    }

    public void CreateInventory(Inventory inventory, Transform parent)
    {
        Instantiate(inventory, parent).gameObject.SetActive(false);
    }

    public void CloseInventories()
    {
        state = GameState.none;
        OnInventoryClosed?.Invoke(inventories);

        foreach (Inventory inventory in inventories)
        {
            inventory.gameObject.SetActive(false);
        }
    }

    public void StartEvent(GameEvent gameEvent)
    {
        state = GameState.game_event;
        EventWindow.gameObject.SetActive(true);
        EventWindow.SetUp(gameEvent);
        OnEventStarted?.Invoke(gameEvent);
    }

    public void EndEvent(GameEvent gameEvent)
    {
        state = GameState.none;
        EventWindow.gameObject.SetActive(false);
        OnEventEnded?.Invoke(gameEvent);
    }
}

