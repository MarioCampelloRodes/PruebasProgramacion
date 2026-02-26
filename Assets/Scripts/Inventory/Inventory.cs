using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events; // Librería para hacer Eventos o Callbacks

public class Inventory : MonoBehaviour
{
    Dictionary<string, uint> items = new Dictionary<string, uint>(); //uint es un int sin signo (siempre +)

    public static Inventory Instance;

    //public UnityEvent<ItemInfo> onAddItem; //Evento que envía información de tipo ItemInfo y se ve en el Inspector

    //Callback -> Actión que se ejecuta cuando se añada un objeto
    //  Pasa como parámetro la info del objeto añadido y qué cantidad de ese objeto hay
    public UnityAction<ItemInfo, uint> onAddedItem;
    public UnityAction<ItemInfo, uint> onRemovedItem;

    void Awake() //Singleton para acceder al código desde otros scripts
    {
        if (Instance == null) 
            Instance = this;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        foreach (var item in items)
        {
            Debug.Log($"Nombre: {item.Key}, Cantidad = {item.Value}");
        }
    }

    public void AddItem(ItemInfo item)
    {
        if (!items.ContainsKey(item.itemName)) //Si el objeto recogido no está en el inventario...
        {
            //Añades un nuevo objeto de ese tipo al diccionario
            items.Add(item.itemName, 1);
        }
        else
        {
            if (item.stackable)
            {
                //Suma uno al valor de la key, que es la cantidad de ese objeto
                items[item.itemName]++; 
            }
            else
            {
                items.Add(item.itemName, 1);
            }
        }

        //Ejecutar el callback de que se ha añadido un nuevo objeto, y pasa su información
        //El operador ? comprueba si hay acciones dentro del callback, sino no hace nada.
        onAddedItem?.Invoke(item, items[item.itemName]); 
    }

    public void RemoveItem(ItemInfo item)
    {
        if (!items.ContainsKey(item.itemName)) 
        {
            //Si el objeto a quitar no está en el inventario no hace nada
            return;
        }

        bool deleteObject = false;

        if (item.stackable)
        {
            items[item.itemName]--;

            if (items[item.itemName] <= 0)
            {
                deleteObject = true;
            }
        }
        else
        {
            deleteObject = true;
            //Forzar a que la cantidad del objeto sea 0
            items[item.itemName] = 0;
        }

        onRemovedItem?.Invoke(item, items[item.itemName]);

        if(deleteObject)
            items.Remove(item.itemName);
    }

    public bool HasItem(ItemInfo itemToFind)
    {
        return items.ContainsKey(itemToFind.itemName);
    }
}
