using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellObject : MonoBehaviour
{
    public Text text;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            changeInventory();
        }
    }

    private Item _item;

    //TODO
    public void changeInventory()
    {
        text.text = _item.ToString();
    }
}
