using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAfricaBullet : Bullet
{
    override protected void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }

        var totem = other.GetComponent<Totem>();
        if (totem)
        {
            totem.Activate();
            Destroy(gameObject);
        }
    }
}
