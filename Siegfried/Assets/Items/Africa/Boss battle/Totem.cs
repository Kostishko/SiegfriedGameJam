using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public bool isActive;
    public Action onActivate;
    private ParticleSystem _smoke;

    private void Start()
    {
        _smoke = GetComponentInChildren<ParticleSystem>();
        _smoke.Stop();
    }

    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            onActivate.Invoke();
            _smoke.Play();
        }
    }

    public void Disactivate()
    {
        isActive = false;
        _smoke.Stop();
    }
}
