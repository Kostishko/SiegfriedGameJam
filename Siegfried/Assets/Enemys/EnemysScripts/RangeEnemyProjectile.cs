using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    public int damage = 3;

    public void Setup(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * _speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterState target = other.GetComponent<CharacterState>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }



}
