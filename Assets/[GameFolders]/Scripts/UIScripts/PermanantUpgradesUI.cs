using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanantUpgradesUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public int starterMoney;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnStageWin.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageWin.RemoveListener(ShowPanel);
    }
    private void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

    }
    private void ShowPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
