using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrenLevel : MonoBehaviour
{

    public int _muLevel;

    
    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }

}
