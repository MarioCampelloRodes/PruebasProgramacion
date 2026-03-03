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
        }
        else
        {
            //Si al iniciar ya hay un Singleton, se destruye para que no haya duplicados
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //DEBUG
        if (Input.GetKeyDown(KeyCode.F6))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
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

    public bool IsChestOpen(uint chestID)
    {
        //Devuelve true o false en función de si el cofre está en la lista de abiertos
        return openChests.Contains(chestID);
    }
}
