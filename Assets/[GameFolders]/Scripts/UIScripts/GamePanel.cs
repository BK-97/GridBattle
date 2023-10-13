using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GamePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public TextMeshProUGUI waveInfoText;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
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
        waveInfoText.text = "Wait For New Wave";
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void Update()
    {
        waveInfoText.text = "WAVE "+GameManager.Instance.GetCurrentWaveLevel();

    }
}
