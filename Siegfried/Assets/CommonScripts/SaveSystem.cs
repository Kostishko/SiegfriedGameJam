using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


public static class SaveSystem
{

    #region Player_data
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
        string _string= JsonUtility.ToJson(_data);

        PlayerPrefs.SetString("Player_data", _string);

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
            return true;
        }

        else
        {
            return false;
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

#endregion



    public class Plot_data
    {
        [DataMember]
        public string FirstLevel;

        [DataMember]
        public string SecondLevel;

        [DataMember]
        public int currentLevel;
    }

    public static void PlotSave (Plot_data _data)
    {
        string _string = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString("Plot_Data", _string);



    }


    public static Plot_data PlotLoad()
    {
        Plot_data _data = JsonUtility.FromJson<Plot_data>(PlayerPrefs.GetString("Plot_data"));
        return _data;
    }

    public static bool isSavedPlotData()
    {
        if (PlayerPrefs.GetString("Plot_data") != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public static Plot_data PlotDefault()
    {
        Plot_data _data = new Plot_data();
        _data.FirstLevel = "Nothing";
        _data.SecondLevel = "Nothing";
        _data.currentLevel = 0;

        return _data;

    }


}