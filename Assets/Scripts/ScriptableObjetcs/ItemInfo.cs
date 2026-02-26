using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemInfo")]      //Para poder crear el SO haciendo Click Drcho en Assets y que esté agrupado en "Scriptable Objects"

public class ItemInfo : ScriptableObject
{
    public string itemName = "defaultName";

    public bool stackable = true;

    public Sprite itemIcon;

    //Función de Usar para todos los objetos
    public virtual void Use()    //Al marcar una función como virtual, se puede sobreescribir desde las clases heredadas
    {
        Debug.Log($"Used Standard Item: {itemName}");
    }
}
