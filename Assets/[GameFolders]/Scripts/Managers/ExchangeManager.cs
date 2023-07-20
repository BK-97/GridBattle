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
    private Dictionary<CurrencyType, int> currencyDictionary;
    public DictonaryEvent OnCurrencyChange = new DictonaryEvent();
    public Transform UITransform;
    public GameObject coinPrefab;
    public ExchangeManager()
    {
        currencyDictionary = new Dictionary<CurrencyType, int>();
    }
    private void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoin,300);
        Debug.Log("For Test Purposes Set Coin 300");
        currencyDictionary[CurrencyType.Coin] = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCoin, 300);

    }
    public bool UseCurrency(CurrencyType currencyType, int amount)
    {
        Debug.Log(currencyType+" "+amount);
        Debug.Log(currencyDictionary[CurrencyType.Coin]);
        if (currencyDictionary.ContainsKey(currencyType))
        {
            if (currencyDictionary[currencyType] >= amount)
            {
                currencyDictionary[currencyType] -= amount;
                PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoin, currencyDictionary[currencyType]);
                OnCurrencyChange.Invoke(currencyDictionary);
                Debug.Log("Used " + amount + " " + currencyType.ToString());
                return true;
            }
            else
            {
                Debug.Log("Insufficient " + currencyType.ToString());
                return false;
            }
        }
        else
        {
            Debug.Log("Invalid currency type: " + currencyType.ToString());
            return false;
        }
    }

    public void AddCurrency(CurrencyType currencyType,int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            currencyDictionary[currencyType] += amount;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCoin, currencyDictionary[currencyType]);
            OnCurrencyChange.Invoke(currencyDictionary);
        }
        else
        {
            Debug.Log("Invalid currency type: " + currencyType.ToString());
        }

        Debug.Log("Added " + amount + " " + currencyType.ToString());
    }

    public int GetCurrency(CurrencyType currencyType)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            return currencyDictionary[currencyType];
        }
        else
        {
            Debug.Log("Invalid currency type: " + currencyType.ToString());
            return 0;
        }
    }
}
public class DictonaryEvent : UnityEvent<Dictionary<CurrencyType,int>> { }