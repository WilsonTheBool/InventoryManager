using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ItemTransferControler))]
public class ItemSellInputController : BaseInputHandler
{
    private ItemTransferControler itemTransferControler;

    private void Start()
    {
        itemTransferControler = GetComponent<ItemTransferControler>();
    }

    public override void HandleInput(InputArgs args)
    {
        switch (args.type)
        {
            case InputArgs.InputType.movement:
                {
                    itemTransferControler.PlayerInputController_OnMoveKeyPressed(args.direction);
                    return;
                }
            case InputArgs.InputType.use:
                {
                    itemTransferControler.TrySellItem();
                    return;
                }
        }
    }
}
