using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class TapToStartPanel : MonoBehaviour
{
    public Transform textTransform;
    private Tween scaleUpTween;
    bool waitingForFirstTouch;
    CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }
    private void StartScale()
    {
        scaleUpTween=textTransform.DOScale(Vector3.one*1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        waitingForFirstTouch = true;

    }
    public void Tapped()
    {
        if (!waitingForFirstTouch)
            return;
        LevelManager.Instance.OnLevelStart.Invoke();
        scaleUpTween.Kill();
        HidePanel();
    }
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
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
        StartScale();
    }
}
