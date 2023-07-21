using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }

    private void OnEnable()
    {
        GameManager.OnGameStart.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        GameManager.OnGameStart.RemoveListener(ShowPanel);
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
