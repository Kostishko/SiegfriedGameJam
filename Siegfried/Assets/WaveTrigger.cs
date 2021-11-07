using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField] private SpawnController _controller;
    [SerializeField] private int _waveNumber;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            _controller.curWaveNumber = _waveNumber;
            _controller.AllowNextWaveStart();

            Destroy(this.gameObject);

        }
    }


}
