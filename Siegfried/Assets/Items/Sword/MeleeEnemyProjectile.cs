using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyProjectile : MonoBehaviour
{
    public Action<Collider2D> OnEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke(other);
    }

    private void Start()
    {
        Destroy(GetComponent<PolygonCollider2D>(), 0.1f);
    }
}
