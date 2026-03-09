using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour
{
    public static PersistentInfo Singleton;

    [SerializeField] private List<uint> openChests;

    private void Awake()
    {
        if(Singleton == null)
        {
            //Cuando no hay nadie como Singleton, se asigna y se marca para no destruirse entre escenas
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);

            //Añadir función al callback de datos cargados
            //Este código tan hermoso es una función anónima. Igual que una normal, pero se crea en el momento para añadirla al callback
            SaveManager.OnDataLoaded += (SaveData saveData) =>
            {
                openChests = new List<uint>(saveData.openChestsIDs);
            };
        }
        else
        {
            //Si al iniciar ya hay un Singleton, se destruye para que no haya duplicados
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SaveManager.OnDataSaved += Save;
    }

    public void AddOpenChests(uint chestID)
    {
        //Si la ID no está en la lista, la añade
        if (!openChests.Contains(chestID))
        {
            openChests.Add(chestID);

            //Guardar cofres abiertos
            SaveManager.Save(openChests);
        }
    }

    //Se añade al callback de guardar info
    void Save(SaveData saveData)
    {
        //Actualizar los datos de guardado con la lista de cofres abiertos
        saveData.openChestsIDs = new List<uint>(openChests);
    }

    public bool IsChestOpen(uint chestID)
    {
        //Devuelve true o false en función de si el cofre está en la lista de abiertos
        return openChests.Contains(chestID);
    }
}
