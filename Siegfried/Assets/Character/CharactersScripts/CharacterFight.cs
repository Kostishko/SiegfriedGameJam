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

    private void Start()
    {
        _machineGun = GetComponentInChildren<MachineGun>();
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
            Vector3 mouseDir = (mousePosition - transform.position).normalized;

            //Vector3 attackPosition = transform.position + mouseDir * _attackOffset;

            //float attackRange = 10f;

            var plumeTransform = Instantiate(_plume, transform.position, Quaternion.identity).GetComponent<Transform>();
            plumeTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg);
            plumeTransform.GetComponentInChildren<Plume>().OnEnter += OnEnterAttackPlume;
            Destroy(plumeTransform.gameObject, 0.3f);

        }
        if (Input.GetMouseButtonDown(1))
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
            print($"Attack {enemy}");
        }
    }
}
