using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventoryCell> cells = new List<InventoryCell>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cells.Add(transform.GetChild(i).GetComponent<InventoryCell>());
        }
    }

    public void Init(int hp, Item hpItem, int mana, Item manaItem, int coin, Item coinItem)
    {
        if (hp > 0)
        {
            cells[0].item = hpItem;
            cells[0].amount = hp;
        }
        if (mana > 0)
        {
            cells[1].item = manaItem;
            cells[1].amount = mana;
        }
        if (coin > 0)
        {
            cells[2].item = coinItem;
            cells[2].amount = coin;
        }
    }

    public bool TryPutItem(Item item)
    {
        var cell = cells.FirstOrDefault(c => c.item != null && c.item.itemName == item.itemName);
        if (cell == null)
        {
            cell = cells.FirstOrDefault(c => c.item == null);
        }

        if (cell == null)
            return false;

        cell.PutItem(item);
        FindObjectOfType<GameController>().AddItemCount(1, item);
        return true;
    }

    public bool TryPutItem(Item item, int amount)
    {
        var cell = cells.FirstOrDefault(c => c.item != null && c.item.itemName == item.itemName);
        if (cell == null)
        {
            cell = cells.FirstOrDefault(c => c.item == null);
        }

        if (cell == null)
            return false;

        cell.PutItem(item);
        return true;
    }
}
