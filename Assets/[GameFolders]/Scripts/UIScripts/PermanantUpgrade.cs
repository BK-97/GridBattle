using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermanantUpgrade : MonoBehaviour
{
    public TextMeshProUGUI upgradeText;
    public string upgradeName;
    public TextMeshProUGUI coinText;
    private int currentCost;
    public int baseCost;
    public TextMeshProUGUI levelText;
    private int upgradeLevel;
    public Button upgradeButton;
    public void Start()
    {
        upgradeText.text = upgradeName.ToUpper();
        CalculateCost();
    }
    public void CalculateCost()
    {
        if (upgradeName == PlayerPrefKeys.GridCount)
            upgradeLevel = PlayerPrefs.GetInt(upgradeName, 4);
        else
            upgradeLevel = PlayerPrefs.GetInt(upgradeName, 1);

        levelText.text = upgradeLevel.ToString();
        currentCost = baseCost * upgradeLevel;
        coinText.text = currentCost.ToString();

    }
    public void Upgrade()
    {
        PlayerPrefs.SetInt(upgradeName, upgradeLevel + 1);
        if (upgradeName == PlayerPrefKeys.GridCount)
        {
            GridSystem.GridController.OnGridLiberate.Invoke();
        }
        else
            ExchangeManager.Instance.UpdateIncomeMultiplier();
        ExchangeManager.Instance.UseCurrency(CurrencyType.Coin,currentCost);
        CalculateCost();
    }

    private void Update()
    {
        if (currentCost > ExchangeManager.Instance.GetCurrency(CurrencyType.Coin))
        {
            upgradeButton.interactable = false;
        }
        else
            upgradeButton.interactable = true;
    }
}
