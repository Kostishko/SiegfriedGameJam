using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnEarth : MonoBehaviour
{
    public Item Item;
    private Rigidbody2D _rg;

    private void Start()
    {
        _rg = GetComponent<Rigidbody2D>();
        _rg.gravityScale = 0.5f;
        float _rndX = Random.Range(-1.5f, 1.5f);
        float _rndY = Random.Range(0f, 1f);
        Vector2 _vec2 = new Vector2(transform.position.x + _rndX, _rndY * 5);
        _rg.AddForce(_vec2, ForceMode2D.Impulse);
        StartCoroutine(gravityEnum());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Debug.Log("it's character!");
            // GameController _controller = FindObjectOfType<GameController>();
            // _controller.AddItemCount(1, Item);

            // for (int i =0; i<=_controller.Inventory.Count-1; i++)
            // {
            //     if (_controller.Inventory[i].item.itemName == Item.itemName)
            //     {
            //         _controller.Inventory[i].PutItem(Item);
            //         Destroy(this.gameObject, 0.1f);
            //     }
            // }

            var inventory = FindObjectOfType<Inventory>();
            if (inventory && inventory.TryPutItem(Item))
            {

                if (_controller.Inventory[i].item.itemName == Item.itemName)
                {
                    _controller.Inventory[i].PutItem(Item);
                    Destroy(this.gameObject, 0.1f);
                }

            }
          
        }




    }

    IEnumerator gravityEnum()
    {
        yield return new WaitForSeconds(0.8f);
        gravityZero();

    }

    private void gravityZero()
    {
        _rg.gravityScale = 0f;
        _rg.velocity = new Vector2(0, 0);
    }




}
