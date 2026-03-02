using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private uint chestID;
    [SerializeField] private bool isOpen;
    [SerializeField] private Material openMat;

    // Start is called before the first frame update
    void Start()
    {
        if (PersistentInfo.Singleton.IsChestOpen(chestID))
        {
            SetOpen();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            Open();
        }
    }

    void Open()
    {
        isOpen = true;
        GetComponent<Renderer>().material = openMat;
        //Al abrir el cofre, se añade a la lista de abiertos
        PersistentInfo.Singleton.AddOpenChests(chestID);
    }

    void SetOpen()
    {
        isOpen = true;
        GetComponent<Renderer>().material = openMat;
    }
}
