using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mana potion", menuName = "ScriptableObjects/Items/Mana Potion", order = 1)]
public class ManaPotion : Item
{
    public int amount;
    public override void Use()
    {
        FindObjectOfType<CharacterState>().TakeMana(amount);
    }



}
