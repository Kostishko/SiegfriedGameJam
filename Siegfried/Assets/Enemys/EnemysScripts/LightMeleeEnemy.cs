using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMelee : MonoBehaviour
{

    [SerializeField] private float _range;
    private Enemy _enemy;
 

    private float _timeToActionCheck;




    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }











}
