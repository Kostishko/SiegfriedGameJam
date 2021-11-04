using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public event Action<int, int> OnHealthChange;
    public event Action<int, int> OnManaChange;
    public event Action OnDie;

    #region HP
    [SerializeField] private int _maxHealth;
    private int _health;
    public int Health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            OnHealthChange?.Invoke(_health, _maxHealth);
        }
    }
    #endregion

    #region Mana
    [SerializeField] private int _maxMana;
    private int _mana;
    public int Mana
    {
        get => _mana;
        private set
        {
            _mana = Mathf.Clamp(value, 0, _maxMana);
            OnManaChange?.Invoke(_mana, _maxMana);
        }
    }
    #endregion

    #region Attack stats
    public float meleeDamage;
    public float rangedDamage;
    #endregion

    void Start()
    {
        Health = _maxHealth;
        Mana = _maxMana;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health == 0)
            OnDie?.Invoke();
    }

    public void TakeHeal(int heal)
    {
        Health += heal;
    }
    public void TakeMana(int mana)
    {
        Mana += mana;
    }
}
