using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public SaveSystem._data _data;

    // Start is called before the first frame update
    void Start()
    {
        _data = SaveSystem.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
