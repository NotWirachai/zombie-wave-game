using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentManager : Singleton<AchievmentManager>
{
    public static Action<Achievment> OnAchievmentUnlocked;
    public static Action<Achievment> OnProgressUpdated;

    [SerializeField] private AchievmentCard achievmentCardPrefab;
    [SerializeField] private Transform achievmentPanelContainer;
    [SerializeField] private Achievment[] achievments;

    private void Start()
    {
        LoadAchievment();
    }

    private void LoadAchievment() 
    {
        for (int i = 0; i < achievments.Length; i++)
        {
            AchievmentCard card = Instantiate(achievmentCardPrefab, achievmentPanelContainer);
            card.SetupAchievment(achievments[i]);
        }
    }

    public void AddProgress(string achievmentID,int amount) 
    {
        Achievment achievmentWanted = AchievmentExists(achievmentID);
        if (achievmentWanted != null)
        {
            achievmentWanted.AddProgress(amount);
        }
    }

    private Achievment AchievmentExists(string achievmentID) 
    {
        for (int i = 0; i < achievments.Length; i++)
        {
            if (achievments[i].ID == achievmentID)
            {
                return achievments[i];
            }
        }

        return null;
    }
}
