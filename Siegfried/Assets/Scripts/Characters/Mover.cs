using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 movement;
    private Animator _animator;
    private readonly int animHorizontal = Animator.StringToHash("Horizontal");
    private readonly int animVertical = Animator.StringToHash("Vertical");
    private readonly int animSpeed = Animator.StringToHash("Speed");

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetFloat(animHorizontal, movement.x);
        _animator.SetFloat(animVertical, movement.y);
        _animator.SetFloat(animSpeed, movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
