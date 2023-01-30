using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject achievmentPanel;
    [SerializeField] private GameObject pauseGamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject selectLevelsPanel;
    [SerializeField] private GameObject tipsForPlayPanel;
    [SerializeField] private GameObject buttonStart;
    [SerializeField] private GameObject buttonCloseNode;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;
    [SerializeField] private TextMeshProUGUI gameWonTotalCoinsText;
    [SerializeField] private TextMeshProUGUI WonLifesText;

    [Header("Sound")]
    [SerializeField] protected AudioClip WinSound;
    [SerializeField] protected AudioClip GameoverSound;

    private AudioSource Sound;

    private Node _currentNodeSelected;

    [Header("Next Level")]
    //  [SerializeField] private SceneFader fader;
    [SerializeField] private LoadingManager loading;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Time.timeScale = 0f;
        Sound = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("levelReached") != 0)
        {
            tipsForPlayPanel.SetActive(false);
        }
    }

    private void Update()
    {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        lifesText.text = LevelManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave}/{Spawner.Instance.TotalWave}";
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseGamePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        buttonStart.SetActive(false);
    }

    public void ShowGameOverPanel() 
    {
        gameOverPanel.SetActive(true);
        Sound.PlayOneShot(GameoverSound);
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        Time.timeScale = 0f;
    }

    public void ShowWonLevelPanel() 
    {
        gameWonPanel.SetActive(true);
        Sound.PlayOneShot(WinSound);
        gameWonTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        WonLifesText.text = LevelManager.Instance.TotalLives.ToString();
    }

    public void CloseTipsPanel()
    {
        tipsForPlayPanel.SetActive(false);
    }

    public void RestartGame() 
    {
        loading.LoadScreen(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void SelectScene(string levelName)
    {
        Time.timeScale = 1f;
        // fader.FadeTo(levelName);
        loading.LoadScreen(levelName);
    }

    public void OpenAchievmentPanel(bool status) 
    {
        achievmentPanel.SetActive(status);
    }

    public void ShowPauseGamePanel(bool status)
    {
        pauseGamePanel.SetActive(status);
        if (status)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1f;
        }
    }

    public void OpenLevelSelecterPanel(bool status) 
    {
        selectLevelsPanel.SetActive(status);
    }

    public void CloseTurretShopPanel() 
    {
        turretShopPanel.SetActive(false);
    }

    public void CloseNodeUIPanel() 
    {
        _currentNodeSelected.CloseAttackRangeSprite();
        buttonCloseNode.SetActive(false);
        nodeUIPanel.SetActive(false);
    }

    public void UpgradeTurret() 
    {
        _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
        UpdateUpgradeText();
        UpdateUpgradeLevel();
        UpdateSellValue();
    }

    public void SellTurret() 
    {
        _currentNodeSelected.SellTurret();
        _currentNodeSelected = null;
        buttonCloseNode.SetActive(false);
        nodeUIPanel.SetActive(false);
    }

    private void ShowNodeUI() 
    {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateUpgradeLevel();
        UpdateSellValue();
    }

    private void UpdateUpgradeText() 
    {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }

    private void UpdateUpgradeLevel() 
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }

    private void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }

    private void NodeSelected(Node nodeSelected) 
    {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty())
        {
            turretShopPanel.SetActive(true);
        }
        else 
        {
            ShowNodeUI();
            buttonCloseNode.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
