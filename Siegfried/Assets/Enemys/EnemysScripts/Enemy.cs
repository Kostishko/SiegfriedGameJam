using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    #region EnemyStats
    [Header("Enemy Stats")]
    [SerializeField] private int _damage = 5;
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

    [SerializeField] bool isMelee = true;
    #endregion

    #region
    [Header("Enemy states")]
    private bool isReloading;
    private bool isDie = false;    
    public enum EnemyState
    {
        MOVING,
        FIGHT,
        DEAD
    }
    private EnemyState _enemyState;
    #endregion

    #region Animation
    [Header("Animations")]
    private AIPath _path;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _damageParticle;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private GameObject _projectile;
    #endregion




    [SerializeField] private GameObject _playerCharacter;


    

    private void Start()
    {
        _playerCharacter = GameObject.FindGameObjectWithTag("Character");
        if (_playerCharacter==null)
        {
            Debug.Log("Can't Find Character!");
        }

        if (_damageParticle==null)
        {
            Debug.Log("damage particle doesn't here!");
        }


        if (_shootParticle == null)
        {
            Debug.Log("shoot particle doesn't here!");
        }

        if (!isMelee&& _projectile==null)
        {
            Debug.Log("Projectfile is null");
        }

        _path = GetComponent<AIPath>();


    }


    private void Update()
    {
        if (!isEnemyMoving())
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
                if (isMelee)
                {
                    EnemyMeleeAttack();
                }
                else
                {
                    EnemyRangeAttack();
                }
                
            }


        }

        if (isEnemyMoving())
        {
            if (_enemyState != EnemyState.MOVING)
            {
                _enemyState = EnemyState.MOVING;
            }
        }

    }

    private bool isEnemyMoving ()
    {
        if (_path.velocity.x < 0.01f && _path.velocity.y < 0.01f)
        {
            return false;
        }
        else
        {
            return true;
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
