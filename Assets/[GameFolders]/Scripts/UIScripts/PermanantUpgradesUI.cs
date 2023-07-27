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
        SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
        LevelManager.Instance.OnLevelStart.AddListener(HidePanel);

    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
        LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);
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
