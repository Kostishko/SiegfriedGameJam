using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyGFX : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private AIPath _path;

     private Vector2 _movement;

    private readonly int animHorizontal = Animator.StringToHash("HorizontalEnemy");
    private readonly int animVertical = Animator.StringToHash("VerticalEnemy");
    private readonly int animSpeed = Animator.StringToHash("SpeedEnemy");

    // Start is called before the first frame update

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _path = GetComponentInParent<AIPath>();
        
    }

    // Update is called once per frame
    void Update()
    {

        _movement.x = _path.desiredVelocity.x;
        _movement.y = _path.desiredVelocity.y;
        _movement = _movement.normalized;



        UpdateAnimatorParameters();

    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetFloat(animHorizontal, _movement.x);
        _animator.SetFloat(animVertical, _movement.y);
        _animator.SetFloat(animSpeed, _movement.sqrMagnitude);
    }

}
