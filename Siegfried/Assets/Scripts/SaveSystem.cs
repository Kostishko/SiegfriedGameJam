using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


public static class SaveSystem
{


    public class _data
    {


    }


    public static void Save(_data _data)
    {
        string _resource = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString("_data", _resource);

    }

    public static _data Load()
    {
        _data _data = JsonUtility.FromJson<_data>(PlayerPrefs.GetString("_data"));
        return _data;
    }

}