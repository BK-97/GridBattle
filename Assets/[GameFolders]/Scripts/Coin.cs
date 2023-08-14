using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    Tween turnTween;
    public Transform coinModel;
    private void Start()
    {
        turnTween=transform.DORotate(360 * Vector3.up, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    public void JumpPos(Vector3 jumpPos)
    {
        transform.DOJump(jumpPos,2,1,0.6f);
    }
    public void GoBackParent(float timeScale)
    {
        turnTween.Kill();
        transform.DOLocalRotate(Vector3.zero,0.2f);
        transform.DOLocalMove(Vector3.zero, timeScale);
    }
}
