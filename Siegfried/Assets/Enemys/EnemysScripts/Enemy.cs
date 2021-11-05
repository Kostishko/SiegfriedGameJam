using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int _maxHealth;
    private int _curHealth;
    public int Health
    { 
        get => _curHealth;
            private set
        {
            _curHealth = Mathf.Clamp(value, 0, _maxHealth);
        }
    }

    private float _reloadTimer;
    [SerializeField] private float _reloadTime;

    private bool isReloading;

    private bool isDie=false;


    private AIPath _path;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _damageParticle;
    [SerializeField] private ParticleSystem _shootParticle;




    [SerializeField] private int _damage = 5;
    [SerializeField] private GameObject _playerCharacter;

    public enum EnemyState
    {
        MOVING,
        FIGHT,
        DEAD
    }

    private EnemyState _enemyState;
    

    private void Start()
    {
        _playerCharacter = GameObject.FindGameObjectWithTag("Character");
        if (_playerCharacter==null)
        {
            Debug.Log("Can't Find Character!");
        }

        _path = GetComponent<AIPath>();


    }


    private void Update()
    {
        if (_path.velocity.x<0.01f && _path.velocity.y<0.01f)
        {
            if (_enemyState == EnemyState.MOVING)
            {
                _enemyState = EnemyState.FIGHT;


            }
        }

        if (_enemyState == EnemyState.FIGHT)
        {

            if (_reloadTimer>=0)
            {
                _reloadTimer -= 1 * Time.deltaTime;
            }
            else
            {
                _reloadTimer = _reloadTime;
                EnemyMeleeAttack();
            }


        }


    }


    public void takeDamage( int _damage)
    {

        Health -= _damage;
        if (Health==0)
        {
            isDie = true;
            _path.maxSpeed = 0;
            // проигрыш анимации смерти
            _enemyState = EnemyState.DEAD;

        }

        //проигрышь партиклей получения урона, в идеале также анимацию получения урона

        _damageParticle.Play();

    }

    public void EnemyMeleeAttack()
    {
       
    }

    public void EnemyRangeAttack()
    {

    }






}
