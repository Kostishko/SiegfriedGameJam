using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


public static class SaveSystem
{


    public class Player_data
    {
        [DataMember]
        public int coins;

        [DataMember]
        public int hpPotion;

        [DataMember]
        public int mnPotion;

        [DataMember]
        public int minMeleeAttack;

        [DataMember]
        public int maxMeleeAttack;

        /*
        [DataMember]
        public int minRangeAttack;

        [DataMember]
        public int maxRangeAttack;
        */
        [DataMember]
        public int critChance;

    }


    public static void PlayerSave(Player_data _data)
    {
        string _resource = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString("Player_data", _resource);

    }

    public static Player_data PlayerLoad()
    {
        Player_data _data = JsonUtility.FromJson<Player_data>(PlayerPrefs.GetString("Player_data"));
        return _data;
    }

    public static bool isSavedPlayerData()
    {
        if (PlayerPrefs.GetString("Player_data")!=null)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public static Player_data PlayerDefault ()
    {
        Player_data _data = new Player_data();
        _data.coins = 0;
        _data.hpPotion = 0;
        _data.mnPotion = 0;
        _data.minMeleeAttack = 25;
        _data.maxMeleeAttack = 35;
        /*
        _data.minRangeAttack = 10;
        _data.maxRangeAttack = 15;
        */
        _data.critChance = 3;
        return _data;

    }



}