using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinStack : MonoBehaviour
{
    private int instantiateAmount;
    private CurrencyType coinType;
    private int amount;
    private float instantiateDelay=0.1f;
    private List<Coin> coins=new List<Coin>();
    private float floatingUITime = 1;
    public void SetInfo(int coin, CurrencyType currency)
    {
        coinType = currency;
        amount = coin;
        instantiateAmount = amount % 10;
        StartCoroutine(InstantiateCoinsCO());

    }
    IEnumerator InstantiateCoinsCO()
    {
        for (int i = 0; i < instantiateAmount; i++)
        {
            InstantiateCoin();
            yield return new WaitForSeconds(instantiateDelay);
        }
        yield return new WaitForSeconds(1);

        FloatingToUI();
    }

    private void FloatingToUI()
    {
        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].GoBackParent(floatingUITime);
        }
        Vector3 targetPosition = ExchangeManager.Instance.UIPos;

        transform.DOMove(targetPosition, floatingUITime).OnComplete(() =>
        {
            DestinationReached();
        });
    }
    private void DestinationReached()
    {
        ExchangeManager.Instance.AddCurrency(coinType, amount);
        Destroy(gameObject);
    }
    private void InstantiateCoin()
    {
        float randomX = Random.Range(-1, 1);
        float randomZ = Random.Range(-1, 1);

        randomX /= 2;
        randomZ /= 2;
        Vector3 instantiatePos = new Vector3(transform.position.x + randomX, transform.position.y+0.5f, transform.position.z + randomZ);

        var go = PoolingSystem.Instance.InstantiateAPS("Coin", transform.position,Quaternion.identity,transform);
        coins.Add(go.GetComponent<Coin>());
        go.GetComponent<Coin>().JumpPos(instantiatePos);
    }

}
