using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Item item;
    public int amount;
    public KeyCode keyCode;
    private TextMeshProUGUI _text;

    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
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
            _text.text = amount.ToString();
        }
        else
        {
            _image.sprite = null;
            _image.color = new Color(1, 1, 1, 0);
        }
        _text.gameObject.SetActive(item);
    }

    public void Use()
    {
        if (item && item.Use())
        {
            FindObjectOfType<GameController>().AddItemCount(-1, item);
            amount--;
            if (amount == 0) item = null;
            Refresh();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode)) Use();
    }
}
