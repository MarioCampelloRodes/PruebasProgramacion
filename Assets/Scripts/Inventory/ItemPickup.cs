using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public ItemInfo item;

    public void Interact()
    {
        Inventory.Instance.AddItem(item);
    }
}
