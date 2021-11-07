using System.Linq;
using UnityEngine;


public class FireArea : MonoBehaviour
{
    private Totem[] totems;
    private ParticleSystem _fire;

    void Start()
    {
        _fire = GetComponent<ParticleSystem>();
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
