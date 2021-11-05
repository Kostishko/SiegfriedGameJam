using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [SerializeField] private float _damage = 5f;
    [SerializeField] private GameObject _playerCharacter;
    


    private void Start()
    {
        _playerCharacter = GameObject.FindGameObjectWithTag("Character");



    }




}
