using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    //El objeto asociado a este objeto de la UI del inventario
    public ItemInfo itemInfo;

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountTxt;

    //Asigna el objeto asociado y actualiza los elementos de la UI
    public void SetItem(ItemInfo info)
    {
        itemInfo = info;
        icon.sprite = itemInfo.itemIcon;

        if (!info.stackable)
        {
            amountTxt.gameObject.SetActive(false);
        }
    }

    //Actualiza el texto con la cantidad de objetos disponibles
    public void UpdateAmount(uint amount)
    {
        amountTxt.text = amount.ToString();
    }
}
