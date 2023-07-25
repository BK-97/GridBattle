using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class CoinUp : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private CurrencyType coinType;
    private int amount;
    public void SetInfo(int coin,CurrencyType currency)
    {
        coinType = currency;
        amount = coin;
        coinText.text = amount.ToString();
        MoveToTargetUIElement();
    }

    private void MoveToTargetUIElement()
    {
        Vector3 targetPosition = ExchangeManager.Instance.UIPos;

        Debug.Log(targetPosition);
        transform.DOMove(targetPosition, 1f).OnComplete(() =>
        {
            DestinationReached();
        });
    }
    private void DestinationReached()
    {
        ExchangeManager.Instance.AddCurrency(coinType, amount);
        Destroy(gameObject);
    }
}
