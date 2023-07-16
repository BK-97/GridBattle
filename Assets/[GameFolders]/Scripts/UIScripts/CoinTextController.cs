using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinTextController : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private void OnEnable()
    {
        ExchangeManager.Instance.OnCurrencyChange.AddListener(SetText);
    }
    private void OnDisable()
    {
        ExchangeManager.Instance.OnCurrencyChange.RemoveListener(SetText);

    }
    private void SetText(Dictionary<CurrencyType,int> currencyDict)
    {
        coinText.text = "Coin= " + currencyDict[CurrencyType.Coin];
    }
}
