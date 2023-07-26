using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinTextController : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private Vector3 offSet;

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
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        offSet = new Vector3(0, 0, -Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(transform.position + offSet);
        Debug.Log(worldPos);
        ExchangeManager.Instance.SetUIPos(worldPos); 
    }
}
