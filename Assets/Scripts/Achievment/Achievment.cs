using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievment")]
public class Achievment : ScriptableObject
{
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;

    public bool IsUnlocked { get; set; }

    private int CurrentProgress;

    public void AddProgress(int amount) 
    {
        CurrentProgress += amount;
        AchievmentManager.OnProgressUpdated?.Invoke(this);
        CheckUnlockStatus();
    }

    private void CheckUnlockStatus() 
    {
        if (CurrentProgress >= ProgressToUnlock) 
        {
            UnlockAchievment(); 
        }
    }

    private void UnlockAchievment() 
    {
        IsUnlocked = true;
        AchievmentManager.OnAchievmentUnlocked?.Invoke(this);
    }

    public string GetProgress() 
    {
        return $"{CurrentProgress}/{ProgressToUnlock}";
    }

    public string GetProgressCompleted() 
    {
        return $"{ProgressToUnlock}/{ProgressToUnlock}";
    }

    private void OnEnable()
    {
        IsUnlocked = false;
        CurrentProgress = 0;
    }
}
