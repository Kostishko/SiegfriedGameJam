using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public SaveSystem.Player_data player_data;

    public GameObject _character;

    public List<InventoryCell> Inventory;




    private void Start()
    {

        if (SaveSystem.isSavedPlayerData())
        {
            player_data = SaveSystem.PlayerLoad();
        }
        else
        {
            player_data = SaveSystem.PlayerDefault();
        }

       
        if (!_character)
        {
            _character = GameObject.FindGameObjectWithTag("Character");
            if (!_character)
            {
                print("Game controller can't find Character!");
            }
            else
            {
                var _mlDamage = _character.GetComponent<CharacterState>().meleeDamage;
                if (player_data.critChance < 100)
                {
                    _mlDamage = player_data.minMeleeAttack;
                }
                else
                {
                    _mlDamage = 777;
                }

            }
        }
       
    }


    public void AddCoin (int _cnt)
    {
        player_data.coins += _cnt;
    }


    public void AddHealthPotion (int _cnt)
    {
        player_data.hpPotion += _cnt;
    }

    public void AddManaPotion(int _cnt)
    {
        player_data.mnPotion += _cnt;
    }









}
