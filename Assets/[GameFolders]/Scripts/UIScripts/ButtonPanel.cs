using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnBattleSessionStart.AddListener(HidePanel);
        GameManager.Instance.OnSpawnSessionStart.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnBattleSessionStart.RemoveListener(HidePanel);
        GameManager.Instance.OnSpawnSessionStart.RemoveListener(ShowPanel);
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable= false;
    }
    private void ShowPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
