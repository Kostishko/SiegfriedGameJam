using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health potion", menuName = "ScriptableObjects/Items/Health Potion", order = 1)]
public class HealthPotion : Item
{
    public int amount;
    public override bool Use()
    {
        FindObjectOfType<CharacterState>().TakeHeal(amount);
        return true;
    }



}
