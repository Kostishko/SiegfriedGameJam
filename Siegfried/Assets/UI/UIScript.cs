using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] UIBar healthBar;
    [SerializeField] UIBar manaBar;
    [SerializeField] GameOver gameOver;

    void Start()
    {
        var characterState = FindObjectOfType<CharacterState>();
        characterState.OnHealthChange += healthBar.UpdateBar;
        characterState.OnManaChange += manaBar.UpdateBar;
        characterState.OnDie += gameOver.ShowGameOver;
    }
}
