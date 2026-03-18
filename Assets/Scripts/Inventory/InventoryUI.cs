using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemUI itemPrefab;
    [SerializeField] private Transform itemLayout; //Todos los prefabs se emparentan a este

    [SerializeField] private List<ItemUI> items = new List<ItemUI>();

    void Start()
    {
        //Añadir la función CreateItem al callback del inventario cuando se añade el objeto
        //  No se necesitan (ItemInfo item) porque la función pilla el parámetro del callback, por tanto debe pedir los mismos parámetros del callback
        Inventory.Instance.onAddedItem += CreateItem;

        Inventory.Instance.onRemovedItem += DeleteItem;
    }

    public void CreateItem(ItemInfo itemInfo, uint amount)
    {
        //Buscar si el objeto ya está en el inventario
        ItemUI duplicate = FindItem(itemInfo);

        //Si no hay duplicados, se crea uno nuevo
        if(duplicate == null)
        {
            Transform slot = null;
            //Buscar en todos lo objetos hijos del layout (huecos)
            for (int i = 0; i < itemLayout.childCount; i++)
            {
                //Si el hueco no tiene objetos hijo, significa que está vacío
                if (itemLayout.GetChild(i).childCount == 0)
                {
                    //Se asigna el hueco vacío y se sale del bucle
                    slot = itemLayout.GetChild(i);
                    break;
                }
            }

            ItemUI newItem = Instantiate(itemPrefab, slot); //Crear un itemUI nuevo y lo emparenta al slot al instanciarlo

            newItem.SetItem(itemInfo);
            items.Add(newItem);
        }
        //Si hay un duplicado, se actualiza la cantidad del objeto
        else
        {
            duplicate.UpdateAmount(amount);
        }
    }

    private ItemUI FindItem(ItemInfo infoToFind)
    {
        //Buscamos en todos los objetos el que coincida con la info que buscamos
        foreach (ItemUI item in items)
        {
            //Si lo encuentra, lo devuelve
            if(item.itemInfo == infoToFind)
            {
                return item;
            }
        }

        //Si no encuentra nada, devuelve NULL
        return null;
    }

    void DeleteItem(ItemInfo item, uint amount)
    {
        //Buscar el objeto a eliminar
        ItemUI itemToDelete = FindItem(item);

        //Si queda al menos un objeto, se actualiza su cantidad
        if(amount > 0)
        {
            itemToDelete.UpdateAmount(amount);
        }
        //Si hay 0 después de restar 1, el objeto se ha gastado y hay que eliminarlo
        else
        {
            //Eliminar de la lista de la UI el objeto
            items.Remove(itemToDelete);

            //Destruir el objeto del canvas
            Destroy(itemToDelete.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            EnableDisableInventory();
        }
    }

    void EnableDisableInventory()
    {
        if (!itemLayout.gameObject.activeSelf)
        {
            //Asegurarnos de que su escala sea 0 al principio
            itemLayout.localScale = Vector3.zero;
            itemLayout.gameObject.SetActive(true);

            //Tween para animación de escala
            itemLayout.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
        }
        else
        {
            itemLayout.LeanScale(Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() => itemLayout.gameObject.SetActive(false));
        }
    }
}
