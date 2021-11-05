using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    public int damage = 3;

    public void Setup(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * _speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy target = other.GetComponent<Enemy>();
        if (target != null)
        {
            target.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
