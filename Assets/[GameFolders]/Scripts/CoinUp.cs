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
    private Transform targetUIElement;
    public void SetInfo(int coin,CurrencyType currency)
    {
        coinType = currency;
        amount = coin;
        coinText.text = amount.ToString();
        targetUIElement = ExchangeManager.Instance.UITransform;
        MoveToTargetUIElement();
    }

    private void MoveToTargetUIElement()
    {
        Debug.Log("Move");

        Vector3 targetPosition = targetUIElement.position;

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
