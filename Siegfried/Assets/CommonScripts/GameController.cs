using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public SaveSystem.Player_data player_data;
    public SaveSystem.Plot_data plot_data;

    public GameObject _character;

    public GameObject _dialogue;

    public List<Speech> _barSpeeches;

<<<<<<< Updated upstream
    public bool isFirstLevel;

    #region Inventory
    //public List<InventoryCell> Inventory;
    [SerializeField] private Item _healPotion;
    [SerializeField] private Item _mpPotion;
    [SerializeField] private Item _coin;

    #endregion



    private void Start()
    {
<<<<<<< Updated upstream

        if (SaveSystem.isSavedPlayerData() && isFirstLevel)
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

        if (SaveSystem.isSavedPlotData())
        {
            plot_data = SaveSystem.PlotLoad();
        }
        else
        {
            plot_data = SaveSystem.PlotDefault();
        }

        Invoke(nameof(BarSpeeches), 1f);
        SetupInventory();

    }

    public void SceneLoader()
    {
        SaveSystem.PlayerSave(player_data);
        SaveSystem.PlotSave(plot_data);
        if (_barSpeeches is null)
        {

            SceneManager.LoadScene(1);

        }

        else
        {
            if (_myLevel._muLevel == 0)
            {
                SceneManager.LoadScene(2);
            }

            if (_myLevel._muLevel == 1)
            {
                SceneManager.LoadScene(3);
            }

        }

    }

    public void BarSpeeches()
    {

        if (_barSpeeches != null)
        {

            if (plot_data.currentLevel == 0)
            {
                Dialogue.instance.StartDialgueOld(_barSpeeches[0]);
            }


            if (plot_data.currentLevel == 1)
            {
                Dialogue.instance.StartDialgueOld(_barSpeeches[1]);
            }


            if (plot_data.currentLevel == 2)
            {
                Dialogue.instance.StartDialgueOld(_barSpeeches[2]);
            }


        }

    }



    private void AddCoin(int _cnt)
    {
        player_data.coins += _cnt;
    }


    private void AddHealthPotion(int _cnt)
    {
        player_data.hpPotion += _cnt;
    }

    private void AddManaPotion(int _cnt)

    {
        player_data.mnPotion += _cnt;
    }

    public void AddItemCount(int _cnt, Item _item)
    {
        if (_item.itemName == _coin.itemName)
        {
            AddCoin(_cnt);
        }
        if (_item.itemName == _healPotion.itemName)
        {
            AddHealthPotion(_cnt);
        }
        if (_item.itemName == _mpPotion.itemName)
        {
            AddManaPotion(_cnt);
        }

    }


    private void SetupInventory()
    {
        var inventory = FindObjectOfType<Inventory>();
        if (inventory)
        {
            inventory.Init(player_data.hpPotion, _healPotion, player_data.mnPotion, _mpPotion, player_data.coins, _coin);
        }
    }






}
