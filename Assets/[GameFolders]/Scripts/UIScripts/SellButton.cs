using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GridSystem.Managers;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SellButton : MonoBehaviour
{
    [SerializeField]
    bool onPointer;
    private void Sell(GameObject sellObject)
    {
        int sellCost = sellObject.GetComponent<WarriorController>().warriorData.cost;
        PoolingSystem.ReturnObjectToPool(sellObject);
        InputManager.Instance.takenObject = null;
        ExchangeManager.Instance.AddCurrency(CurrencyType.Coin, sellCost);
        CharacterManager.Instance.RemoveAlly(sellObject);
        PunchScale();
    }
    public void Check()
    {

        if (InputManager.Instance.takenObject != null && IsPointerOverUI())
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
    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
