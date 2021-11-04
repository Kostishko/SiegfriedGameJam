using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _dashDistance = 5f;
    [SerializeField] private LayerMask _dashLayerMask;
    [SerializeField] private GameObject _dashEffect;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private bool isDashButtonDown;

    #region Animator
    private Animator _animator;
    private readonly int animHorizontal = Animator.StringToHash("Horizontal");
    private readonly int animVertical = Animator.StringToHash("Vertical");
    private readonly int animSpeed = Animator.StringToHash("Speed");
    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement = _movement.normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }

        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetFloat(animHorizontal, _movement.x);
        _animator.SetFloat(animVertical, _movement.y);
        _animator.SetFloat(animSpeed, _movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);

        if (isDashButtonDown)
        {
            var dashDirection = _movement;
            if (_movement == Vector2.zero)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mouseDir = ((Vector2)mousePosition - _rb.position).normalized;
                dashDirection = -mouseDir;
            }

            Vector2 dashPosition = _rb.position + dashDirection * _dashDistance;

            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, dashDirection, _dashDistance, _dashLayerMask);
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
            }

            // Spawn visual effect
            var dashTransform = Instantiate(_dashEffect, _rb.position, Quaternion.identity).GetComponent<Transform>();
            var dashSize = Vector3.Distance(transform.position, dashPosition);
            dashTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dashDirection));
            dashTransform.localScale = new Vector3(dashSize / 1f, 1, 1);
            Destroy(dashTransform.gameObject, 0.3f);

            _rb.MovePosition(dashPosition);
            isDashButtonDown = false;
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
