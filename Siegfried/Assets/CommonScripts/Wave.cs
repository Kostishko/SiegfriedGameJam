using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> _spawnPoint;

    public List<GameObject> lightEnemys;
    public int lightEnemyCount;

    public List<GameObject> hardEnemys;
    public int hardEnemyCount;

    public  GameObject _block;

    public int WaveNumber;

    // time between spawn
    public float waitForLightSpawnEnemy;
    public float waitForHardSpawnEnemy;



}
