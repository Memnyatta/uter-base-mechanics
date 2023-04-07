using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public Inventory _inventory;

    public CollectibleItem _item;

    MyNameIsUter _controls = null;

    private void Awake()
    {
        _controls = new MyNameIsUter();
        _controls.Player.Enable();
        _controls.Player.PickUp.started += PickUp;
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
        _controls.Player.PickUp.started -= PickUp;
    }

    void PickUp(InputAction.CallbackContext context)
    {
        if (_item != null)
        {
            if (_item.canPick)
            {
                PickItem(_item);
                _item.PickUobject();
            }
        }
    }

    public void PickItem(CollectibleItem item)
    {
        _inventory.AddItem(item);
    }
}
