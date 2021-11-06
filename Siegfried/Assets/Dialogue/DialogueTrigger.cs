using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueTrigger : MonoBehaviour
{
    public Speech speech;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterState>())
        {
            Dialogue.instance.StartDialgue(speech);

            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
