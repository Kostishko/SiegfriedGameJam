using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    public int damage = 3;

    public void Setup(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction.normalized * _speed, ForceMode2D.Impulse);
        GetComponent<Transform>().Rotate(0, 0, GetAngleFromVectorFloat(direction));
        Destroy(gameObject, 5f);
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Enemy target = other.GetComponent<Enemy>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
