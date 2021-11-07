using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public List<GameObject> SpawnPoints;
    public enum SpawnState : byte
    {
        WAITING,
        SPAWNING,
        STAGING
    }
    public SpawnState spawnState = SpawnState.STAGING;

    public bool isWaveShouldStart = false; // Bool for wave starting

    [SerializeField]
    private List<Wave> ListOfWaves;
    public int curWaveNumber; 




    private void Start()
    {

        FillWavesList(); // load waves from scene
        

    }

    private void Update()
    {
        if (spawnState==SpawnState.WAITING)
        {
            if (!isEnemyAlive())
            {
                waveComplited();
            }
            else
            {
                return;
            }

        }

        if (isWaveShouldStart) // here comand to start
        {
            if (spawnState != SpawnState.SPAWNING)
            {
                isWaveShouldStart = false;
                StartCoroutine(WaveSpawn(ListOfWaves[curWaveNumber]));
                


            }
        }

    }


    private void FillWavesList ()
    {
        for (int i =0; i< this.transform.childCount; i++)
        {
            var _wave = this.transform.GetChild(i).GetComponent<Wave>();
            if (_wave!=null)
            {
                ListOfWaves.Add(_wave);

            }
        }

    }



    // enemy alive check
    private bool isEnemyAlive()
    {

        return false;
    }

    // complite current wave

    private void waveComplited()
    {

        Debug.Log("Wave Completed!");

        spawnState = SpawnState.STAGING;
        Debug.Log("SpawnState=" + spawnState);
        
        if (curWaveNumber + 1 > ListOfWaves.Count - 1)
        {

            Debug.Log("ALL WAVES COMPLETE!");
           
        }

        if (ListOfWaves[curWaveNumber]._block!=null)
        {
            Destroy(ListOfWaves[curWaveNumber]._block);
        }



    }

    IEnumerator WaveSpawn (Wave _wave)
    {
        Debug.Log(" Wave " + _wave.WaveNumber);
        spawnState = SpawnState.SPAWNING;
        Debug.Log("SpawnState = " + spawnState);

        for (int i =0; i<_wave.lightEnemyCount; i++)
        {
            float _random = Random.Range(0, _wave.lightEnemys.Count);
            GameObject _currEnemy = _wave.lightEnemys[(int)Mathf.Round(_random)];
            float _rnd = Random.Range(0, _wave._spawnPoint.Count);
            Transform _sp = _wave._spawnPoint[(int)Mathf.Round(_rnd)].transform;
            SpawnEnemy(_currEnemy, _sp);
            yield return new WaitForSeconds(_wave.waitForLightSpawnEnemy * Time.deltaTime);
        }

        for (int i = 0; i < _wave.hardEnemyCount; i++)
        {
            float _random = Random.Range(0, _wave.hardEnemys.Count);
            GameObject _currEnemy = _wave.hardEnemys[(int)Mathf.Round(_random)];
            float _rnd = Random.Range(0, _wave._spawnPoint.Count);
            Transform _sp = _wave._spawnPoint[(int)Mathf.Round(_rnd)].transform;
            SpawnEnemy(_currEnemy, _sp);
            yield return new WaitForSeconds(_wave.waitForHardSpawnEnemy * Time.deltaTime);
        }


        spawnState = SpawnState.WAITING;
        Debug.Log("SpawnState=" + spawnState);
        yield break;

    }


    public void SpawnEnemy(GameObject _enemy, Transform _pos)
    {

        var _en = Instantiate(_enemy, _pos.position, _pos.rotation);
    }

    public void AllowNextWaveStart()
    {
        isWaveShouldStart = true;
    }




}
