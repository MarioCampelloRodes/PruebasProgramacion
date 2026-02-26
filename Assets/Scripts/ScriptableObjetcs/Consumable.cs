using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Consumable")]
public class Consumable : ItemInfo      //Hereda todas las variables y funciones escritas en ItemInfo, más aparte lo que escribamos aquí
{
    public int healthAmount = 0;
    public float speedBoost = 0;
    public float duration = 0;
    //Override de la función Use() de la clase base ItemInfo para cambiar su comportamiento
    public override void Use()
    {
        Inventory.Instance.RemoveItem(this);
    }
}
