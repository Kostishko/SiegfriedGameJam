using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SimpleTrigger : MonoBehaviour
{
    public GameObject gObject;
    public string methodName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterState>())
        {
            gObject.SendMessage(methodName);
            Destroy(this.gameObject);
        }
    }


}
