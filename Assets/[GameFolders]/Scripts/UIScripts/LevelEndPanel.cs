using UnityEngine;
public class LevelEndPanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public GameObject levelFailed;
    public GameObject levelCompleted;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnStageWin.AddListener(()=>ShowPanel(true));
        GameManager.Instance.OnStageLoose.AddListener(()=>ShowPanel(false));
        SceneController.Instance.OnSceneLoaded.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageWin.RemoveListener(() => ShowPanel(true));
        GameManager.Instance.OnStageLoose.RemoveListener(() => ShowPanel(false));
        SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanel);
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        levelCompleted.SetActive(false);
        levelFailed.SetActive(false);

    }
    private void ShowPanel(bool status)
    {
        if (status)
            levelCompleted.SetActive(true);
        else
            levelFailed.SetActive(true);

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
