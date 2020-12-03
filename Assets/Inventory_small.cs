using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public abstract class aItem : INullable
{
    protected string itemName;

    //TODO
    public override string ToString()
    {
        return itemName;
    }

    public abstract bool IsNull { get; }
}

public class Item : aItem, INullable
{
    private aItem _aItemImplementation;

    public Item(string name)
    {
        itemName = name;
    }

    public override bool IsNull => _aItemImplementation.IsNull;
}

public interface IInventory
{
    [CanBeNull]
    Item popItem(int index);
    void pushItem(Item item, int index);
    Item getItemInfo(int index);
    int getCount();
}

public class Inventory : IInventory
{
    public List<Item> items = new List<Item>();

    public Item popItem(int index)
    {
        try
        {
            items[index] = null;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogWarning("Inventory Count is under " + index);
        }

        return null;
    }

    public void pushItem(Item item, int index)
    {
        try
        {
            items[index] = item;
        }
        catch (IndexOutOfRangeException e)
        {
        }
    }

    public int getCount()
    {
        return items.Count;
    }

    public Item getItemInfo(int index)
    {
        try
        {
            return items[index];
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning("Inventory index : " + index + " is Null");
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogWarning("Inventory Count is under " + index);
        }

        return null;
    }
}

public class Inventory_small : MonoBehaviour
{
    public GridLayoutGroup layout;
    public GameObject cellObject;
    
    public int columnCount;
    public int rowCount;

    public void Awake()
    {
        settings();
    }

    private void settings()
    {
        var rTransform = layout.GetComponent<RectTransform>();
        var size = rTransform.rect.size;
        var padding = layout.padding;
        var spacing = layout.spacing;

        size.x -= (padding.left + padding.right);
        size.y -= (padding.top + padding.bottom);

        var spacing_x = spacing.x * (columnCount - 1);
        var spacing_y = spacing.y * (rowCount - 1);

        size.x -= spacing_x;
        size.y -= spacing_y;

        var cell_x = size.x / columnCount;
        var cell_y = size.y / rowCount;
        
        Debug.Log("cell size = " + cell_x + "," + cell_y);
        
        layout.cellSize = new Vector2(cell_x,cell_y);
        for (int i = 0; i < columnCount * rowCount; i++)
        {
            var cell = GameObject.Instantiate(cellObject, layout.transform);
            cell.GetComponent<InventoryCellObject>().item = new Item("item " + i);
            cell.SetActive(true);
        }
    }
}
