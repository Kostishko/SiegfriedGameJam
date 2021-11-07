using System.Linq;
using UnityEngine;


public class FireArea : MonoBehaviour
{
    private Totem[] totems;
    private ParticleSystem _fire;

    private AudioSource _audio;

    void Start()
    {
        _fire = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        totems = GetComponentsInChildren<Totem>();
        foreach (var totem in totems)
        {
            totem.onActivate = onTotemActivated;
        }
    }

    private void onTotemActivated()
    {
        if (totems.Count(t => t.isActive) == totems.Length)
        {
            IgniteTheArea();
            Invoke(nameof(Disactivate), 2f);
        }
    }

    private void IgniteTheArea()
    {
        _fire.Play();
        _audio.Play();
        var boss = FindObjectOfType<BossAfrica>();
        if (boss) boss.TakeDamage(10);
    }

    private void Disactivate()
    {
        foreach (var totem in totems)
        {
            totem.Disactivate();
        }
    }
}
