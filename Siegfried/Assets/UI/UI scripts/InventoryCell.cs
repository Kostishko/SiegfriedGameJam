using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Item item;
    public int amount;

    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        Refresh();
    }

    public void PutItem(Item item)
    {
        if (this.item)
        {
            amount++;
        }
        else
        {
            this.item = item;
            amount = 1;
        }
        Refresh();
    }

    private void Refresh()
    {
        if (item)
        {
            _image.sprite = item.sprite;
            _image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _image.sprite = null;
            _image.color = new Color(1, 1, 1, 0);
        }
    }

    public void Use()
    {
        if (item)
        {
            item.Use();
            amount--;
            if (amount == 0) item = null;
            Refresh();
        }
    }
}