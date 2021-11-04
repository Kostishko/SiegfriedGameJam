using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public event Action<int, int> OnHealthChange;
    public event Action OnDie;

    #region HP
    [SerializeField] private int maxHealth;
    private int health;
    public int Health
    {
        get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChangeHandler();
        }
    }
    #endregion

    #region Attack stats
    public float meleeDamage;
    public float rangedDamage;
    #endregion

    void Start()
    {
        Health = maxHealth;
    }

    private void OnHealthChangeHandler()
    {
        OnHealthChange?.Invoke(Health, maxHealth);
        if (Health == 0)
            OnDie?.Invoke();
    }
}
