using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GamePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public TextMeshProUGUI levelText;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    private void ShowPanel()
    {
        levelText.text = "Level "+(PlayerPrefs.GetInt(PlayerPrefKeys.LastLevel, 0) + 1).ToString();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
