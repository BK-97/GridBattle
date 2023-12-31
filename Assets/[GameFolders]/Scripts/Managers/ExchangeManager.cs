using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CurrencyType
{
    Coin
}

public class ExchangeManager: Singleton<ExchangeManager>
{
    #region Params
    private Dictionary<CurrencyType, int> currencyDictionary;
    public DictonaryEvent OnCurrencyChange = new DictonaryEvent();
    [HideInInspector]
    public UnityEvent OnCurrencyAdded = new UnityEvent();
    [HideInInspector]
    public Vector3 UIPos;
    private float incomeMultiplier=1.1f;
    public float currentIncomeMultiplier;
    const int STARTER_COIN= 100;
    #endregion
    #region StarterMethods
    public ExchangeManager()
    {
        currencyDictionary = new Dictionary<CurrencyType, int>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.CurrentCoin, STARTER_COIN) < STARTER_COIN)
            PlayerPrefs.SetInt(PlayerPrefKeys.CurrentCoin, STARTER_COIN);

        currencyDictionary[CurrencyType.Coin] = PlayerPrefs.GetInt(PlayerPrefKeys.CurrentCoin, STARTER_COIN);

        UpdateIncomeMultiplier();
    }
    public void SetUIPos(Vector3 pos)
    {
        UIPos = pos;
    }
    #endregion
    #region UpgradeMethods
    public void UpdateIncomeMultiplier()
    {
        currentIncomeMultiplier = PlayerPrefs.GetInt(PlayerPrefKeys.IncomeLevel, 1) * incomeMultiplier;
    }
    #endregion
    #region CurrencyMethods
    public bool UseCurrency(CurrencyType currencyType, int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            if (currencyDictionary[currencyType] >= amount)
            {
                currencyDictionary[currencyType] -= amount;
                PlayerPrefs.SetInt(PlayerPrefKeys.CurrentCoin, currencyDictionary[currencyType]);
                OnCurrencyChange.Invoke(currencyDictionary);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void AddCurrency(CurrencyType currencyType,int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            currencyDictionary[currencyType] += amount;
            PlayerPrefs.SetInt(PlayerPrefKeys.CurrentCoin, currencyDictionary[currencyType]);
            OnCurrencyChange.Invoke(currencyDictionary);
            OnCurrencyAdded.Invoke();
        }
    }

    public int GetCurrency(CurrencyType currencyType)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            return currencyDictionary[currencyType];
        }
        else
        {
            return 0;
        }
    }
    #endregion
}
public class DictonaryEvent : UnityEvent<Dictionary<CurrencyType,int>> { }