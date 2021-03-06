using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour, IDamageable
{
    public event Action<int, int> OnHealthChange;
    public event Action<int, int> OnManaChange;
    public event Action OnDie;

    public CharacterStates state = CharacterStates.Normal;

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
    [SerializeField] private int _manaRegeneration = 1;
    #endregion

    #region Attack stats
    public int meleeDamage;
    //public int rangedDamage;
    #endregion

    void Start()
    {
        Health = _maxHealth;
        Mana = _maxMana;

        StartCoroutine(ManaRegeneration());
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        DamagePopup.Create(transform.position, damage);
        StartCoroutine(Blinking());

        if (Health == 0)
        {
            OnDie?.Invoke();
            state = CharacterStates.Dead;
        }
    }

    public void TakeHeal(int heal)
    {
        Health += heal;
    }
    public void TakeMana(int mana)
    {
        Mana += mana;
    }

    public void SpendMana(int mana)
    {
        Mana -= mana;
    }

    IEnumerator ManaRegeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Mana < _maxMana)
                Mana += _manaRegeneration;
        }
    }

    IEnumerator Blinking()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var time = 0.1f;
        var scaleTime = 1f / time;
        while (time > 0)
        {
            time -= Time.deltaTime;
            renderer.color = Color.Lerp(Color.red, Color.white, time * scaleTime);
            yield return new WaitForEndOfFrame();
        }
        time = 0.1f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            renderer.color = Color.Lerp(Color.white, Color.red, time * scaleTime);
            yield return new WaitForEndOfFrame();
        }
        renderer.color = Color.white;
    }
}

public enum CharacterStates
{
    Normal,
    MeleeAttack,
    Dead
}
