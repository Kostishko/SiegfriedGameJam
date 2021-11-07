using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    private List<IDamageable> clients = new List<IDamageable>();

    private AudioSource _burnAudio;

    private void Start()
    {
        _burnAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var charState = other.GetComponent<IDamageable>();
        if (charState != null)
        {
            StartCoroutine(Burning(charState));
            if (!_burnAudio.isPlaying)
                _burnAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var charState = other.GetComponent<IDamageable>();
        if (charState != null && clients.Contains(charState))
            clients.Remove(charState);

        if (clients.Count == 0) _burnAudio.Stop();
    }

    IEnumerator Burning(IDamageable character)
    {
        clients.Add(character);

        while (clients.Contains(character))
        {
            character.TakeDamage(_damage);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
