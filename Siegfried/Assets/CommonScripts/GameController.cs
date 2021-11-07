using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameController : MonoBehaviour
{

    public SaveSystem.Player_data player_data;

    public GameObject _character;


    #region Inventory
    public List<InventoryCell> Inventory;
    [SerializeField] private Item _healPotion;
    [SerializeField] private Item _mpPotion;
    [SerializeField] private Item _coin;

    #endregion



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


    private void AddCoin (int _cnt)
    {
        player_data.coins += _cnt;
    }


    private void AddHealthPotion (int _cnt)
    {
        player_data.hpPotion += _cnt;
    }

    private void AddManaPotion(int _cnt)
    {
        player_data.mnPotion += _cnt;
    }

    public void AddItemCount (int _cnt, Item _item)
    {
        if (_item == _coin)
        {
            AddCoin(_cnt);
        }
        if (_item == _healPotion)
        {
            AddHealthPotion(_cnt);
        }
        if (_item == _mpPotion)
        {
            AddManaPotion(_cnt);
        }

    }








}
