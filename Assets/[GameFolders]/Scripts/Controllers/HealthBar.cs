using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [Header("Sliders")]
    public Slider healthBar;
    public Slider fakeBar;

    [Header("Behaviour")]
    public BarReactionType barReactionType;
    public float reactionTime=0.3f;

    public bool rotateToCam = false;

    Tween reactionTween;
    private void Update()
    {
        if (!rotateToCam)
            return;
        if (reactionTween!=null)
            return;

        transform.LookAt(Camera.main.transform,Vector3.up);
    }
    public void SetBar(float initValue)
    {
        healthBar.maxValue = initValue;
        healthBar.value = initValue;
        healthBar.enabled = true;
        fakeBar.maxValue = initValue;
        fakeBar.value = initValue;
        fakeBar.enabled = true;
    }
    public void ChangeBar(float newValue)
    {
        healthBar.value = newValue;
        StartCoroutine(FakeBarWaitCO(newValue));

        switch (barReactionType)
        {
            case BarReactionType.ShakeRot:
                ShakeRot();
                break;
            case BarReactionType.ScalePunch:
                PunchScale();
                break;
            default:
                break;
        }
    }
    IEnumerator FakeBarWaitCO(float newValue)
    {
        yield return new WaitForSeconds(reactionTime);
        fakeBar.value = newValue;
    }
    void ShakeRot()
    {
        reactionTween = transform.DOShakeRotation(reactionTime, 10, 10, 10, true, ShakeRandomnessMode.Full);
    }
    void PunchScale()
    {
        reactionTween = transform.DOPunchScale(Vector3.one * 0.1f, reactionTime, 10, 1);
    }
}
public enum BarReactionType { ShakeRot, ScalePunch }
