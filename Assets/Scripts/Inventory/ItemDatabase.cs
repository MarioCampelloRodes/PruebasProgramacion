using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{
    private static Dictionary<string, ItemInfo> allItems;

    //Este parámetro sirve para que la función se llame antes que el Awake, para que al cargar los datos ya esté el diccionario creado
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //Busca en el proyecto todos los objetos que haya e inicializa el diccionario con todos ellos
    static void GetItems()
    {
        allItems = new Dictionary<string, ItemInfo>();

        //Es necesario que todos los items (Scriptable Objects) del proyecto estén dentro de una carpeta (ruta especificada) dentro de otra llamada "Resources"
        ItemInfo[] foundItems = Resources.LoadAll<ItemInfo>("Items");

        foreach (ItemInfo foundItem in foundItems)
        {
            allItems.Add(foundItem.itemName, foundItem);
            Debug.Log($"Added: {foundItem.itemName}");
        }
    }

    public static ItemInfo FindItem(string itemName)
    {
        Debug.Log($"Item found: {itemName}");
        return allItems[itemName];
    }
}
