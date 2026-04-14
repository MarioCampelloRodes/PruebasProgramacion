using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private uint chestID;
    [SerializeField] private bool isOpen;
    [SerializeField] private Material openMat;
    [SerializeField] private ItemInfo itemToAdd;

    // Start is called before the first frame update
    void Start()
    {
        if (PersistentInfo.Singleton.IsChestOpen(chestID))
        {
            SetOpen();
        }
    }

    public void Interact()
    {
        Open();
    }

    void Open()
    {
        isOpen = true;
        GetComponent<Renderer>().material = openMat;
        //Al abrir el cofre, se añade a la lista de abiertos
        PersistentInfo.Singleton.AddOpenChests(chestID);

        Inventory.Instance.AddItem(itemToAdd);
    }

    void SetOpen()
    {
        isOpen = true;
        GetComponent<Renderer>().material = openMat;
    }
}
