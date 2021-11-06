using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    private List<CharacterState> clients = new List<CharacterState>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        var charState = other.GetComponent<CharacterState>();
        if (charState)
            StartCoroutine(Burning(charState));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var charState = other.GetComponent<CharacterState>();
        if (charState && clients.Contains(charState))
            clients.Remove(charState);
    }

    IEnumerator Burning(CharacterState character)
    {
        clients.Add(character);

        while (clients.Contains(character))
        {
            character.TakeDamage(_damage);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
