using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableSystem : MonoBehaviour
{
    //Una lista con todos los consumibles que queramos llevar equipados
    [SerializeField] private List<ConsumableSlot> slots;

    public static UnityAction<ItemInfo> onConsumableUsed;

    private void Update()
    {
        //Comprobar si se ha pulsado alguna tecla de algún slots
        for(int i = 0; i < slots.Count; i++)
        {
            if (Input.GetKeyDown(slots[i].key) && Inventory.Instance.HasItem(slots[i].consumable))
            {
                //Se usa lo que haya asignado a este slot
                slots[i].UseConsumable();

                onConsumableUsed?.Invoke(slots[i].consumable);
            }
        }
    }
}

//Igual que los Vector3, los Structs guardan varios valores
[System.Serializable]
public struct ConsumableSlot
{
    public ItemInfo consumable;
    public KeyCode key;

    public void AssignConsumable(ItemInfo item)
    {
        consumable = item;
    }

    public void UseConsumable()
    {
        consumable.Use();
    }
}
