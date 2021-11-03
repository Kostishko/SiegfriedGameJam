using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 movement;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
