using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class TurretCard : MonoBehaviour
{
    public static Action<TurretSetting> OnPlaceTurret;

    [SerializeField] private Image TurretImage;
    [SerializeField] private TextMeshProUGUI TurretCost;

    public TurretSetting TurretLoaded { get; set; }

    public void SetupTurretButton(TurretSetting turretSetting) 
    {
        TurretLoaded = turretSetting;
        TurretImage.sprite = turretSetting.TurretShopSprite;
        TurretCost.text = turretSetting.TurretShopCost.ToString();
    }

    public void PlaceTurret() 
    {
        if (CurrencySystem.Instance.TotalCoins >= TurretLoaded.TurretShopCost)
        {
            CurrencySystem.Instance.RemoveCoins(TurretLoaded.TurretShopCost);
            UIManager.Instance.CloseTurretShopPanel();
            OnPlaceTurret?.Invoke(TurretLoaded);
        }
    }
}
