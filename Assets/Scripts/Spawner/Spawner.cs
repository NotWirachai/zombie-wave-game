using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes 
{
    Fixed,
    Random
}


public class Spawner : Singleton<Spawner>
{
    public static Action OnWaveCompleted;

    [Header("Level Won")]
    public int TotalWave = 3;
    [SerializeField] private int levelUnlock = 2;

    [Header("Setting")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWave = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")]
    [SerializeField] private List<ObjectPooler> enemyWavePooler;
    [SerializeField] private List<ObjectPooler> enemyWave2Pooler;
    [SerializeField] private List<ObjectPooler> enemyWave3Pooler;
    [SerializeField] private List<ObjectPooler> enemyWave4Pooler;
    [SerializeField] private List<ObjectPooler> enemyWave5Pooler;
    [SerializeField] private List<ObjectPooler> enemyWave6Pooler;
    [SerializeField] private List<ObjectPooler> enemyWave7Pooler;
    [SerializeField] private List<ObjectPooler> enemyWave8Pooler;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;

    private Waypoint _waypoint;
    private void Start()
    {
        _waypoint = GetComponent<Waypoint>();

        _enemiesRamaining = enemyCount;
    }

    // Update is called once per frame
    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy() 
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay() 
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else 
        {
            delay = GetRandomDelay();
        }
        return delay;
    }
    private float GetRandomDelay() 
    {
        float randomTimer = UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private ObjectPooler GetPooler() 
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave == 1)// 1- 10
        {
            for (int i = 0; i < enemyWavePooler.Count; i++)
            {
                return enemyWavePooler[UnityEngine.Random.Range(0, enemyWavePooler.Count)];
            }
        }

       if (currentWave == 2 ) // 11- 20
        {
            for (int i = 0; i < enemyWave2Pooler.Count; i++)
            {
                return enemyWave2Pooler[UnityEngine.Random.Range(0, enemyWave2Pooler.Count)];
            }
        }
        
        if (currentWave == 3 ) // 21- 30
        {
            for (int i = 0; i < enemyWave3Pooler.Count; i++)
            {
                return enemyWave3Pooler[UnityEngine.Random.Range(0, enemyWave3Pooler.Count)];
            }
        }
        
        if (currentWave == 4 ) // 31- 40
        {
            for (int i = 0; i < enemyWave4Pooler.Count; i++)
            {
                return enemyWave4Pooler[UnityEngine.Random.Range(0, enemyWave4Pooler.Count)];
            }
        }

        if (currentWave == 5 ) // 41- 50
        {
            for (int i = 0; i < enemyWave5Pooler.Count; i++)
            {
                return enemyWave5Pooler[UnityEngine.Random.Range(0, enemyWave5Pooler.Count)];
            }
        }
        
        if (currentWave == 6 ) // 51- 60
        {
            for (int i = 0; i < enemyWave6Pooler.Count; i++)
            {
                return enemyWave6Pooler[UnityEngine.Random.Range(0, enemyWave6Pooler.Count)];
            }
        } 
        
        if (currentWave == 7 ) // 61- 70
        {
            for (int i = 0; i < enemyWave7Pooler.Count; i++)
            {
                return enemyWave7Pooler[UnityEngine.Random.Range(0, enemyWave7Pooler.Count)];
            }
        }
        
        if (currentWave == 8 ) // 71- 80
        {
            for (int i = 0; i < enemyWave8Pooler.Count; i++)
            {
                return enemyWave8Pooler[UnityEngine.Random.Range(0, enemyWave8Pooler.Count)];
            }
        }

        return null;
    }

    private IEnumerator NextWave() 
    {
        yield return new WaitForSeconds(delayBtwWave);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemy(Enemy enemy) 
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        int totalLives = LevelManager.Instance.TotalLives;
        Debug.Log("currentWave:>>" + currentWave + "totalLives" + totalLives);
        _enemiesRamaining--;
        if (_enemiesRamaining == 0) 
        {
            if (currentWave < TotalWave)
            {
                OnWaveCompleted?.Invoke();
                StartCoroutine(NextWave());
            }
            else if(totalLives > 0 && currentWave == TotalWave)
            {
                GameWon();
            }
        }
    }

    private void GameWon()
    {
        int totalLives = LevelManager.Instance.TotalLives;
        Debug.Log("totalLives" + totalLives);
        if (totalLives > 0)
        {
            StartCoroutine(WaitForWin());
        }
    }

    private IEnumerator WaitForWin()
    {
            yield return new WaitForSeconds(2f);
            UIManager.Instance.ShowWonLevelPanel();
        if (levelUnlock > PlayerPrefs.GetInt("levelReached")) 
        {
            PlayerPrefs.SetInt("levelReached", levelUnlock);
        }
    }


    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;

    }
}
