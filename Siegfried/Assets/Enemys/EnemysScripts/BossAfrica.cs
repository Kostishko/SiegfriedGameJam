using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAfrica : MonoBehaviour
{
    private BossStates _state = BossStates.Attack;
    private Transform _playerCharacter;

    [SerializeField] private LayerMask _totemLayerMask;
    [SerializeField] private int _moveDistance = 2;
    [SerializeField] private float _moveSpeed = 3f;
    private Vector2 _waypoint;
    private Vector2 _startPosition;
    [SerializeField] private int _maxHealth;
    private int _health;
    public int Health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
        }
    }

    private float _reloadTimer;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _queueSize = 6;
    private int _queueCount;
    private Vector2 _attackDir;
    private float _shootTimer;
    [SerializeField] private float _shootTime;
    [SerializeField] private Transform _bulletStartPosion;
    [SerializeField] private GameObject _bulletPrefab;

    private Rigidbody2D _rb;
    private void Start()
    {
        Health = _maxHealth;
        _rb = GetComponent<Rigidbody2D>();
        _playerCharacter = GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>();
        _startPosition = _rb.position;
    }

    private void Update()
    {
        switch (_state)
        {
            case BossStates.Reload:
                _reloadTimer -= Time.deltaTime;
                if (_reloadTimer < 0)
                {
                    _state = BossStates.Attack;
                    _reloadTimer = _reloadTime;
                }
                break;
            case BossStates.Attack:
                HandleAttack();
                break;

        }
        _attackDir = (_playerCharacter.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        var angle = Mathf.Atan2(_attackDir.y, _attackDir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        transform.eulerAngles = new Vector3(0, 0, angle);
        Moving();
    }
    private void HandleAttack()
    {
        _shootTimer -= Time.deltaTime;

        if (_shootTimer < 0)//raycast totem
        {
            var shootDir = (_playerCharacter.position - _bulletStartPosion.position).normalized;
            if (CheckActiveTotem(shootDir))
                return;
            _shootTimer = _shootTime;
            Shoot(shootDir);
            _queueCount++;
            if (_queueCount == _queueSize)
            {
                _state = BossStates.Reload;
                _queueCount = 0;
            }
        }
    }

    private void Shoot(Vector3 shootDir)
    {
        var _bullet = Instantiate(_bulletPrefab, _bulletStartPosion.position, Quaternion.identity);

        _bullet.GetComponentInChildren<Bullet>().Setup(shootDir);
    }

    public void TakeDamage(int damage)
    {

        Health -= damage;
        if (Health == 0)
        {
            _state = BossStates.Dead;

        }
        DamagePopup.Create(transform.position, damage);

    }

    private void Moving()
    {
        if (Vector2.Distance(_rb.position, _waypoint) < 0.1f)
            _waypoint = _startPosition + new Vector2(Random.Range(-_moveDistance, _moveDistance), Random.Range(-_moveDistance, _moveDistance));
        else
        {
            var dir = (_waypoint - _rb.position).normalized;
            _rb.MovePosition(_rb.position + dir * _moveSpeed * Time.fixedDeltaTime);
        }

    }

    private bool CheckActiveTotem(Vector3 shootDir)
    {
        RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, shootDir, 100, _totemLayerMask);
        print(raycastHit2d.collider);
        return raycastHit2d.collider != null && raycastHit2d.collider.GetComponent<Totem>() && raycastHit2d.collider.GetComponent<Totem>().isActive;
    }
    public enum BossStates { AFK, Reload, Attack, Dead }
}
