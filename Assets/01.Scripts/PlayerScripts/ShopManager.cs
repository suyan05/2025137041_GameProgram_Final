using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public DroneManager droneManager;

    [Header("기본 비용 설정")]
    public int baseCostMaxHealth = 10;
    public int baseCostFireRate = 15;
    public int baseCostMoveSpeed = 15;
    public int baseCostDrone = 20;

    [Header("UI 연결 (Text가 비어도 null 체크로 보호됨)")]
    public TextMeshProUGUI costTextMaxHealth;
    public TextMeshProUGUI costTextFireRate;
    public TextMeshProUGUI costTextMoveSpeed;
    public TextMeshProUGUI costTextDrone;

    private int levelMaxHealth = 0;
    private int levelFireRate = 0;
    private int levelMoveSpeed = 0;
    private int levelDrone = 0;

    void Start()
    {
        LoadUpgradeData();
        UpdateCostUI();
    }

    void LoadUpgradeData()
    {
        levelMaxHealth = PlayerPrefs.GetInt("Level_MaxHealth", 0);
        levelFireRate = PlayerPrefs.GetInt("Level_FireRate", 0);
        levelMoveSpeed = PlayerPrefs.GetInt("Level_MoveSpeed", 0);
        levelDrone = PlayerPrefs.GetInt("Level_Drone", 0);

        // 강화 효과도 반영해줘야 함
        playerHealth.IncreaseMaxHealth(levelMaxHealth * 20f);
        playerController.autoFireInterval -= 0.05f * levelFireRate;
        playerController.moveSpeed += 0.5f * levelMoveSpeed;
        for (int i = 0; i < levelDrone; i++)
            droneManager.IncreaseDroneCount();
    }

    void SaveUpgradeData()
    {
        PlayerPrefs.SetInt("Level_MaxHealth", levelMaxHealth);
        PlayerPrefs.SetInt("Level_FireRate", levelFireRate);
        PlayerPrefs.SetInt("Level_MoveSpeed", levelMoveSpeed);
        PlayerPrefs.SetInt("Level_Drone", levelDrone);
    }


    public void BuyMaxHealth()
    {
        int cost = baseCostMaxHealth + levelMaxHealth * 5;
        if (!CanAfford(cost)) return;

        GameManager.Instance.totalCoin -= cost;
        PlayerPrefs.SetInt("TotalCoin", GameManager.Instance.totalCoin);

        playerHealth.IncreaseMaxHealth(20f);
        levelMaxHealth++;
        SaveUpgradeData();
        UpdateCostUI();
    }

    public void BuyFireRate()
    {
        int cost = baseCostFireRate + levelFireRate * 5;
        if (!CanAfford(cost)) return;

        GameManager.Instance.totalCoin -= cost;
        PlayerPrefs.SetInt("TotalCoin", GameManager.Instance.totalCoin);

        playerController.autoFireInterval = Mathf.Max(0.05f, playerController.autoFireInterval - 0.05f);
        levelFireRate++;
        SaveUpgradeData();
        UpdateCostUI();
    }

    public void BuyMoveSpeed()
    {
        int cost = baseCostMoveSpeed + levelMoveSpeed * 5;
        if (!CanAfford(cost)) return;

        GameManager.Instance.totalCoin -= cost;
        PlayerPrefs.SetInt("TotalCoin", GameManager.Instance.totalCoin);

        playerController.moveSpeed += 0.5f;
        levelMoveSpeed++;
        SaveUpgradeData();
        UpdateCostUI();
    }

    public void BuyDroneUpgrade()
    {
        int cost = baseCostDrone + levelDrone * 10;
        if (!CanAfford(cost)) return;

        GameManager.Instance.totalCoin -= cost;
        PlayerPrefs.SetInt("TotalCoin", GameManager.Instance.totalCoin);

        droneManager.IncreaseDroneCount();
        levelDrone++;
        SaveUpgradeData();
        UpdateCostUI();
    }

    private bool CanAfford(int cost)
    {
        return GameManager.Instance != null && GameManager.Instance.totalCoin >= cost;
    }


    void UpdateCostUI()
    {
        costTextMaxHealth.text = $"HelthUp\nCoin: {baseCostMaxHealth + (levelMaxHealth * 5)}";
        costTextFireRate.text = $"AttackSpeedUp\nCoin: {baseCostFireRate + (levelFireRate * 5)}";
        costTextMoveSpeed.text = $"SpeedUp\nCoin: {baseCostMoveSpeed + (levelMoveSpeed * 5)}";
        costTextDrone.text = $"DroneUp\nCoin: {baseCostDrone + (levelDrone * 10)}";
    }
}