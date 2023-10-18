using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class CoinTextController : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Image coinImage;
    private Vector3 offSet;

    private void OnEnable()
    {
        ExchangeManager.Instance.OnCurrencyChange.AddListener(SetText);
        ExchangeManager.Instance.OnCurrencyAdded.AddListener(CoinPump);
    }
    private void OnDisable()
    {
        ExchangeManager.Instance.OnCurrencyChange.RemoveListener(SetText);
        ExchangeManager.Instance.OnCurrencyAdded.RemoveListener(CoinPump);
    }
    private void SetText(Dictionary<CurrencyType,int> currencyDict)
    {
        coinText.text = currencyDict[CurrencyType.Coin].ToString();
    }
    private void Start()
    {
        coinText.text = ExchangeManager.Instance.GetCurrency(CurrencyType.Coin).ToString();
    }
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        offSet = new Vector3(0, 0, -Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(coinImage.transform.position + offSet);
        ExchangeManager.Instance.SetUIPos(worldPos); 
    }
    public void CoinPump()
    {
        Vector3 currentScale = coinImage.transform.localScale;
        coinImage.transform.DOPunchScale(currentScale * 1.1f,0.2f);
    }
}
