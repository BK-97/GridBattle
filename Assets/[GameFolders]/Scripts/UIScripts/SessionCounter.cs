using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class SessionCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Slider timeSlider;
    private float sessionTime;
    private float currentTime;
    private bool canCountTime;
    public void SetSessionTime(float sTime)
    {
        sessionTime = sTime;
        currentTime = sessionTime;
        timeSlider.maxValue = sessionTime;
        timeSlider.value = sessionTime;
    }

    private void OnEnable()
    {
        EventManager.OnTimeSet.AddListener((float sTime) => SetSessionTime(sTime));
        GameManager.Instance.OnSpawnSessionStart.AddListener(StartTimer);
    }
    private void OnDisable()
    {
        EventManager.OnTimeSet.RemoveListener((float sTime) => SetSessionTime(sTime));
        GameManager.Instance.OnSpawnSessionStart.RemoveListener(StartTimer);
    }
    private void StartTimer()
    {
        currentTime = sessionTime;
        canCountTime = true;

    }
    private void Update()
    {
        if (canCountTime && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeSlider.value = currentTime;
            timeText.text = Mathf.RoundToInt(currentTime).ToString();
            if (currentTime <= 0)
            {
                TimeUp();
            }
            else if (currentTime <= 3 && currentTime > 0)
            {
                int roundedTime = Mathf.RoundToInt(currentTime);
                if (roundedTime != Mathf.RoundToInt(currentTime + Time.deltaTime))
                {
                    PunchScale();
                }
            }
        }
    }
    private void PunchScale()
    {
        timeSlider.gameObject.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1);
    }
    private void TimeUp()
    {
        currentTime = 0;
        timeSlider.value = currentTime;
        timeText.text = Mathf.RoundToInt(currentTime).ToString();
        canCountTime = false;
        GameManager.Instance.OnBattleSessionStart.Invoke();
    }
}
