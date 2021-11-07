using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private GameController _controller;
    public int myLevel;
    public BossAfrica myBoss;


    public void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            if (myBoss.Health <= 0)
            {
                _controller.plot_data.currentLevel = myLevel;
                _controller.SceneLoader();
                Destroy(this.gameObject);
            }

        }


    }





}
