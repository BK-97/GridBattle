using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class TapToStartPanel : MonoBehaviour
{
    public Transform textTransform;
    bool waitingForFirstTouch;
    CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        ShowPanel();
    }
    private void Start()
    {
        textTransform.DOScale(Vector3.one*1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        waitingForFirstTouch = true;
    }
    private void Update()
    {
        if (!waitingForFirstTouch)
            return;
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            waitingForFirstTouch = false;
            GameManager.OnGameStart.Invoke();
            HidePanel();
        }
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
