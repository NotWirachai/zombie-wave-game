using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AchievmentCard : MonoBehaviour
{
    [SerializeField] private Image achievmentImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Button rewardButton;

    public Achievment AchievmentLoaded { get; set; }

    public void SetupAchievment(Achievment achievment) 
    {
        AchievmentLoaded = achievment;
        achievmentImage.sprite = achievment.Sprite;
        title.text = achievment.Title;
        progress.text = achievment.GetProgress();
        reward.text = achievment.GoldReward.ToString();
    }

    public void GetReward() 
    {
        if (AchievmentLoaded.IsUnlocked)
        {
            CurrencySystem.Instance.AddCoins(AchievmentLoaded.GoldReward);
            rewardButton.gameObject.SetActive(false);
        }
    }

    private void LoadAchievmentProgress() 
    {
        if (AchievmentLoaded.IsUnlocked)
        {
            progress.text = AchievmentLoaded.GetProgressCompleted();
        }
        else
        {
            progress.text = AchievmentLoaded.GetProgress();
        }
    }

    private void CheckRewardButtonStatus() 
    {
        if (AchievmentLoaded.IsUnlocked)
        {
            rewardButton.interactable = true;
        }
        else 
        {
            rewardButton.interactable = false;
        }
    }

    private void UpdateProgress(Achievment achievmentWithProgress)
    {
        if (AchievmentLoaded == achievmentWithProgress) 
        {
            LoadAchievmentProgress();
        }
    }

    private void AchievmentUnlocked(Achievment achievment)
    {
        if (AchievmentLoaded == achievment)
        {
            CheckRewardButtonStatus();
        }
    }

    private void OnEnable()
    {
        CheckRewardButtonStatus();
        LoadAchievmentProgress();
        AchievmentManager.OnProgressUpdated += UpdateProgress;
        AchievmentManager.OnAchievmentUnlocked += AchievmentUnlocked;
    }

    private void OnDisable()
    {
        AchievmentManager.OnProgressUpdated -= UpdateProgress;
        AchievmentManager.OnAchievmentUnlocked -= AchievmentUnlocked;
    }
}
