using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
        Debug.Log("1");
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
        Debug.Log("2");

    }
    private void Update()
    {
        if (canCountTime && currentTime > 0)
        {
            Debug.Log(currentTime);
            currentTime -= Time.deltaTime;
            timeSlider.value = currentTime;
            timeText.text = Mathf.RoundToInt(currentTime).ToString();
            if (currentTime <= 0)
            {
                TimeUp();
            }
        }

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
