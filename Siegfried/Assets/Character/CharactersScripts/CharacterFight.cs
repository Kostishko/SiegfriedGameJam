using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFight : MonoBehaviour
{
    [SerializeField] float _attackOffset = 5f;
    [SerializeField] float _meleeTimeBetweenAttacks = 1f;
    [SerializeField] private GameObject _plume;

    private MachineGun _machineGun;
    private float _timeSinceMeleeAttack;
    private CharacterState _charState;
    private Animator _animator;
    private readonly int _animAtack = Animator.StringToHash("MeleeAtack");
    private Vector3 _attackDir;

    private void Start()
    {
        _machineGun = GetComponentInChildren<MachineGun>();
        _charState = GetComponent<CharacterState>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        _timeSinceMeleeAttack += Time.deltaTime;

        HandleAttack();
    }
    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && _timeSinceMeleeAttack > _meleeTimeBetweenAttacks)
        {
            _timeSinceMeleeAttack = 0f;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _attackDir = (mousePosition - transform.position).normalized;

            //Vector3 attackPosition = transform.position + mouseDir * _attackOffset;

            //float attackRange = 10f;

            _animator.SetTrigger(_animAtack);
            _charState.state = CharacterStates.MeleeAttack;
            _machineGun.gameObject.SetActive(false);
            CreatePlumb();
        }
        if (Input.GetMouseButtonDown(1) && _charState.state != CharacterStates.MeleeAttack)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _machineGun.Shoot();
    }

    private void OnEnterAttackPlume(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.takeDamage(_charState.meleeDamage);
        }
    }

    #region Animation events
    public void EndMeleeAtack()//Animation event
    {
        _charState.state = CharacterStates.Normal;
        _machineGun.gameObject.SetActive(true);
    }
    #endregion

    private void CreatePlumb()
    {
        float alpha = _attackDir.y < 0 && Mathf.Abs(_attackDir.y) > Mathf.Abs(_attackDir.x) ? 0f : 1f;

        var plumeTransform = Instantiate(_plume, transform.position, Quaternion.identity).GetComponent<Transform>();
        plumeTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_attackDir.y, _attackDir.x) * Mathf.Rad2Deg);
        plumeTransform.GetComponentInChildren<Plume>().OnEnter += OnEnterAttackPlume;
        plumeTransform.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        Destroy(plumeTransform.gameObject, 0.3f);
    }
}
