using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFight : MonoBehaviour
{
    [SerializeField] float _attackOffset = 5f;

    private void Update()
    {
        HandleAttack();
    }
    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDir = (mousePosition - transform.position).normalized;

            Vector3 attackPosition = transform.position + mouseDir * _attackOffset;

            Vector3 attackDir = mouseDir;

            //float attackRange = 10f;

        }
    }

}
