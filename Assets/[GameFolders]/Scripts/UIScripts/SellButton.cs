using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GridSystem.Managers;
using UnityEngine.EventSystems;
public class SellButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    bool onPointer;
    private void Sell(GameObject sellObject)
    {
        int sellCost = sellObject.GetComponent<WarriorController>().warriorData.cost;
        PoolingSystem.ReturnObjectToPool(sellObject);
        InputManager.Instance.takenObject = null;
        ExchangeManager.Instance.AddCurrency(CurrencyType.Coin, sellCost);
        PunchScale();
    }
    public void Check()
    {
        if (InputManager.Instance.takenObject != null&& onPointer)
        {
            Sell(InputManager.Instance.takenObject);
        }
    }
    private void OnEnable()
    {
        InputManager.OnCheckSell.AddListener(Check);
    }
    private void OnDisable()
    {
        InputManager.OnCheckSell.RemoveListener(Check);
    }
    private void PunchScale()
    {
        transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 10, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
    }
}
