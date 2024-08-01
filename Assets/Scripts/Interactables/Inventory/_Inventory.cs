using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Inventory : _ObjectInteractable
{
    public override void OnLookAt() { }
    public override void OnLookAway() { }
    public override void OnInteraction() { }

    public bool _isEquiped()
    {
        if (gameObject.GetComponentInParent<PlayerController>())
            return true;
        else
        {
            print(gameObject.name + " is False");
            return false;
        }
    }

    public void EquipInventory()
    {
        gameObject.SetActive(true);
    }

    public void UnequipInventory()
    {
        gameObject.SetActive(false);
    }

    public abstract void ObtainInventory(PlayerController playerReference);
}
