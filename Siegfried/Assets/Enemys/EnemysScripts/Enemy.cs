using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, IDamageable
{
    #region EnemyStats
    [Header("Enemy Stats")]
    [SerializeField] private int _damage = 5;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _curHealth;
    [SerializeField]
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
    private Vector2 _attackDir;

    #endregion

    #region Enemy State
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
    private AIDestinationSetter _AISetter;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip swordSound;


    private void Start()
    {
        _curHealth = _maxHealth;
        _playerCharacter = GameObject.FindGameObjectWithTag("Character");
        if (_playerCharacter == null)
        {
            Debug.Log("Can't Find Character!");
        }

        _AISetter = GetComponent<AIDestinationSetter>();
        _AISetter.target = _playerCharacter.transform;



        if (_damageParticle == null)
        {
            Debug.Log("damage particle doesn't here!");
        }


        if (_shootParticle == null)
        {
            Debug.Log("shoot particle doesn't here!");
        }

        if (!isMelee && _projectile == null)
        {
            Debug.Log("Projectfile is null");
        }

        _path = GetComponent<AIPath>();
        if (!_path)
        {
            Debug.Log("_path doesn't exist!");
        }


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

        if (_enemyState == EnemyState.FIGHT && _path.endReachedDistance > Vector2.Distance(_playerCharacter.transform.position, transform.position))
        {

            if (_reloadTimer >= 0)
            {
                _reloadTimer -= 1 * Time.deltaTime;
            }
            else
            {
                _reloadTimer = _reloadTime;
                _attackDir = (_playerCharacter.transform.position - transform.position).normalized;
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
            if (_enemyState != EnemyState.MOVING && _enemyState != EnemyState.DEAD)
            {
                _enemyState = EnemyState.MOVING;
            }
        }

    }

    private bool isEnemyMoving()
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

    public void TakeDamage(int _damage)
    {

        _curHealth -= _damage;
        if (_curHealth <= 0)
        {
            isDie = true;
            _path.maxSpeed = 0;
            // проигрыш анимации смерти
            if (_enemyState != EnemyState.DEAD)
            {
                Instantiate(GameAssets.instance.Coin, transform.position, Quaternion.identity);
                FindObjectOfType<SpawnController>().OnEnemyDie(this);
            }
            _enemyState = EnemyState.DEAD;

            Destroy(this.gameObject, 0.3f);


        }
        DamagePopup.Create(transform.position, _damage);

        //проигрышь партиклей получения урона, в идеале также анимацию получения урона

        _damageParticle.Play();

    }

    public void EnemyMeleeAttack()
    {
        float alpha = _attackDir.y < 0 && Mathf.Abs(_attackDir.y) > Mathf.Abs(_attackDir.x) ? 0f : 1f;

        var enemyPlum = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Transform>();
        enemyPlum.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_attackDir.y, _attackDir.x) * Mathf.Rad2Deg);
        enemyPlum.GetComponentInChildren<MeleeEnemyProjectile>().OnEnter += OnEnterAttackPlume;
        enemyPlum.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        Destroy(enemyPlum.gameObject, 0.3f);

        audioSource.PlayOneShot(swordSound);

    }

    private void OnEnterAttackPlume(Collider2D other)
    {
        CharacterState _character = other.GetComponent<CharacterState>();
        if (_character)
        {
            _character.TakeDamage(_damage);
        }
    }

    public void EnemyRangeAttack()
    {
        audioSource.PlayOneShot(shootSound);
        var _bullet = Instantiate(_projectile, transform.position, Quaternion.identity);
        Vector2 _dir = new Vector2(_attackDir.x, _attackDir.y);
        _bullet.GetComponentInChildren<RangeEnemyProjectile>().Setup(_dir);

    }




}