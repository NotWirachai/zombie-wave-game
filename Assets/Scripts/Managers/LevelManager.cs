using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }

    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLive(Enemy enemy) 
    {
        TotalLives--;
        if (TotalLives == 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void GameOver() 
    {
        UIManager.Instance.ShowGameOverPanel();
    }

    private void WaveCompleted()
    {
            CurrentWave++;
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLive;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLive;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}
