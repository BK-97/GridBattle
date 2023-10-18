using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyPanel : PanelBase
{
    void Awake()
    {
        HidePanel();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnBattleSessionStart.AddListener(HidePanel);
        GameManager.Instance.OnSpawnSessionStart.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnBattleSessionStart.RemoveListener(HidePanel);
        GameManager.Instance.OnSpawnSessionStart.RemoveListener(ShowPanel);
    }
    public void StartBattle()
    {
        GameManager.Instance.OnBattleSessionStart.Invoke();
    }
}
