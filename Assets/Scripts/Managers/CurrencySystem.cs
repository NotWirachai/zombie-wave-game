using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : Singleton<CurrencySystem>
{
    [SerializeField] private int coinTest;
    private string CURRENCY_SAVE_KEY = "MYGAME_CURRENCY";
    public int TotalCoins { get; set; }

    private void Start()
    {
        PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
        LoadCoins();    
    }

    private void LoadCoins() 
    {
        TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY, coinTest);
    }
    public void AddCoins(int amount) 
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(int amount) 
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
        }
    }

    private void AddCoins(Enemy enemy)
    {
        if (enemy.ToString() == "origin_zombie_low(Clone) (Enemy)" || enemy.ToString() == "origin_zombie(Clone) (Enemy)")
        {
            AddCoins(2);
        }
        
        if (enemy.ToString() == "mischievous_zombie_low(Clone) (Enemy)" || enemy.ToString() == "mischievous_zombie(Clone) (Enemy)")
        {
            AddCoins(3);
        }

        if (enemy.ToString() == "prisoner_zombie_low(Clone) (Enemy)" || enemy.ToString() == "prisoner_zombie(Clone) (Enemy)")
        {
            AddCoins(4);
        }

        if (enemy.ToString() == "girl_zombie_low(Clone) (Enemy)" || enemy.ToString() == "girl_zombie(Clone) (Enemy)")
        {
            AddCoins(5);
        }

        if (enemy.ToString() == "baby_girl_zombie_low(Clone) (Enemy)" || enemy.ToString() == "baby_girl_zombie(Clone) (Enemy)")
        {
            AddCoins(10);
        }

        else if (enemy.ToString() == "gamma_zombie_low(Clone) (Enemy)" || enemy.ToString() == "gamma_zombie(Clone) (Enemy)")
        {
            AddCoins(15);
        }

        if (enemy.ToString() == "general_zombie_low(Clone) (Enemy)" || enemy.ToString() == "general_zombie(Clone) (Enemy)")
        {
            AddCoins(20);
        }

        if (enemy.ToString() == "omega_zombie_low(Clone) (Enemy)" || enemy.ToString() == "omega_zombie(Clone) (Enemy)")
        {
            AddCoins(30);
        }

        if (enemy.ToString() == "alpha_zombie(Clone) (Enemy)")
        {
            AddCoins(100);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }
}
